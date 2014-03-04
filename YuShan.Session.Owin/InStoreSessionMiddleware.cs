using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace YuShan.Session.Owin
{
    public class InStoreSessionMiddleware: OwinMiddleware
    {

        private IDictionary<string, IDictionary<string, object>> _sessionStore; 

        public InStoreSessionMiddleware(OwinMiddleware next) : base(next)
        {
            _sessionStore = new Dictionary<string, IDictionary<string, object>>();
        }

        public override Task Invoke(IOwinContext context)
        {
            string sessionKey = null;
            foreach (var cookie in context.Request.Cookies)
            {
                if (cookie.Key == "YuShan.SessionId")
                {
                    sessionKey = cookie.Value;
                }
            }
            

            IDictionary<string, object> session = null;
            if (!string.IsNullOrWhiteSpace(sessionKey))
            {
                _sessionStore.TryGetValue(sessionKey, out session);
            }
            else
            {
                sessionKey = Guid.NewGuid().ToString();
                context.Response.Cookies.Append("YuShan.SessionId", sessionKey);
            }

            object sessionObject = null;
            if (context.Environment.TryGetValue("YuShan.Session", out sessionObject))
            {
                session = sessionObject as IDictionary<string, object>;
            }
            else
            {
                if (session == null)
                {
                    session = new Dictionary<string, object>();
                }

                _sessionStore[sessionKey] = session;
                context.Environment.Add("YuShan.Session", session);
            }



            return Next.Invoke(context);
        }
    }
}
