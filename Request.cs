using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace RPCX
{
    public class TrustAllCertificatePolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint sp,
         X509Certificate cert, WebRequest req, int problem)
        {
            return true;
        }
    }
    public class Request
    {
        public static JObject InvokeMethod(string a_sMethod, params object[] a_params)
        {
            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            string Url = "http://192.168.31.245:8332";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Url);
            NetworkCredential nc = new NetworkCredential("steel", "Sftechnology2020!!");
            webRequest.Credentials = nc;

            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";

            JObject joe = new JObject();
            joe["jsonrpc"] = "1.0";
            joe["id"] = "1";
            joe["verbose"] = "1";
            joe["method"] = a_sMethod;

            if (a_params != null)
            {
                if (a_params.Length > 0)
                {
                    JArray props = new JArray();
                    foreach (var p in a_params)
                    {
                        props.Add(p);
                    }
                    joe.Add(new JProperty("params", props));
                }
            }

            string s = JsonConvert.SerializeObject(joe);
            // serialize json for the request
            byte[] byteArray = Encoding.UTF8.GetBytes(s);
            webRequest.ContentLength = byteArray.Length;

            try
            {
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch (WebException we)
            {

                //inner exception is socket
                //{"A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 23.23.246.5:8332"}
                throw;
            }
            WebResponse webResponse = null;
            try
            {
                using (webResponse = webRequest.GetResponse())
                {
                    using (Stream str = webResponse.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(str))
                        {
                            return JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                        }
                    }
                }
            }
            catch (WebException webex)
            {
                using (Stream str = webex.Response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(str))
                    {
                        var tempRet = JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                        return tempRet;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static JObject InvokeMethodX(string a_sMethod, params object[] a_params)
        {
            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            string Url = "http://192.168.31.245:8334";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Url);
            NetworkCredential nc = new NetworkCredential("steel", "Sftechnology2020");
            webRequest.Credentials = nc;

            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";

            JObject joe = new JObject();
            joe["jsonrpc"] = "1.0";
            joe["id"] = "1";
            joe["verbose"] = "1";
            joe["method"] = a_sMethod;

            if (a_params != null)
            {
                if (a_params.Length > 0)
                {
                    JArray props = new JArray();
                    foreach (var p in a_params)
                    {
                        props.Add(p);
                    }
                    joe.Add(new JProperty("params", props));
                }
            }

            string s = JsonConvert.SerializeObject(joe);
            // serialize json for the request
            byte[] byteArray = Encoding.UTF8.GetBytes(s);
            webRequest.ContentLength = byteArray.Length;

            try
            {
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch (WebException we)
            {

                //inner exception is socket
                //{"A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 23.23.246.5:8332"}
                throw;
            }
            WebResponse webResponse = null;
            try
            {
                using (webResponse = webRequest.GetResponse())
                {
                    using (Stream str = webResponse.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(str))
                        {
                            return JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                        }
                    }
                }
            }
            catch (WebException webex)
            {
                using (Stream str = webex.Response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(str))
                    {
                        var tempRet = JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                        return tempRet;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}