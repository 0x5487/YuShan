using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace YuShan.Middlewares
{
    class NotFoundMiddleware : OwinMiddleware
    {
        public NotFoundMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("page not found");
        }
    }
}
