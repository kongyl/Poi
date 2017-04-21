using log4net;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace Poi.Util
{
    class HttpUtil
    {
        private static ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string GetResponseContent(string url)
        {
            Uri uri = new Uri(url);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);

            string responseContent = null;
            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream stream = res.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"));
                responseContent = reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                logger.Error(ex.ToString());
            }

            return responseContent;
        }
    }
}
