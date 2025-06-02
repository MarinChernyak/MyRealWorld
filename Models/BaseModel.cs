using Microsoft.AspNetCore.Http;
using MyRealWorld.Common;

namespace MyRealWorld.Models
{
    public class BaseModel
    {
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

    }
}
