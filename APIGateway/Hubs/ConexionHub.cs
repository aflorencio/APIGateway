using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace SignalRChat
{
    public class ConexionHub : Hub
    {
        #region CONTACTO
        public void Send(string nombre, string apellidos, string dni, string cif, string direccion, string localidad, string provincia, string cp, string pais, string telefono1, string telefono2, string email1, string email2, string langNative, string particularEmpresa, string descripcionCaso, string fuenteCliente) 
        {
            CoreLib.Start _ = new CoreLib.Start("");

            _.Contacto.CreateContactos(nombre,apellidos,dni,cif,direccion,localidad,provincia,cp,pais, telefono1, telefono2, email1, email2, langNative, particularEmpresa, descripcionCaso, fuenteCliente);
            

        }

        public async Task readcontactos() {

            CoreLib.Start _ = new CoreLib.Start("");

            string json  = await _.Contacto.ReadAllContactos();

            Clients.All.addNewContactoToPage(json);
        }
        #endregion
    }
}