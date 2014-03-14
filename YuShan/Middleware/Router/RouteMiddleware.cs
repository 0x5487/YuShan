using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using YuShan.Routing;
using YuShan.Middlewares;
using JasonSoft;

namespace YuShan.Middlewares
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class RouteMiddleware 
    {
        private RouteRegex _routeBuilder = null;
        private AppFunc _next = null;
        private Func<IOwinContext, Task<string>> _actionAsync = null;
        private Func<IOwinContext, string> _action = null;


        public RouteMiddleware(AppFunc next, string route, Func<IOwinContext, Task<string>> actionAsync)
        {
            this._routeBuilder = new RouteRegex(route);
            this._next = next;
            this._actionAsync = actionAsync;
        }


        public RouteMiddleware(AppFunc next, string route, Func<IOwinContext, string> action)
        {
            this._routeBuilder = new RouteRegex(route);
            this._next = next;
            this._action = action;
        }

        public Task Invoke(IDictionary<string, object> env)
        {

            var path = (string)env["owin.RequestPath"];

            if (path.Length > 1 && path.EndsWith("/"))
                path = path.TrimEnd('/');

            if ((string)env["owin.RequestMethod"] == "GET" && _routeBuilder.Validate(path))
            {                     
                
                IOwinContext owinContext = new OwinContext(env);

                if(!_routeBuilder.Parameters.IsNullOrEmpty())
                {
                    owinContext.Request.Set<IDictionary<string, string>>("param", _routeBuilder.Parameters);
                }

                string content = string.Empty;
                if (_action != null)
                {
                    content = _action.Invoke(owinContext);
                }
                else if(_actionAsync != null)
                {
                    content = _actionAsync.Invoke(owinContext).Result;
                }

                return owinContext.Response.WriteAsync(content);
            }
            else
            {
                return _next.Invoke(env);
            }


        }
    }
}
