using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reto_4_Restaurante.Models
{
    public class Contacto
    {
       
        public int id_contacto { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public int telefono { get; set; }
        public string asunto { get; set; }
        public string mensaje { get; set; }
    }
}
