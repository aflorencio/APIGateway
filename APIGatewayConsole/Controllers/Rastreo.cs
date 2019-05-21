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

namespace APIGatewayConsole.Controllers
{
    [RestResource]
    class Rastreo
    {

        #region GET

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/api/rastreo/all")]
        public IHttpContext ReadAllContacto(IHttpContext context)
        {

            var runer = Task.Run(() =>
            {
                LibCore.Start _ = new LibCore.Start("");
                var dataRun = _.Rastreo.ReadAllRastreo();
                return JsonConvert.SerializeObject(dataRun, Formatting.Indented);
            });

            runer.Wait();
            runer.Dispose();

            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(runer.Result);
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/api/rastreo/one")]
        public IHttpContext ReadOneContacto(IHttpContext context)
        {

            var runner = Task.Run(() => {
                LibCore.Start _ = new LibCore.Start("");
                var id = context.Request.QueryString["id"] ?? "noneid?"; //Si no id dara error
                var dataRun = _.Rastreo.ReadOneRastreo(id);
                return JsonConvert.SerializeObject(dataRun, Formatting.Indented);
            });

            runner.Wait();
            runner.Dispose();

            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(runner.Result);
            return context;
        }


        #endregion

        #region POST

        [RestRoute(HttpMethod = HttpMethod.POST, PathInfo = "/api/rastreo/addLink")]
        public IHttpContext AddContacto(IHttpContext context)
        {
            var id = context.Request.QueryString["id"] ?? "what?";
            string jsonRAW = context.Request.Payload;
            LibCore.Link dataId = JsonConvert.DeserializeObject<LibCore.Link>(jsonRAW);

            LibCore.Start _ = new LibCore.Start("");
            //var hola = dataId?.nombre.Value;

            try {
                _.Rastreo.CreateLinkRastreo(dataId, id);

            }
            catch (Exception e)
            {
                return null;
            }


            //context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse("OK!");
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.POST, PathInfo = "/api/rastreo/add")]
        public IHttpContext AddRastreo(IHttpContext context)
        {
            string jsonRAW = context.Request.Payload;
            LibCore.Mrastreo dataId = JsonConvert.DeserializeObject<LibCore.Mrastreo>(jsonRAW);

            LibCore.Start _ = new LibCore.Start("");

            try
            {

                _.Rastreo.CreateRastreo(dataId);

            }
            catch (Exception e)
            {
                return null;
            }


            //context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse("OK!");
            return context;
        }

        #endregion

        #region PUT

        [RestRoute(HttpMethod = HttpMethod.PUT, PathInfo = "/api/rastreo/update")]
        public IHttpContext UpdateContacto(IHttpContext context)
        {

            LibCore.Start _ = new LibCore.Start("");

            var id = context.Request.QueryString["id"] ?? "noneID?"; //Si no id dara error
            var name = context.Request.QueryString["name"] ?? "noneName?"; //Si no id dara error
            var valor = context.Request.QueryString["value"] ?? "noneValor?"; //Si no id dara error

            _.Rastreo.UpdateRastreo(id, name, valor);
            context.Response.SendResponse("Updated!");
            return context;
        }


        #endregion

        #region DELETE

        [RestRoute(HttpMethod = HttpMethod.DELETE, PathInfo = "/api/rastreo/delete")]
        public IHttpContext DeleteRastreo(IHttpContext context)
        {
            LibCore.Start _ = new LibCore.Start("");
            var id = context.Request.QueryString["id"] ?? "what?";
            try
            {

                _.Rastreo.DeleteRastreo(id);

            }
            catch (Exception e)
            {
                return null;
            }


            //context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse("OK!");
            return context;
        }


        #endregion
    }
}
