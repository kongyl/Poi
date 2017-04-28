using NPOI.SS.UserModel;
using Poi.Model;

namespace Poi.Util
{
    class XlsUtil
    {
        public static void AddBaiduHeader(ISheet sheet)
        {
            IRow headRow = sheet.CreateRow(0);
            headRow.CreateCell(0).SetCellValue("编号");
            headRow.CreateCell(1).SetCellValue("名称");
            headRow.CreateCell(2).SetCellValue("经度");
            headRow.CreateCell(3).SetCellValue("纬度");
            headRow.CreateCell(4).SetCellValue("地址");
            headRow.CreateCell(5).SetCellValue("电话");
            headRow.CreateCell(6).SetCellValue("分类");
            headRow.CreateCell(7).SetCellValue("标签");
            headRow.CreateCell(8).SetCellValue("详情页");
            headRow.CreateCell(9).SetCellValue("价格");
            headRow.CreateCell(10).SetCellValue("营业时间");
            headRow.CreateCell(11).SetCellValue("总体评分");
            headRow.CreateCell(12).SetCellValue("口味评分");
            headRow.CreateCell(13).SetCellValue("服务评分");
            headRow.CreateCell(14).SetCellValue("环境评分");
            headRow.CreateCell(15).SetCellValue("星级（设备）评分");
            headRow.CreateCell(16).SetCellValue("卫生评分");
            headRow.CreateCell(17).SetCellValue("技术评分");
            headRow.CreateCell(18).SetCellValue("图片数");
            headRow.CreateCell(19).SetCellValue("团购数");
            headRow.CreateCell(20).SetCellValue("优惠数");
            headRow.CreateCell(21).SetCellValue("评论数");
            headRow.CreateCell(22).SetCellValue("收藏数");
            headRow.CreateCell(23).SetCellValue("签到数");
        }

        public static void AddBaiduContent(ISheet sheet, PoiInfo poi, int index)
        {
            IRow contentRow = sheet.CreateRow(index);
            contentRow.CreateCell(0).SetCellValue(index);
            contentRow.CreateCell(1).SetCellValue(poi.Name);
            contentRow.CreateCell(2).SetCellValue(poi.Lng);
            contentRow.CreateCell(3).SetCellValue(poi.Lat);
            contentRow.CreateCell(4).SetCellValue(poi.Address);
            if (poi.Telephone != null)
            {
                contentRow.CreateCell(5).SetCellValue(poi.Telephone);
            }
            if (poi.Type != null)
            {
                contentRow.CreateCell(6).SetCellValue(poi.Type);
            }
            if (poi.Tag != null)
            {
                contentRow.CreateCell(7).SetCellValue(poi.Tag);
            }
            if (poi.DetailUrl != null)
            {
                contentRow.CreateCell(8).SetCellValue(poi.DetailUrl);                
            }
            if (poi.Price != null)
            {
                contentRow.CreateCell(9).SetCellValue(poi.Price);                
            }
            if (poi.ShopHours != null)
            {
                contentRow.CreateCell(10).SetCellValue(poi.ShopHours);                
            }
            if (poi.OverallRating != null)
            {
                contentRow.CreateCell(11).SetCellValue(poi.OverallRating);                
            }
            if (poi.TasteRating != null)
            {
                contentRow.CreateCell(12).SetCellValue(poi.TasteRating);
            }
            if (poi.ServiceRating != null)
            {
                contentRow.CreateCell(13).SetCellValue(poi.ServiceRating);                
            }
            if (poi.EnvironmentRating != null)
            {
                contentRow.CreateCell(14).SetCellValue(poi.EnvironmentRating);                
            }
            if (poi.FacilityRating != null)
            {
                contentRow.CreateCell(15).SetCellValue(poi.FacilityRating);                
            }
            if (poi.HygieneRating != null)
            {
                contentRow.CreateCell(16).SetCellValue(poi.HygieneRating);                
            }
            if (poi.TechnologyRating != null)
            {
                contentRow.CreateCell(17).SetCellValue(poi.TechnologyRating);                
            }
            if (poi.ImageNum != null)
            {
                contentRow.CreateCell(18).SetCellValue(poi.ImageNum);                
            }
            if (poi.GrouponNum != null)
            {
                contentRow.CreateCell(19).SetCellValue(poi.GrouponNum);                
            }
            if (poi.DiscountNum != null)
            {
                contentRow.CreateCell(20).SetCellValue(poi.DiscountNum);                
            }
            if (poi.CommentNum != null)
            {
                contentRow.CreateCell(21).SetCellValue(poi.CommentNum);                
            }
            if (poi.FavoriteNum != null)
            {
                contentRow.CreateCell(22).SetCellValue(poi.FavoriteNum);                
            }
            if (poi.CheckinNum != null)
            {
                contentRow.CreateCell(23).SetCellValue(poi.CheckinNum);
            }           
        }
    }
}
