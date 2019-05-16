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

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/api/contacto/tomadecontacto")]
        public IHttpContext TomaDeContacto(IHttpContext context)
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

        #region POST

        [RestRoute(HttpMethod = HttpMethod.POST, PathInfo = "/api/contacto/add")]
        public IHttpContext AddContacto(IHttpContext context)
        {

            string jsonRAW = context.Request.Payload;
            LibCore.MContacto dataId = JsonConvert.DeserializeObject<LibCore.MContacto>(jsonRAW);

            LibCore.Start _ = new LibCore.Start("");
            //var hola = dataId?.nombre.Value;

            try {
                _.Contacto.CreateContacto(dataId);

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

        [RestRoute(HttpMethod = HttpMethod.PUT, PathInfo = "/api/contacto/update")]
        public IHttpContext UpdateContacto(IHttpContext context)
        {

            LibCore.Start _ = new LibCore.Start("");

            var id = context.Request.QueryString["id"] ?? "noneID?"; //Si no id dara error
            var name = context.Request.QueryString["name"] ?? "noneName?"; //Si no id dara error
            var valor = context.Request.QueryString["valor"] ?? "noneValor?"; //Si no id dara error

            _.Contacto.UpdateContacto(id, name, valor);
            context.Response.SendResponse("Updated!");
            return context;
        }


        #endregion

        #region DELETE

        [RestRoute(HttpMethod = HttpMethod.DELETE, PathInfo = "/api/contacto/delete")]
        public IHttpContext DeleteContacto(IHttpContext context)
        {

            LibCore.Start _ = new LibCore.Start("");

            var id = context.Request.QueryString["id"] ?? "noneID?"; //Si no id dara error

            _.Contacto.DeleteContacto(id);
            context.Response.SendResponse("Deleted!");
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
