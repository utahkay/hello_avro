using System.IO;
using System.Net;
using System.Text;

namespace HelloAvro.Main
{
    public class HttpStatusAndResponseBody
    {
        public string ResponseBody { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public WebException Exception { get; set; }
    }

    public class HttpHelper
    {
        public static HttpStatusAndResponseBody Get(string url, string contentType)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = contentType;

            var response = (HttpWebResponse) request.GetResponse();
            return new HttpStatusAndResponseBody
                       {
                           StatusCode = response.StatusCode,
                           ResponseBody = GetResponseBody(response)
                       };
        }

        public static HttpStatusAndResponseBody Post(string url, string contentType, string payload)
        {
            byte[] dataArray = Encoding.Default.GetBytes(payload);

            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "POST";
            request.ContentLength = dataArray.Length;
            request.ContentType = contentType;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(dataArray, 0, dataArray.Length);
            }

            var response = (HttpWebResponse) request.GetResponse();
            return new HttpStatusAndResponseBody
                       {
                           StatusCode = response.StatusCode,
                           ResponseBody = GetResponseBody(response)
                       };
        }

        public static HttpStatusCode Delete(string url, string contentType)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = contentType;
            request.Method = "DELETE";

            var response = (HttpWebResponse) request.GetResponse();
            return response.StatusCode;
        }

        private static string GetResponseBody(HttpWebResponse httpWebResponse)
        {
            var responseStream = httpWebResponse.GetResponseStream();
            var responseBody = "";
            if (responseStream != null)
            {
                using (var reader = new StreamReader(responseStream))
                {
                    responseBody = reader.ReadToEnd();
                }
            }
            return responseBody;
        }
    }
}
