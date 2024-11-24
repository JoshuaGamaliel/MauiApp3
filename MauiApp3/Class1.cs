using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3
{
    public class RPropuesta
    {
        public string Nombre { get; set; }
        public string Detalles { get; set; }
        public string Estado { get; set; } 
        public RPropuesta(string nombre, string detalles, string estado)
        {
            Detalles = detalles;
            Nombre = nombre;
            Estado = estado;
        }
        public override string ToString()
        {
            return $"{Nombre}-{Detalles}-{Estado}";
        }
    }
}
