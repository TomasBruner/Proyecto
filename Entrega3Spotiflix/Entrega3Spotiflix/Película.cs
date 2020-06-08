using System;
//using WMPLib;
using System.Media;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3Spotiflix
{
    [Serializable]
    public class Película
    {
        public string titulo;
        public List<string> categoria;
        public string estudio;
        public string descripcion;
        public string premios;
        public int duracion;
        public int min;
        public Person director;
        public int añoPublicacion;
        public int rating;
        public List<int> Rating;
        public int avg_Ranking;
        public List<Person> Actores;
        public int reproducciones;
        public string Imagen;
        public int añoPublicación;
        public string Url;
        //public static WindowsMediaPlayer player = new WindowsMediaPlayer();
        public string peli;
        public Película(string titulo, List<string> Categoria, Person director, string descripcion, int duracion, int añoPublicacion, int clasificacion, List<double> rating, int Avg_Ranking, string Imagen, string Url, int reproducciones)
        {
            this.Titulo = titulo;
            this.director = director;
            this.Premios = premios;
            this.Descripcion = descripcion;
            this.duracion = duracion;
            this.AñoPublicacion = añoPublicacion;
            this.Avg_Ranking = avg_Ranking;
            this.Imagen = Imagen;
            this.Url = Url;
            this.reproducciones = reproducciones;
        }
        public List<Person> actores
        {
            get => Actores; set => Actores = value;
        }
        public string Titulo
        {
            get => titulo; set => titulo = value;
        }
        public List<string> Categoria
        {
            get => Categoria; set => Categoria = value;
        }
        public string Estudio
        {
            get => estudio; set => estudio = value;
        }
        public string Premios
        {
            get => premios; set => premios = value;
        }
        public string Descripcion
        {
            get => descripcion; set => descripcion = value;
        }
        public int Duracion
        {
            get => duracion; set => duracion = value;
        }
        public int AñoPublicacion
        {
            get => añoPublicacion; set => añoPublicacion = value;
        }
        public int Avg_Ranking
        {
            get => Avg_Ranking; set => Avg_Ranking = value;
        }
        public void informacion()
        {
            Console.WriteLine("Título" + titulo + "\nArtista: " + "\nEstudio: " + estudio + "\nDuración: " + duracion + "\nCalificación: " + Avg_Ranking + "\nAño de publicación: " + añoPublicacion);
            Console.WriteLine("Actores: ");
            foreach (Person actores in Actores)
            {
                Console.WriteLine(actores.Nombre);
            }
        }

    }

}
