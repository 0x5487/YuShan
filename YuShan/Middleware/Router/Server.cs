using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Katana.Framework
{
    public static class Server
    {
        public static ConcurrentDictionary<string, Func<IOwinContext, Task>> GetRoutes { get; private set; }
        public static ConcurrentDictionary<string, Func<IOwinContext, Task>> PostRoutes { get; private set; }

        static Server()
        {
            GetRoutes = new ConcurrentDictionary<string, Func<IOwinContext, Task>>();
            PostRoutes = new ConcurrentDictionary<string, Func<IOwinContext, Task>>();
        }


        public static void Get(string path, Func<IOwinContext, Task> action)
        {
            GetRoutes.TryAdd(path, action);

        }

        public static void Post(string path, Func<IOwinContext, Task> action)
        {
            PostRoutes.TryAdd(path, action);
        }


    }
}
