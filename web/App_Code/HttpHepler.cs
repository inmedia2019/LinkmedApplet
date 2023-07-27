using System.IO;
using System.Collections;
using System.Net;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

/// <summary>
/// Class1 的摘要说明
/// </summary>
public class HttpHepler
{


    
    public static string Http(string url, string method = "GET", string contenttype = "application/json;charset=utf-8", Hashtable header = null, string data = null)
    {
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = string.IsNullOrEmpty(method) ? "GET" : method;
        request.ContentType = string.IsNullOrEmpty(contenttype) ? "application/json;charset=utf-8" : contenttype;
        if (header != null)
        {
            foreach (var i in header.Keys)
            {
                request.Headers.Add(i.ToString(), header[i].ToString());
            }
        }
        if (!string.IsNullOrEmpty(data))
        {
            Stream RequestStream = request.GetRequestStream();
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            RequestStream.Write(bytes, 0, bytes.Length);
            RequestStream.Close();
        }
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream ResponseStream = response.GetResponseStream();
        StreamReader StreamReader = new StreamReader(ResponseStream, Encoding.GetEncoding("utf-8"));
        string re = StreamReader.ReadToEnd();
        StreamReader.Close();
        ResponseStream.Close();
        return re;
    }

    private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
    {
        return true;
    }


}