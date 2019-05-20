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
            dynamic dataId = JsonConvert.DeserializeObject<object>(jsonRAW);

            string[] words = dataId?.idContactoService.ToString().Split(',');

            LibCore.Mrastreo data = new LibCore.Mrastreo();

            List<string> contactoServiceList = new List<string>();
            foreach (string word in words)
            {
                contactoServiceList.Add(word);
            }
            data.idContactoService = contactoServiceList;
            data.finalizado = dataId?.finalizado == "true" ? true : false;
            data.idTicketService = dataId?.idTicketService;
            data.keyWord = dataId?.keyWord;

            LibCore.Start _ = new LibCore.Start("");

            try
            {
                _.Rastreo.CreateRastreo(data);

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
