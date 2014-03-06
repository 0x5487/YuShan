using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Katana.Framework;
using Microsoft.Owin;

namespace YuShan.Middlewares
{
    class RouteMiddleware : OwinMiddleware
    {
        public RouteMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
          

            string verb = context.Request.Method;
            string path = context.Request.Path.ToString();
            Func<IOwinContext, Task> method;

            switch (verb)
            {
                case "GET":
                    if (Server.GetRoutes.TryGetValue(path, out method))
                    {
                        return method(context);
                    }
                    break;
                case "POST":
                    if (Server.PostRoutes.TryGetValue(path, out method))
                    {
                        return method(context);
                    }
                    break;
            }

            return Next.Invoke(context);
        }
    }
}
