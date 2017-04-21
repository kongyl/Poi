using Poi.Model;
using Poi.Util;
using System.Data;
using System.Data.SQLite;

namespace Poi.Dao
{
    class DictionaryDao
    {
        private static string fields = "id, key, value";

        private static Dictionary mapper(DataRow row)
        {
            Dictionary dictionary = new Dictionary();
            dictionary.Id = row["id"];
            dictionary.Key = row["key"].ToString();
            dictionary.Value = row["value"].ToString();
            return dictionary;
        }

        public static Dictionary SelectByKey(string key)
        {
            string sql = "select " + fields + " from dictionary where key = @key";
            SQLiteCommand cmd = new SQLiteCommand(sql);
            cmd.Parameters.Add("key", DbType.String).Value = key;
            DataTable table = DbUtil.Query(cmd);

            if (table.Rows.Count == 0)
            {
                return null;
            }

            return mapper(table.Rows[0]);
        }

        public static int UpdateByKey(Dictionary dictionary)
        {
            string sql = "update dictionary set value = @value where key = @key";
            SQLiteCommand cmd = new SQLiteCommand(sql);
            cmd.Parameters.Add("key", DbType.String).Value = dictionary.Key;
            cmd.Parameters.Add("value", DbType.String).Value = dictionary.Value;

            return DbUtil.Execute(cmd);
        }
    }
}
