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
            var v = "v0.1.0.0";
            Console.Title = "APIGateway " + v;
            Console.WriteLine("     A P I   G A T E W A Y    " + v);            
            var serverStandar = new RestServer();

            var input = "";
            while ((input = Console.ReadLine()) != "q")
            {
                switch (input)
                {
                    case "start":
                        Console.WriteLine("Starting service...");
                        serverStandar.Port = "5000";
                        serverStandar.Host = "*";
                        serverStandar.Start();
                        Console.Title = "[ON] APIGateway " + v;

                        break;


                    case "start --log":
                        Console.WriteLine("Starting service...");

                        using (var server = new RestServer())
                        {
                            server.Port = "5000";
                            server.Host = "*";
                            server.LogToConsole().Start();
                            Console.Title = "[ON] APIGateway " + v;
                            Console.ReadLine();
                            server.Stop();
                        }

                        break;
                    case "stop":
                        Console.WriteLine("Stopping service...");
                        serverStandar.Stop();
                        Console.Title = "APIGateway " + v;
                        break;
                    case "--version":
                        Console.WriteLine(v);

                        break;
                    default:
                        Console.WriteLine(String.Format("Unknown command: {0}", input));
                        break;
                }

            }
        }
    }


}
