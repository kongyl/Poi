using log4net;
using System;
using System.Data;
using System.Data.SQLite;
using System.Reflection;

namespace Poi.Util
{
    class DbUtil
    {
        private static ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static string connStr = "Data Source = " + Environment.CurrentDirectory + "\\poi.db";
        private static SQLiteConnection conn = new SQLiteConnection(connStr);

        private static bool open()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
        }

        private static void close()
        {
            try
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        // 查询
        public static DataTable Query(string sqlStr)
        {
            DataTable dt = new DataTable();
            if (open())
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand(sqlStr, conn);
                    SQLiteDataAdapter adpt = new SQLiteDataAdapter(cmd);
                    adpt.Fill(dt);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                }
                close();
            }

            return dt;
        }

        // 查询
        public static DataTable Query(SQLiteCommand cmd)
        {
            DataTable dt = new DataTable();
            if (open())
            {
                try
                {
                    cmd.Connection = conn;
                    SQLiteDataAdapter adpt = new SQLiteDataAdapter(cmd);
                    adpt.Fill(dt);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                }
                close();
            }

            return dt;
        }

        // 增删改
        public static int Execute(string sqlStr)
        {
            try
            {
                if (open())
                {
                    SQLiteCommand cmd = new SQLiteCommand(sqlStr, conn);
                    return cmd.ExecuteNonQuery();
                }
                return -1;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return -1;
            }
            finally
            {
                close();
            }
        }

        // 增删改
        public static int Execute(SQLiteCommand cmd)
        {
            try
            {
                if (open())
                {
                    cmd.Connection = conn;
                    return cmd.ExecuteNonQuery();
                }
                return -1;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return -1;
            }
            finally
            {
                close();
            }
        }
    }
}
