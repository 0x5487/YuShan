using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Owin.HelloWorld.Routing;
using YuShan.Middlewares;

namespace YuShan.Routing
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public static class Routing
    {

        public static IAppBuilder Get(this IAppBuilder app, string route, Func<IOwinContext, Task> task)
        {
            var regex = RouteBuilder.RouteToRegex(route);

            return app.Use(new Func<AppFunc, AppFunc>(next => ( env =>
            {

                var path = (string)env["owin.RequestPath"];

                //if (path.EndsWith("/"))
                //    path = path.TrimEnd('/');

                if ((string)env["owin.RequestMethod"] == "GET" && regex.IsMatch(path))
                {
                    IOwinContext owinContext = new OwinContext(env);
                    return task.Invoke(owinContext);
                }
                else
                {
                    return next.Invoke(env);
                }
            })));
        }
    }
}
