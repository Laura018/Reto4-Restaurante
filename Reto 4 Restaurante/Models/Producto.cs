using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reto_4_Restaurante.Models
{
    public class Producto
    {
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int precio { get; set; }
        public string imagen_producto { get; set; }
    }
}
