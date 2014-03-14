using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace YuShan
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:3000"))
            {
                Console.WriteLine("listen on port:3000");
                Console.ReadLine();
            }
        }
    }
}
