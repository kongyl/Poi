using log4net;
using Poi.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Poi.Baidu
{
    class JsonParser
    {
        private static ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private Dictionary<string, object> res;

        public JsonParser(string resStr)
        {
            res = parseJson(resStr);
        }

        // 解析 json
        private Dictionary<string, object> parseJson(string json)
        {
            Dictionary<string, object> model = null;

            if (json != null)
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                try
                {
                    model = js.Deserialize<Dictionary<string, object>>(json);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }

            return model;
        }

        // 获取返回状态码
        public int GetStatus()
        {
            int status = 7;

            try
            {
                status = Convert.ToInt32(res["status"]);
            }
            catch (Exception ex) // json 解析错误
            {
                logger.Error(ex.Message);
            }

            return status;
        }

        // 获取总数
        public int GetTotalNum(string regionName)
        {
            int totalNum = 0;

            try
            {
                IList results = res["results"] as ArrayList;
                foreach (Dictionary<string, object> result in results)
                {
                    string name = result["name"].ToString();
                    if (name.Equals(regionName))
                    {
                        totalNum = Convert.ToInt32(result["num"]);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return totalNum;
        }

        // 获取 poi
        public List<PoiInfo> GetPois()
        {
            List<PoiInfo> poiList = new List<PoiInfo>();

            try
            {
                IList results = res["results"] as ArrayList;
                foreach (Dictionary<string, object> result in results)
                {
                    PoiInfo poi = new PoiInfo();
                    poi.Name = result["name"].ToString();
                    Dictionary<string, object> location = result["location"] as Dictionary<string, object>;
                    poi.Lat = Convert.ToSingle(location["lat"]);
                    poi.Lng = Convert.ToSingle(location["lng"]);
                    poi.Address = result["address"].ToString();
                    if (result.ContainsKey("telephone"))
                    {
                        poi.Telephone = result["telephone"].ToString();
                    }
                    poi.Uid = result["uid"].ToString();
                    if (result.ContainsKey("detail_info"))
                    {
                        Dictionary<string, object> detailInfo = result["detail_info"] as Dictionary<string, object>;
                        if (detailInfo.ContainsKey("type"))
                        {
                            poi.Type = detailInfo["type"].ToString();
                        }
                        if (detailInfo.ContainsKey("tag"))
                        {
                            poi.Tag = detailInfo["tag"].ToString();
                        }
                        if (detailInfo.ContainsKey("detail_url"))
                        {
                            poi.DetailUrl = detailInfo["detail_url"].ToString();
                        }
                        if (detailInfo.ContainsKey("price"))
                        {
                            poi.Price = detailInfo["price"].ToString();
                        }
                        if (detailInfo.ContainsKey("shop_hours"))
                        {
                            poi.ShopHours = detailInfo["shop_hours"].ToString();
                        }
                        if (detailInfo.ContainsKey("overall_rating"))
                        {
                            poi.OverallRating = detailInfo["overall_rating"].ToString();
                        }
                        if (detailInfo.ContainsKey("taste_rating"))
                        {
                            poi.TasteRating = detailInfo["taste_rating"].ToString();
                        }
                        if (detailInfo.ContainsKey("service_rating"))
                        {
                            poi.ServiceRating = detailInfo["service_rating"].ToString();
                        }
                        if (detailInfo.ContainsKey("environment_rating"))
                        {
                            poi.EnvironmentRating = detailInfo["environment_rating"].ToString();
                        }
                        if (detailInfo.ContainsKey("facility_rating"))
                        {
                            poi.FacilityRating = detailInfo["facility_rating"].ToString();
                        }
                        if (detailInfo.ContainsKey("hygiene_rating"))
                        {
                            poi.HygieneRating = detailInfo["hygiene_rating"].ToString();
                        }
                        if (detailInfo.ContainsKey("technology_rating"))
                        {
                            poi.TechnologyRating = detailInfo["technology_rating"].ToString();
                        }
                        if (detailInfo.ContainsKey("image_num"))
                        {
                            poi.ImageNum = detailInfo["image_num"].ToString();
                        }
                        if (detailInfo.ContainsKey("groupon_num"))
                        {
                            poi.GrouponNum = Convert.ToInt32(detailInfo["groupon_num"]);
                        }
                        if (detailInfo.ContainsKey("discount_num"))
                        {
                            poi.DiscountNum = Convert.ToInt32(detailInfo["discount_num"]);
                        }
                        if (detailInfo.ContainsKey("comment_num"))
                        {
                            poi.CommentNum = detailInfo["comment_num"].ToString();
                        }
                        if (detailInfo.ContainsKey("favorite_num"))
                        {
                            poi.FavoriteNum = detailInfo["favorite_num"].ToString();
                        }
                        if (detailInfo.ContainsKey("checkin_num"))
                        {
                            poi.CheckinNum = detailInfo["checkin_num"].ToString();
                        }
                    }
                    poiList.Add(poi);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return null; // json 解析错误
            }

            return poiList;
        }
    }
}
