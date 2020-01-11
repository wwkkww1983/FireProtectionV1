using ShortMessage;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FireProtectionV1.Common.Helper
{
    public class ShotMessageHelper
    {
        public static async Task<int> SendMessage(ShortMessage shortMessage)
        {
            //创建 HTTP 绑定对象
            var binding = new BasicHttpBinding();
            //根据 WebService 的 URL 构建终端点对象
            var endpoint = new EndpointAddress(@"http://cf.82009668.com:888/SDK/Service.asmx");
            //创建调用接口的工厂，注意这里泛型只能传入接口
            var factory = new ChannelFactory<ServiceSoap>(binding, endpoint);
            //从工厂获取具体的调用实例
            var callClient = factory.CreateChannel();
            //调用具体的方法，这里是 HelloWorldAsync 方法
            return await callClient.sendMessageAsync("cf-yfcfzh", "X372QH", shortMessage.Phones, shortMessage.Contents, "", "");

            //获取结果
            //HelloWorldResponse response = responseTask.Result;
        }
    }

    public class ShortMessage
    {
        /// <summary>
        /// 接收短信的手机号，以英文逗号分隔
        /// </summary>
        public string Phones { get; set; }
        /// <summary>
        /// 短信内容，300字以内，必须是通知类短信，且必须加签名
        /// </summary>
        public string Contents { get; set; }
    }
}
