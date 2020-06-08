using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega2
{
    public class Playlist
    {
        public string Nombre;
        public List<Canción> playlist_Canciones = new List<Canción>();
        public List<Película> playlist_Películas = new List<Película>();
        public string Tipo_playlist;
        public Playlist(string nombre, string tipo_playlist)
        {
            this.Nombre = nombre;
            this.Tipo_playlist = tipo_playlist;
        }
        public void Agregar_películas(Película película)
        {
            if (Tipo_playlist == "película")
            {
                playlist_Películas.Add(película);
            }
        }
        public void Agregar_canciones(Canción canción)
        {
            if (Tipo_playlist == "canción")
            {
                playlist_Canciones.Add(canción);
            }
        }
        public void VerPlaylist()
        {
            Playlist playlist = new Playlist(this.Nombre, this.Tipo_playlist);
            Console.WriteLine("Nombre Playlist:" + playlist.Nombre);
            if (this.Tipo_playlist == "canciones")
            {
                foreach (Canción canción in playlist.playlist_Canciones)
                {
                    Console.WriteLine("-" + canción.titulo);
                }
            }
            else if (this.Tipo_playlist == "películas")
            {
                foreach (Película película in playlist.playlist_Películas)
                {
                    Console.WriteLine("-" + película.titulo);
                }
            }

        }

    }
}
