using Microsoft.AspNetCore.Http;
using MyRealWorld.Common;
using System.Net.NetworkInformation;

namespace MyRealWorld.Models
{
    public class BaseModel
    {

        public bool IsOnline { get; protected set; }
        protected int _Index { get; }

        public BaseModel()
        {
            _Index = Constants.Values.Counter;
            
        }
        protected ISession _session { get; set; }
        public void SetSession(ISession session)
        {
            _session = session;
            
        }
        protected void SetContextValuesAsync(ISession session)
        {
            SetSession(session);
            //await SetOnlineStatusAsync();
        }
        protected bool getIsOnline()
        {
            try
            {
                Ping myPing = new Ping();
                string host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
