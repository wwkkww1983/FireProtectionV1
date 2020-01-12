//------------------------------------------------------------------------------
// <自动生成>
//     此代码由工具生成。
//     //
//     对此文件的更改可能导致不正确的行为，并在以下条件下丢失:
//     代码重新生成。
// </自动生成>
//------------------------------------------------------------------------------

namespace ShortMessage
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.82009668.com/", ConfigurationName="ShortMessage.ServiceSoap")]
    public interface ServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.82009668.com/changePassWord", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<int> changePassWordAsync(string username, string password, string newpwd);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.82009668.com/getBalance", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<int> getBalanceAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.82009668.com/sendMessage", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<int> sendMessageAsync(string username, string pwd, string phones, string contents, string scode, string setTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.82009668.com/sendMMS", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<ShortMessage.sendMMSResponse> sendMMSAsync(ShortMessage.sendMMSRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.82009668.com/sendMMSBase64", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<int> sendMMSBase64Async(string username, string pwd, string phones, string mmsBase64, string scode, string setTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.82009668.com/sendChat", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<int> sendChatAsync(string username, string pwd, string phones, string contents, string scode, string setTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.82009668.com/getChat", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> getChatAsync(string username, string pwd, string scode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.82009668.com/Meeting", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> MeetingAsync(string username, string pwd, string cmdStr, string ReportURL);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.82009668.com/sendFax", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<ShortMessage.sendFaxResponse> sendFaxAsync(ShortMessage.sendFaxRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.82009668.com/sendVoice", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> sendVoiceAsync(string username, string pwd, string phones, string contents, string setTime, string ReportURL);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendMMS", WrapperNamespace="http://www.82009668.com/", IsWrapped=true)]
    public partial class sendMMSRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=0)]
        public string username;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=1)]
        public string pwd;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=2)]
        public string phones;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] mmsBytes;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=4)]
        public string scode;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=5)]
        public string setTime;
        
        public sendMMSRequest()
        {
        }
        
        public sendMMSRequest(string username, string pwd, string phones, byte[] mmsBytes, string scode, string setTime)
        {
            this.username = username;
            this.pwd = pwd;
            this.phones = phones;
            this.mmsBytes = mmsBytes;
            this.scode = scode;
            this.setTime = setTime;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendMMSResponse", WrapperNamespace="http://www.82009668.com/", IsWrapped=true)]
    public partial class sendMMSResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=0)]
        public int sendMMSResult;
        
        public sendMMSResponse()
        {
        }
        
        public sendMMSResponse(int sendMMSResult)
        {
            this.sendMMSResult = sendMMSResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendFax", WrapperNamespace="http://www.82009668.com/", IsWrapped=true)]
    public partial class sendFaxRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=0)]
        public string username;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=1)]
        public string pwd;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=2)]
        public string phones;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] docBytes;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=4)]
        public string setTime;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=5)]
        public string ReportURL;
        
        public sendFaxRequest()
        {
        }
        
        public sendFaxRequest(string username, string pwd, string phones, byte[] docBytes, string setTime, string ReportURL)
        {
            this.username = username;
            this.pwd = pwd;
            this.phones = phones;
            this.docBytes = docBytes;
            this.setTime = setTime;
            this.ReportURL = ReportURL;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendFaxResponse", WrapperNamespace="http://www.82009668.com/", IsWrapped=true)]
    public partial class sendFaxResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.82009668.com/", Order=0)]
        public string sendFaxResult;
        
        public sendFaxResponse()
        {
        }
        
        public sendFaxResponse(string sendFaxResult)
        {
            this.sendFaxResult = sendFaxResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface ServiceSoapChannel : ShortMessage.ServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class ServiceSoapClient : System.ServiceModel.ClientBase<ShortMessage.ServiceSoap>, ShortMessage.ServiceSoap
    {
        
    /// <summary>
    /// 实现此分部方法，配置服务终结点。
    /// </summary>
    /// <param name="serviceEndpoint">要配置的终结点</param>
    /// <param name="clientCredentials">客户端凭据</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ServiceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(ServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), ServiceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<int> changePassWordAsync(string username, string password, string newpwd)
        {
            return base.Channel.changePassWordAsync(username, password, newpwd);
        }
        
        public System.Threading.Tasks.Task<int> getBalanceAsync(string username, string password)
        {
            return base.Channel.getBalanceAsync(username, password);
        }
        
        public System.Threading.Tasks.Task<int> sendMessageAsync(string username, string pwd, string phones, string contents, string scode, string setTime)
        {
            return base.Channel.sendMessageAsync(username, pwd, phones, contents, scode, setTime);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ShortMessage.sendMMSResponse> ShortMessage.ServiceSoap.sendMMSAsync(ShortMessage.sendMMSRequest request)
        {
            return base.Channel.sendMMSAsync(request);
        }
        
        public System.Threading.Tasks.Task<ShortMessage.sendMMSResponse> sendMMSAsync(string username, string pwd, string phones, byte[] mmsBytes, string scode, string setTime)
        {
            ShortMessage.sendMMSRequest inValue = new ShortMessage.sendMMSRequest();
            inValue.username = username;
            inValue.pwd = pwd;
            inValue.phones = phones;
            inValue.mmsBytes = mmsBytes;
            inValue.scode = scode;
            inValue.setTime = setTime;
            return ((ShortMessage.ServiceSoap)(this)).sendMMSAsync(inValue);
        }
        
        public System.Threading.Tasks.Task<int> sendMMSBase64Async(string username, string pwd, string phones, string mmsBase64, string scode, string setTime)
        {
            return base.Channel.sendMMSBase64Async(username, pwd, phones, mmsBase64, scode, setTime);
        }
        
        public System.Threading.Tasks.Task<int> sendChatAsync(string username, string pwd, string phones, string contents, string scode, string setTime)
        {
            return base.Channel.sendChatAsync(username, pwd, phones, contents, scode, setTime);
        }
        
        public System.Threading.Tasks.Task<string> getChatAsync(string username, string pwd, string scode)
        {
            return base.Channel.getChatAsync(username, pwd, scode);
        }
        
        public System.Threading.Tasks.Task<string> MeetingAsync(string username, string pwd, string cmdStr, string ReportURL)
        {
            return base.Channel.MeetingAsync(username, pwd, cmdStr, ReportURL);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ShortMessage.sendFaxResponse> ShortMessage.ServiceSoap.sendFaxAsync(ShortMessage.sendFaxRequest request)
        {
            return base.Channel.sendFaxAsync(request);
        }
        
        public System.Threading.Tasks.Task<ShortMessage.sendFaxResponse> sendFaxAsync(string username, string pwd, string phones, byte[] docBytes, string setTime, string ReportURL)
        {
            ShortMessage.sendFaxRequest inValue = new ShortMessage.sendFaxRequest();
            inValue.username = username;
            inValue.pwd = pwd;
            inValue.phones = phones;
            inValue.docBytes = docBytes;
            inValue.setTime = setTime;
            inValue.ReportURL = ReportURL;
            return ((ShortMessage.ServiceSoap)(this)).sendFaxAsync(inValue);
        }
        
        public System.Threading.Tasks.Task<string> sendVoiceAsync(string username, string pwd, string phones, string contents, string setTime, string ReportURL)
        {
            return base.Channel.sendVoiceAsync(username, pwd, phones, contents, setTime, ReportURL);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ServiceSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.ServiceSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ServiceSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://cf.82009668.com:888/SDK/Service.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.ServiceSoap12))
            {
                return new System.ServiceModel.EndpointAddress("http://cf.82009668.com:888/SDK/Service.asmx");
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            ServiceSoap,
            
            ServiceSoap12,
        }
    }
}
