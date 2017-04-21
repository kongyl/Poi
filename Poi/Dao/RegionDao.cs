using Poi.Model;
using Poi.Util;
using System.Collections.Generic;
using System.Data;

namespace Poi.Dao
{
    class RegionDao
    {
        private static string fields = "id, name, parentId";

        private static Region mapper(DataRow row)
        {
            Region region = new Region();
            region.Id = row["id"];
            region.Name = row["name"].ToString();
            region.ParentId = row["parentId"];
            return region;
        }

        public static Region SelectByPrimaryKey(int id)
        {
            string sql = "select " + fields + " from region where id = " + id.ToString();
            DataTable table = DbUtil.Query(sql);

            if (table.Rows.Count == 0)
            {
                return null;
            }

            return mapper(table.Rows[0]);
        }

        public static List<Region> SelectByParentId(int parentId)
        {
            List<Region> regionList = new List<Region>();

            string sql = "select " + fields + " from region where parentId = " + parentId.ToString();
            DataTable table = DbUtil.Query(sql);
            foreach (DataRow row in table.Rows)
            {
                regionList.Add(mapper(row));
            }

            return regionList;
        }
    }
}
