using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LoadBalancer = CoreLib.Core.DB.Query.LoadBalancerQuery;

namespace CoreLib
{
    public class Start
    {
        //public CoreLib.Core.lib.IContactoComercial Contacto;
       public CoreLib.Core.lib.IContacto Contacto;

        public Start(string token) {
            // 1. Ira al load balancer y sacara los servidores activos
            // 2. Comprobará la autorizacion para usar la API
            // 3. Que usuario está detras de la peticion y rol
            // 4. devuelve la peticion.


            CoreLib.Core.DB.Models.LoadBalancerModelDB checkServer = new LoadBalancer().GetBalancerInfo();



            Contacto = new Core.lib.CContacto(checkServer);

        }

    }
}
