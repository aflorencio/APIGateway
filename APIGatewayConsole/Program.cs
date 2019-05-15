using Grapevine.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGatewayConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "APIGateway v0.1.0.0";
            Console.WriteLine("     A P I   G A T E W A Y   v0.1.0.0 ");
            Console.ReadLine();

            using (var server = new RestServer())
            {
                server.Port = "5000";
                server.Host = "*";
                server.LogToConsole().Start();
                Console.Title = "[ON] APIGateway v0.1.0.0";
                Console.ReadLine();
                server.Stop();
                System.GC.Collect();
                
            }
        Console.ReadLine();
        }
    }


}
