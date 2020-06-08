using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
//using WMPLib;
using System.IO;
namespace Entrega2
{
    public class Canción
    {
        public Album album;
        public Artista artista;
        public string titulo;
        public string genero;
        public string estudio;
        public string letra;
        public string premios;
        public string compositor;
        public List<int> calificacion;
        public int Avg_calificacion;
        public int añoPublicacion;
        public int numReproductions;
        //public static WindowsMediaPlayer player = new WindowsMediaPlayer();
        public string Music;
        public int min;
        public void informacion()
        {
            Console.WriteLine("Título" + titulo + "\nArtista: " + artista.Nombre + "\nEstudio: " + estudio + "\nCompositor: " + compositor + "\nCalificación: " + Avg_calificacion + "\nAlbum: " + album.Nombre + "\nGenero: " + genero + "\nNúmero de reproducciones: " + numReproductions + "\nAño de publicación: " + añoPublicacion);
        }
        public Canción(string titulo, Artista artista, Album album, string genero, string estudio, string compositor, int añoPublicacion, int numReproductions, int Avg_calificacion, string Music, int min)
        {
            this.titulo = titulo;
            this.artista = artista;
            this.album = album;
            this.genero = genero;
            this.estudio = estudio;
            this.compositor = compositor;
            this.añoPublicacion = añoPublicacion;
            this.numReproductions = numReproductions;
            this.Avg_calificacion = Avg_calificacion;
            this.numReproductions = numReproductions;
            this.Music = Music;
            this.min = min;
            this.Music = Music;
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
            var D = carpeta + this.Music;
            //player.URL = D;
            numReproductions += 1;
            //player.controls.play();
            Console.WriteLine("Se está reproduciendo " + titulo);
            Console.WriteLine("Presione enter si desea parar la reproducción");
            Console.ReadLine();
            //player.controls.stop();
        }
        
    }

}
