using System;
using System.Net.NetworkInformation;

namespace MyRealWorld.ViewModels
{
    public class VisualBaseVM : ViewModelBase
    {

        private bool _isonline;
        public bool IsOnline { get { return _isonline; } }
        public VisualBaseVM()
        {
            _isonline = getIsOnline();
        }
        public bool getIsOnline()
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
