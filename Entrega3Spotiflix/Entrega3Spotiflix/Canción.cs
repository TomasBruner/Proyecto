using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
//using WMPLib;
using System.IO;
namespace Entrega3Spotiflix
{
    [Serializable]
    public class Canción
    {
        public Album album;
        public Artista artista;
        public string titulo;
        public List<string> genero;
        public string estudio;
        public string letra;
        public string premios;
        public List<int> calificación;
        public int Avg_calificacion;
        public int añoPublicacion;
        public int reproducciones;
        //public static WindowsMediaPlayer player = new WindowsMediaPlayer();
        public int min;
        public int duración;
        public string resolución;
        public string espacio;
        public string Url;
        public bool Agregada;
        public void informacion()
        {
            Console.WriteLine("Título" + titulo + "\nArtista: " + artista.Nombre + "\nCalificación: " + Avg_calificacion + "\nAlbum: " + album.Nombre + "\nGenero: " + genero + "\nNúmero de reproducciones: " + reproducciones + "\nAño de publicación: " + añoPublicacion);
        }
        public Canción(string titulo, Artista artista, Album album, List<string>genero, int añoPublicacion, int reproducciones, int Avg_calificacion, int duración, string resolución, string espacio, string Url)
        {
            this.titulo = titulo;
            this.artista = artista;
            this.album = album;
            this.genero = genero;
            this.añoPublicacion = añoPublicacion;
            this.reproducciones = reproducciones;
            this.Avg_calificacion = Avg_calificacion;
            this.duración = duración;
            this.resolución = resolución;
            this.espacio = espacio;
            this.Url = Url;
        }
        public string Titulo
        {
            get => titulo; set => titulo = value;
        }

        public string discografia
        {
            get => discografia; set => discografia = value;
        }
        public string Estudio
        {
            get => estudio; set => estudio = value;
        }

        public string cantante
        {
            get => cantante; set => cantante = value;
        }

        public void Play()
        {
            var carpeta = Directory.GetCurrentDirectory();
            //var D = carpeta + this.Music;
            //player.URL = D;
            reproducciones += 1;
            //player.controls.play();
            Console.WriteLine("Se está reproduciendo " + titulo);
            Console.WriteLine("Presione enter si desea parar la reproducción");
            Console.ReadLine();
            //player.controls.stop();
        }

    }

}
