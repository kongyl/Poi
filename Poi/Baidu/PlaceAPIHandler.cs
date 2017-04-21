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
        private DataGridView dataGridView; // 输出的 DataGridView

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

        public PlaceAPIHandler(Region region, string keyWord, DataGridView dataGridView)
        {
            // 外部参数
            this.region = region;
            this.keyWord = keyWord;
            this.dataGridView = dataGridView;

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
                    errMsg = "权限错误，请检查 ak 和 sk 配置";
                    break;
                case -4:
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

        /*/ 根据页码获取数据
        public int GetBoundsData(int rowNum, int colNum, DataGridView dataGridView)
        {
            // 获取对应矢量
            string cityName = region.Name;
            City city = CityDao.SelectByName(cityName);
            if (city == null)
            {
                return -6;
            }
            int fid = Convert.ToInt32(city.Id);
            Geometry geometry = GisUtil.GetGeometry(fid);

            // 获取间隔
            Envelope envelope = new Envelope();
            geometry.GetEnvelope(envelope);
            double xInterval = (envelope.MaxX - envelope.MinX) / colNum;
            double yInterval = (envelope.MaxY - envelope.MinY) / rowNum;

            // 逐分块请求
            double minX = envelope.MinX;
            double minY = envelope.MinY;
            double maxX;
            double maxY;
            for (int i = 0; i < rowNum; i++)
            {
                maxY = minY + yInterval;
                for (int j = 0; j < colNum; j++)
                {
                    maxX = minX + xInterval;

                    int subType = GisUtil.GetEnvelopeRelationship(geometry, minX, maxX, minY, maxY);
                    if (subType > 0)
                    {
                        string bounds = string.Format("{0},{1},{2},{3}", minY, minX, maxY, maxX);
                        //if (subType > 1) // geometry 包含整个子块
                        //{
                            int page = 0;
                            int stopFlag = 0;
                            while (stopFlag == 0)
                            {
                                stopFlag = GetBoundsPageData(bounds, page, dataGridView);
                                page++;
                            }
                        //}
                    }

                    minX = maxX;
                }
                minY = maxY;
            }

            return 0;
        }*/

        // 根据页码获取 bounds 数据
        private int GetBoundsPageData(string bounds, int page)
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

            return RequestData(url);
        }

        // 请求数据
        private int RequestData(string url)
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

            // 输出
            outputDataGridView(poiList, dataGridView); // 输出到 DataGridView

            return 0;
        }

        // 输出到 DataGridView
        private void outputDataGridView(List<PoiInfo> poiList, DataGridView dataGridView)
        {
            foreach (PoiInfo poi in poiList)
            {
                getNum++;

                DataGridViewRow row = new DataGridViewRow();
                int index = dataGridView.Rows.Add(row);
                // 编号
                dataGridView.Rows[index].Cells[0].Value = getNum;
                // 标题
                dataGridView.Rows[index].Cells[1].Value = poi.Name;
                // 坐标
                dataGridView.Rows[index].Cells[2].Value = string.Format("{0}, {1}", poi.Lat, poi.Lng);
                // 地址
                dataGridView.Rows[index].Cells[3].Value = poi.Address;
                // 定位到当前行，以触发刷新事件
                dataGridView.CurrentCell = dataGridView.Rows[index].Cells[0];                
            }
        }
    }
}
