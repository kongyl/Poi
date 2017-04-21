using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Poi.Baidu
{
    class AKSNCalculater
    {
        private static string MD5(string text)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            try
            {
                MD5CryptoServiceProvider crypt = new MD5CryptoServiceProvider();
                byte[] hash = crypt.ComputeHash(textBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte a in hash)
                {
                    sb.Append(a.ToString("x2"));
                }
                return sb.ToString();
            }
            catch
            {
                throw;
            }
        }

        private static string UrlEncode(string str)
        {
            str = HttpUtility.UrlEncode(str);
            byte[] buf = Encoding.ASCII.GetBytes(str);
            for (int i = 0; i < buf.Length; i++)
            {
                if (buf[i] == '%')
                {
                    if (buf[i + 1] >= 'a')
                    {
                        buf[i + 1] -= 32;
                    }
                    if (buf[i + 2] >= 'a')
                    {
                        buf[i + 2] -= 32;
                    }
                    i += 2;
                }
            }
            return Encoding.ASCII.GetString(buf);
        }

        private static string HttpBuildQuery(IDictionary<string, string> queryStrings, string ak)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in queryStrings)
            {
                sb.Append(UrlEncode(item.Key));
                sb.Append("=");
                sb.Append(UrlEncode(item.Value));
                sb.Append("&");
            }
            sb.Append("ak=");
            sb.Append(ak);
            return sb.ToString();
        }

        public static string CaculateAKSN(string ak, string sk, string url, IDictionary<string, string> queryStrs)
        {
            string queryStr = HttpBuildQuery(queryStrs, ak);
            string str = url + "?" + queryStr;
            return str + "&sn=" + MD5(UrlEncode(str + sk));
        }
    }
}
