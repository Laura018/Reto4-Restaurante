using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reto_4_Restaurante.Models
{
    public class Servicios
    {
  
        public int id_servicios { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public string imagen_servicios { get; set; }
    }
}

