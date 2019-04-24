using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Core.lib
{
    public interface IContacto
    {
        void CreateContactos(string nombre, string apellidos, string dni, string cif, string direccion, string localidad, string provincia, string cp, string pais, string telefono1, string telefono2, string email1, string email2, string langNative, string particularEmpresa, string descripcionCaso, string fuenteCliente);
        Task<string> ReadAllContactos();
        //void ReadAllContactosComercial();
    }
}
