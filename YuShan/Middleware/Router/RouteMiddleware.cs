using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Owin.HelloWorld.Routing;
using YuShan.Middlewares;

namespace YuShan.Middlewares
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class RouteMiddleware 
    {
        private Regex _route = null;
        private AppFunc _next = null;
        private Func<IOwinContext, Task> _action = null;

        public RouteMiddleware(AppFunc next, string route, Func<IOwinContext, Task> action)
        {
            this._route = RouteBuilder.RouteToRegex(route);
            this._next = next;
            this._action = action;
        }

        public Task Invoke(IDictionary<string, object> env)
        {

            var path = (string)env["owin.RequestPath"];

            if (path.Length > 1 && path.EndsWith("/"))
                path = path.TrimEnd('/');

            if ((string)env["owin.RequestMethod"] == "GET" && _route.IsMatch(path))
            {
                IOwinContext owinContext = new OwinContext(env);
                return _action.Invoke(owinContext);
            }
            else
            {
                return _next.Invoke(env);
            }


        }
    }
}
