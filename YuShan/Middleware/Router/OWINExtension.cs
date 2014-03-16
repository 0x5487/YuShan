using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using YuShan.Routing;
using YuShan.Middlewares;

namespace YuShan.Routing
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public static class OWINExtension
    {

        public static IAppBuilder Get(this IAppBuilder app, string route, Func<IOwinContext, Task<string>> action)
        {
            return app.Use(typeof(RouteMiddleware), route, action);
        }

        public static IAppBuilder Get(this IAppBuilder app, string route, Func<IOwinContext, string> action)
        {
            return app.Use(typeof(RouteMiddleware), route, action);
        }

        public static IAppBuilder Get(this IAppBuilder app, string route, Action<IOwinContext> action)
        {
            return app.Use(typeof(RouteMiddleware), route, action);
        }
    }
}
