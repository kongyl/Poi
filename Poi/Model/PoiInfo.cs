namespace Poi.Model
{
    class PoiInfo
    {
        public string Name { get; set; } // poi 名称

        public float Lat { get; set; } // 纬度值

        public float Lng { get; set; } // 经度值

        public string Address { get; set; } // poi 地址信息

        public string Telephone { get; set; } // poi 电话信息

        public string Uid { get; set; } // poi 的唯一标示

        public string Type { get; set; } // 所属分类

        public string Tag { get; set; } // 标签

        public string DetailUrl { get; set; } // poi 的详情页

        public string Price { get; set; } // poi 商户的价格

        public string ShopHours { get; set; } // 营业时间

        public string OverallRating { get; set; } // 总体评分

        public string TasteRating { get; set; } // 口味评分

        public string ServiceRating { get; set; } // 服务评分

        public string EnvironmentRating { get; set; } // 环境评分

        public string FacilityRating { get; set; } // 星级（设备）评分

        public string HygieneRating { get; set; } // 卫生评分

        public string TechnologyRating { get; set; } // 技术评分

        public string ImageNum { get; set; } // 图片数

        public int GrouponNum { get; set; } // 团购数

        public int DiscountNum { get; set; } // 优惠数

        public string CommentNum { get; set; } // 评论数

        public string FavoriteNum { get; set; } // 收藏数

        public string CheckinNum { get; set; } // 签到数
    }
}
