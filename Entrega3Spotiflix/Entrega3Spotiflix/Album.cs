using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entrega3Spotiflix
{
    public class Album
    {
        public int Año;
        public string Nombre;
        public Artista Artista;
        public string Imagen;
        //falta foto
        public Album(string Imagen, string nombre, Artista artista, int año)
        {
            this.Nombre = nombre;
            this.Artista = artista;
            this.Año = año;
            this.Imagen = Imagen;
        }
        public string Nombre_get
        {
            get => Nombre; set => Nombre = value;
        }
        public Artista Artista_get
        {
            get => Artista; set => Artista = value;
        }
        public int Año_get
        {
            get => Año; set => Año = value;
        }
    }

}
