using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Katana.Controllers;
using Katana.Framework;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Owin;
using YuShan.Middlewares;
using YuShan.Session.Owin;

namespace YuShan
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
#if DEBUG
            app.UseErrorPage();
#endif


            //app.Use(typeof (InStoreSessionMiddleware));

            new Home();
            app.Use(typeof (WebMiddleware));
            app.Use(typeof(NotFoundMiddleware));

        }
    }
}
