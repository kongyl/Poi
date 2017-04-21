using Poi.Model;
using Poi.Util;
using System.Collections.Generic;
using System.Data;

namespace Poi.Dao
{
    class KeyWordDao
    {
        private static string fields = "id, name, parentId, type";

        private static KeyWord mapper(DataRow row)
        {
            KeyWord keyWord = new KeyWord();
            keyWord.Id = row["id"];
            keyWord.Name = row["name"].ToString();
            keyWord.ParentId = row["parentId"];
            keyWord.Type = row["type"];
            return keyWord;
        }

        public static List<KeyWord> SelectByParentId(int parentId)
        {
            List<KeyWord> keyWordList = new List<KeyWord>();

            string sql = "select " + fields + " from keyWord where parentId = " + parentId.ToString();
            DataTable table = DbUtil.Query(sql);
            foreach (DataRow row in table.Rows)
            {
                keyWordList.Add(mapper(row));
            }

            return keyWordList;
        }
    }
}
