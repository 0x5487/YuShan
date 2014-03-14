using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Owin.HelloWorld.ViewEngine;
using RazorEngine;

namespace YuShan.ViewEngine
{
    public static class ViewEngineExtensions
    {
        public static IAppBuilder DefaultViewEngine<TViewEngine>(this IAppBuilder builder)
            where TViewEngine : IViewEngine, new()
        {
            return builder.DefaultViewEngine(() => new TViewEngine());
        }

        public static IAppBuilder DefaultViewEngine(this IAppBuilder builder, Func<IViewEngine> viewEngine)
        {
            ViewEngineActivator.DefaultViewEngine = viewEngine();
            return builder.RegisterViewEngine(viewEngine, "defaultViewEngine");
        }

        public static IAppBuilder RegisterViewEngine(this IAppBuilder builder, Func<IViewEngine> viewEngine, string viewEngineId)
        {
            ViewEngineActivator.RegisterViewEngine(viewEngineId, viewEngine);
            return builder;
        }

        public static string Render(this IOwinContext ctx, string view)
        {
            var output = ViewEngineActivator.DefaultViewEngine.Parse(view);
            return output;
        }

        public static string Render(this IOwinContext ctx, string viewName, object model)
        {
            return ViewEngineActivator.DefaultViewEngine.Parse(viewName, model);
        }

        //public static void View(this Response res, string view, string viewEngineId)
        //{
        //    var viewEngine = ViewEngineActivator.ResolveViewEngine(viewEngineId);
        //    var output = viewEngine.Parse(view);

        //    res.ContentType = "text/html";
        //    res.Status = "200 OK";
        //    res.End(output);
        //}

        public static string Render<T>(this IOwinContext ctx, string view, T model)
        {
            return ViewEngineActivator.DefaultViewEngine.Parse(view, model);
        }

        //public static void View<T>(this Response res, string view, T model, string viewEngineId)
        //{
        //    var viewEngine = ViewEngineActivator.ResolveViewEngine(viewEngineId);
        //    var output = viewEngine.Parse(view);

        //    res.ContentType = "text/html";
        //    res.Status = "200 OK";
        //    res.End(output);
        //}
    }
}
