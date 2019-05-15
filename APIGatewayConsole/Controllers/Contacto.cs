using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGatewayConsole
{
    [RestResource]
    public class TestResource
    {

        #region GET
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/api/contacto/all")]
        public IHttpContext ReadAllContacto(IHttpContext context)
        {
            
            var runer = Task.Run(() =>
            {
                LibCore.Start _ = new LibCore.Start("");
                var dataRun = _.Contacto.ReadAllContacto();
                return JsonConvert.SerializeObject(dataRun, Formatting.Indented);
            });

            runer.Wait();
            runer.Dispose();

            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(runer.Result);
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/api/contacto/one")]
        public IHttpContext ReadOneContacto(IHttpContext context)
        {

            var runner = Task.Run(() => {
                LibCore.Start _ = new LibCore.Start("");
                var id = context.Request.QueryString["id"] ?? "noneid?"; //Si no id dara error
                var dataRun = _.Contacto.ReadOneContacto(id);
                return JsonConvert.SerializeObject(dataRun, Formatting.Indented);
            });

            runner.Wait();
            runner.Dispose();

            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(runner.Result);
            return context;
        }
        #endregion

        [RestRoute]
        public IHttpContext Default(IHttpContext context)
        {
            context.Response.SendResponse("APIGateway.");
            return context;
        }
    }
}
