using Poi.Model;
using Poi.Util;
using System.Data;

namespace Poi.Dao
{
    class CityDao
    {
        private static string fields = "id, name";

        private static City mapper(DataRow row)
        {
            City city = new City();
            city.Id = row["id"];
            city.Name = row["name"].ToString();
            return city;
        }

        public static City SelectByName(string name)
        {
            string sql = "select " + fields + " from city where name = '" + name + "'";
            DataTable table = DbUtil.Query(sql);

            if (table.Rows.Count == 0)
            {
                return null;
            }

            return mapper(table.Rows[0]);
        }
    }
}
