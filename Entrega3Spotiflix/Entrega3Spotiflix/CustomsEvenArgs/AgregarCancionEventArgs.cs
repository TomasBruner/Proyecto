using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3Spotiflix.CustomsEvenArgs
{
    public class AgregarCancionEventArgs : EventArgs
    {
        public string Nombre { get; set; }
        public string Artista { get; set; }
        public string artista { get; set; }
        public string Album { get; set; }
        public string Duracion { get; set; }
        public string genero { get; set; }
        public string Espacio { get; set; }
        public string Resolucion { get; set; }
        public string URL { get; set; }
        public string añoPublicacion { get; set; }
        public int Reproducciones { get; set; }
        public int Calificacion { get; set; }
        public int avg_calificacion{ get; set; }
    }
}
