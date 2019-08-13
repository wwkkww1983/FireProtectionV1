using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeviceServer.Tcp
{
    public class Transfer
    {
        Dictionary<string, TransTcp> _transes = new Dictionary<string, TransTcp>();
        public void AddTransDis(string address,string ip,int port, int localport)
        {
            _transes.Add(address, new TransTcp(address,ip, port, localport));
        }
        public async Task Send(string srcAddressString, byte[] data, DeviceServer.Tcp.Session session)
        {
            var transTcp = _transes[srcAddressString];
            if (transTcp == null)
                return;
            session.IsTransfer = true;
            transTcp.DeviceSession = session;
            await transTcp.Send(data);
        }
    }
}
