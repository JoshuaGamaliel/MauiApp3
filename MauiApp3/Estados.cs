using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3
{
    public class Estados
    {
        public string NombreEstado { get; set; }
        public string NivelAcademico { get; set; }
        public Estados(string estadonombre, string nivelacademico) 
        {
            NombreEstado= estadonombre;
            NivelAcademico= nivelacademico;

        }
        public override string ToString()
        {
            return $"{NombreEstado}-{NivelAcademico}";
        }
    }
}
