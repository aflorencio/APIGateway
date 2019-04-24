using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tiny.RestClient;

using Newtonsoft.Json;

namespace CoreLib.Core.lib
{
    class CContacto : CoreLib.Core.lib.IContacto
    {


        public Core.DB.Models.LoadBalancerModelDB LoadBalancer;

        public CContacto(Core.DB.Models.LoadBalancerModelDB balancer) {
            LoadBalancer = balancer;
            //Tambien recibiremos el Auth para comprobar que usuario es siempre y que rol tiene. Para permitir si tendra la informacion o no.
        }

        public void CreateContactos(string nombre, string apellidos, string dni, string cif, string direccion, string localidad, string provincia, string cp, string pais, string telefono1, string telefono2, string email1, string email2, string langNative, string particularEmpresa, string descripcionCaso, string fuenteCliente) {

           // var gola =  balancerData.flowService.server.FirstOrDefault();
           
            var client = new TinyRestClient(new HttpClient(), LoadBalancer.ContactoService.server.url);
            var response = client.PostRequest("contacto").

                AddFormParameter("nombre", nombre == "" ? null : nombre).
                AddFormParameter("apellidos", apellidos == "" ? null : apellidos).
                AddFormParameter("dni", dni == "" ? null : dni).
                AddFormParameter("cif", cif == "" ? null : cif).
                AddFormParameter("direccion", direccion == "" ? null : direccion).
                AddFormParameter("municipio", localidad == "" ? null : localidad).
                AddFormParameter("provincia", provincia == "" ? null : provincia).
                AddFormParameter("codPostal", cp == "" ? null : cp).
                AddFormParameter("pais", pais == "" ? null : pais).

                AddFormParameter("telefono1", telefono1 == "" ? null : telefono1).
                AddFormParameter("telefono2", telefono2 == "" ? null : telefono2).
                AddFormParameter("email1", email1 == "" ? null : email1).
                AddFormParameter("email2", email2 == "" ? null : email2).

                AddFormParameter("langNative", langNative == "" ? null : langNative).
                AddFormParameter("particularEmpresa", particularEmpresa == "" ? null : particularEmpresa).
                AddFormParameter("descripcionCaso", descripcionCaso == "" ? null : descripcionCaso). 
                AddFormParameter("fuentePosibleCliente", fuenteCliente == "" ? null : fuenteCliente).
                ExecuteAsync<bool>();

            //LLAMO AL SERVICIO FLOW Y CREO UN FLOW AL CONTACTO CON LOS STATUS EN FALSE
            var idContacto = "5c9ba0766aa59e1350466315";
            var clienteFlow = new TinyRestClient(new HttpClient(), LoadBalancer.FlowService.server.url);
            var resClienteFlow = clienteFlow.PostRequest("flow/" + idContacto).ExecuteAsync<bool>();



            //CREO UN TIMELINE AL FLOW DEL TIPO CONTACTO CREADO 
           // var createTimeline = new TinyRestClient(new HttpClient(), LoadBalancer.ContactoService.server.url);
            var timeLineRest = clienteFlow.PostRequest("addToFlow/" + idContacto).
                AddFormParameter("fecha", "20190423000000").
                AddFormParameter("tipo", "Registro").
                AddFormParameter("idTipo", idContacto).
                AddFormParameter("titulo", "Contacto creado").
                AddFormParameter("mensaje", "El contacto ha sido creado").
                AddFormParameter("ticket", "00015458").
                AddFormParameter("visto", "false").
                AddFormParameter("terminado", "false").
                ExecuteAsync<bool>(); 

        }

        public async Task<string> ReadAllContactos()
        {
            var client = new TinyRestClient(new HttpClient(), LoadBalancer.ContactoService.server.url);
            var contacto = await client.GetRequest("contacto").ExecuteAsync<List<CoreLib.Core.lib.Contacto.Models.Contacto>>();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(contacto, Formatting.Indented);

            return json;
        }

    }
}
