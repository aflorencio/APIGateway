using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIGateway.Controllers
{
    public class ContactoController : ApiController
    {
        #region GET

        // GET: api/Contacto
        [HttpGet]
        [Route("api/contacto")]
        public async Task<List<LibCore.MContacto>> Get()
        {
            LibCore.Start _ = new LibCore.Start("");

            var data = await _.Contacto.ReadAllContacto();
            return data;
        }

        // GET: api/Contacto/5
        [HttpGet]
        [Route("api/contacto/{id}")]
        public async Task<LibCore.MContacto> Get(string id)
        {
            LibCore.Start _ = new LibCore.Start("");

            var data = await _.Contacto.ReadOneContacto(id);
            return data;

        }

        #endregion

        // POST: api/Contacto
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Contacto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Contacto/5
        public void Delete(int id)
        {
        }
    }
}
