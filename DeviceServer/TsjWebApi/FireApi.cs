using FireProtectionV1.FireWorking.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TsjWebApi
{
    public class FireApi
    { 
          /// <summary>
          /// 异步请求post（键值对形式,可等待的）
          /// </summary>
          /// <param name="url">网络的地址("/api/UMeng")</param>
          /// <param name="formData">键值对List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();formData.Add(new KeyValuePair<string, string>("userid", "29122"));formData.Add(new KeyValuePair<string, string>("umengids", "29122"));</param>
          /// <param name="charset">编码格式</param>
          /// <param name="mediaType">头媒体类型</param>
          /// <returns></returns>
        static public string HttpPost(string url, DeviceBaseInput param = null )
        {
            param.Origin = Origin.Anjisi;
            string postData = JsonConvert.SerializeObject(param);
            //定义request并设置request的路径
            WebRequest request = WebRequest.Create(url);
            request.Method = "post";
            //初始化request参数
            //string postData = "{\"dataSource\":\"DataSource=192.168.0.70/orcl70;UserID=sde;PassWord=sde;\",\"type\":\"0\",\"whereCondition\":\"dlwz='国和百寿一村6号'\",\"tableName\":\"hzdzd_pt\"}";
            //var json = "{ \"dataSource\": \"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.70)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl70)));User ID=sde;Password=sde;Unicode=True\" }";
            //设置参数的编码格式，解决中文乱码
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            //设置request的MIME类型及内容长度
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            //打开request字符流
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            //定义response为前面的request响应
            try
            {
                WebResponse response = request.GetResponse();
                //获取相应的状态代码
                //Console.WriteLine(DateTime.Now+ $"HttpPost ok  url:{url} postData:{postData}");
                //定义response字符流
                var stream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream))
                {
                    string res= reader.ReadToEnd();//读取所有
                    if (string.IsNullOrEmpty(res))
                        return "";
                    var jobj = JObject.Parse(res);
                    return jobj["result"].ToString();

                }
            }
            catch (Exception e) {
                Console.WriteLine(DateTime.Now + $"HttpPost请求失败 url:{url} postData:{postData}");
                    }
            return "";
            //定义response字符流
            //dataStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(dataStream);
            //string responseFromServer = reader.ReadToEnd();//读取所有
            //Console.WriteLine(responseFromServer);
        }
        ///// <summary>
        ///// 异步请求post（键值对形式,可等待的）
        ///// </summary>
        ///// <param name="uri">网络基址("http://localhost:59315")</param>
        ///// <param name="url">网络的地址("/api/UMeng")</param>
        ///// <param name="formData">键值对List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();formData.Add(new KeyValuePair<string, string>("userid", "29122"));formData.Add(new KeyValuePair<string, string>("umengids", "29122"));</param>
        ///// <param name="charset">编码格式</param>
        ///// <param name="mediaType">头媒体类型</param>
        ///// <returns></returns>
        //static public async Task<string> HttpPostAsync(string url, List<KeyValuePair<string, string>> formData = null, string charset = "UTF-8", string mediaType = "application/x-www-form-urlencoded")
        //{
        //    string tokenUri = url;
        //    var client = new HttpClient();
        //    client.BaseAddress = new Uri(Config.Configuration["FireApi:UrlBase"]);
        //    HttpContent content = new FormUrlEncodedContent(formData);
        //    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType);
        //    content.Headers.ContentType.CharSet = charset;
        //    for (int i = 0; i < formData.Count; i++)
        //    {
        //        content.Headers.Add(formData[i].Key, formData[i].Value);
        //    }

        //    HttpResponseMessage resp = await client.PostAsync(tokenUri, content);
        //    resp.EnsureSuccessStatusCode();
        //    string token = await resp.Content.ReadAsStringAsync();
        //    return token;
        //}
    }
}
