﻿using NPOI.SS.UserModel;
using OSGeo.OGR;
using Poi.Dao;
using Poi.Model;
using Poi.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Poi.Baidu
{
    class PlaceAPIHandler
    {
        private string ak;
        private string sk;
        private string url;
        private string keyWord;
        private Region region;
        private int totalNum; // 一次搜索的总数
        private int getNum; // 获取的poi数量
        private DataGridView dataGridView;
        private ISheet sheet;

        public int GetNum
        {
            get
            {
                return getNum;
            }
            set
            {
                getNum = value;
            }
        }

        public int TotalNum
        {
            get
            {
                return totalNum;
            }
            set
            {
                totalNum = value;
            }
        }

        public PlaceAPIHandler(Region region, string keyWord, DataGridView dataGridView = null, ISheet sheet = null)
        {
            // 外部参数
            this.region = region;
            this.keyWord = keyWord;
            this.dataGridView = dataGridView;
            this.sheet = sheet;

            // 内部参数
            Dictionary dicAk = DictionaryDao.SelectByKey("ak");
            ak = dicAk.Value;
            Dictionary dicSk = DictionaryDao.SelectByKey("sk");
            sk = dicSk.Value;
            url = "/place/v2/search";
            totalNum = 0;
            getNum = 0;
        }

        // 生成 url
        private string generateUrl(IDictionary<string, string> paramDic)
        {
            return "http://api.map.baidu.com" + AKSNCalculater.CaculateAKSN(ak, sk, url, paramDic);
        }

        // 获取总数
        public int GetTotalNum(string parentRegion)
        {
            // 组装参数
            IDictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("q", keyWord);
            int parentId = Convert.ToInt32(region.ParentId);
            if (parentId == 0)
            {
                paramDic.Add("region", "全国");
            }
            else
            {
                paramDic.Add("region", parentRegion);
            }
            paramDic.Add("output", "json");

            // 发送请求
            string url = generateUrl(paramDic);
            string res = HttpUtil.GetResponseContent(url);
            if (res == null)
            {
                return -1; // 请求失败，没有网络
            }

            // 解析响应
            JsonParser jsonParser = new JsonParser(res);
            int status = jsonParser.GetStatus();
            if (status != 0) // 返回错误
            {
                return -status;
            }

            totalNum = jsonParser.GetTotalNum(region.Name);

            return totalNum;
        }

        // 返回错误信息
        public static string GetErrMsg(int code)
        {
            string errMsg = "";
            switch (code)
            {
                case -1:
                    errMsg = "请求失败，请检查网络";
                    break;
                case -2:
                    errMsg = "请求参数错误";
                    break;
                case -3:
                case -5:
                case -200:
                    errMsg = "权限错误，请检查 ak 和 sk 配置";
                    break;
                case -4:
                case -302:
                    errMsg = "今日配额已达上限";
                    break;
                case -6:
                    errMsg = "此城市没有相应的矢量数据，无法分块获取 POI";
                    break;
                case -7:
                    errMsg = "json 解析错误";
                    break;
                default:
                    errMsg = "POI 获取失败";
                    break;
            }
            return errMsg;
        }

        // 获取页数，受api限制，不超过400条数据
        public int GetLimitedPageNum()
        {
            int limitedTotal = totalNum;
            if (limitedTotal > 400)
            {
                limitedTotal = 400;
            }

            return (limitedTotal + 19) / 20;
        }

        // 根据页码获取 region 数据
        public int GetRegionPageData(int page)
        {
            // 组装参数
            IDictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("q", keyWord);
            paramDic.Add("region", region.Name);
            paramDic.Add("output", "json");
            paramDic.Add("scope", "2"); // 获取详情
            paramDic.Add("page_size", "20");
            paramDic.Add("page_num", page.ToString());
            paramDic.Add("coord_type", "1"); // wgs84
            // 发送请求
            string url = generateUrl(paramDic);

            return RequestData(url);
        }

        // 根据页码获取 bounds 数据
        public int GetBoundsPageData(string bounds, int page, Geometry geometry)
        {
            // 组装参数
            IDictionary<string, string> paramDic = new Dictionary<string, string>();
            paramDic.Add("q", keyWord);
            paramDic.Add("bounds", bounds);
            paramDic.Add("output", "json");
            paramDic.Add("scope", "2"); // 获取详情
            paramDic.Add("page_size", "20");
            paramDic.Add("page_num", page.ToString());
            paramDic.Add("coord_type", "1"); // wgs84
            // 发送请求
            string url = generateUrl(paramDic);

            return RequestData(url, geometry);
        }

        // 请求数据
        private int RequestData(string url, Geometry geometry = null)
        {
            string res = HttpUtil.GetResponseContent(url);
            if (res == null)
            {
                return -1; // 请求失败，没有网络
            }

            // 解析响应
            JsonParser jsonParser = new JsonParser(res);
            int status = jsonParser.GetStatus(); // 获取状态码
            if (status != 0) // 返回错误
            {
                return -status;
            }

            // 获取 poi
            List<PoiInfo> poiList = jsonParser.GetPois();
            if (poiList == null) // json 解析错误
            {
                return -7;
            }
            if (poiList.Count == 0) // 请求成功，但没有结果
            {
                return 1;
            }

            if (geometry != null) // 剔除 Geometry 外的点
            {
                poiList = removeOuter(poiList, geometry);
            }

            // 输出
            output(poiList); // 输出到 DataGridView

            return 0;
        }

        // 输出
        private void output(List<PoiInfo> poiList)
        {
            foreach (PoiInfo poi in poiList)
            {
                getNum++;

                // 输出到 Excel
                if (sheet != null)
                {
                    XlsUtil.AddBaiduContent(sheet, poi, getNum);
                }

                // 输出到 DataGridView
                if (dataGridView != null)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    int index = dataGridView.Rows.Add(row);
                    // 编号
                    dataGridView.Rows[index].Cells[0].Value = getNum;
                    // 标题
                    dataGridView.Rows[index].Cells[1].Value = poi.Name;
                    // 坐标
                    dataGridView.Rows[index].Cells[2].Value = string.Format("{0}, {1}", poi.Lng, poi.Lat);
                    // 地址
                    dataGridView.Rows[index].Cells[3].Value = poi.Address;
                    // 定位到当前行，以触发刷新事件
                    dataGridView.CurrentCell = dataGridView.Rows[index].Cells[0];
                }
            }
        }

        // 剔除 Geometry 外的点
        private List<PoiInfo> removeOuter(List<PoiInfo> poiList, Geometry geometry)
        {
            List<PoiInfo> newPoiList = new List<PoiInfo>();

            foreach (PoiInfo poi in poiList)
            {
                if (GisUtil.IsContainsPoint(geometry, poi.Lng, poi.Lat))
                {
                    newPoiList.Add(poi);
                }
            }

            return newPoiList;
        }
    }
}
