using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Adoptar.Models
{
    public class Ficha
    {
        public long ID { get; set; }
        public string Fecha { get; set; }
        public string Nombre { get; set; }
        public string Texto { get; set; }
        public string Imagen { get; set; }
        public Usuario Autor { get; set; }
        public string Ubicacion { get; set; }
        public string Edad { get; set; }
        public string Sexo { get; set; }
        public string Tipo { get; set; }
    }
}