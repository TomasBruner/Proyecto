using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3Spotiflix
{
    public class Búsqueda
    {
        /*public List<Canción> buscar_cancion_por_tipo()
        {
            Console.WriteLine("Seleccione el filtro por el cual desea buscar");
            Console.WriteLine("1) Título");
            Console.WriteLine("2) Género");
            Console.WriteLine("3) Compositor");
            Console.WriteLine("4) Album");
            Console.WriteLine("5) Artista");
            string option = Console.ReadLine();
            Console.Write("Escriba el nombre a buscar: ");
            string name = Console.ReadLine();
            List<Canción> resultado = new List<Canción>();

            if (option == "1")
            {
                foreach (Canción canción in Archivos.cancionesApp)
                {
                    if (canción.titulo == name)
                    {
                        resultado.Add(canción);
                    }
                }
            }
            if (option == "2")
            {
                foreach (Canción canción in Archivos.cancionesApp)
                {
                    if (canción.genero == name)
                    {
                        resultado.Add(canción);
                    }
                }
            }
            if (option == "3")
            {
                foreach (Canción canción in Archivos.cancionesApp)
                {
                    if (canción.compositor == name)
                    {
                        resultado.Add(canción);
                    }
                }
            }
            if (option == "4")
            {
                foreach (Canción canción in Archivos.cancionesApp)
                {
                    if (canción.album.Nombre == name)
                    {
                        resultado.Add(canción);
                    }
                }
            }
            if (option == "5")
            {
                foreach (Canción canción in Archivos.cancionesApp)
                {
                    if (canción.artista.Nombre == name)
                    {
                        resultado.Add(canción);
                    }
                }
            }
            return resultado;
        }

        public List<Película> buscar_pelicula_por_tipo()
        {
            Console.WriteLine("Seleccione el filtro por el cual desea buscar");
            Console.WriteLine("1) Título");
            Console.WriteLine("2) Categoría");
            Console.WriteLine("3) Director");
            Console.WriteLine("4) Actor");
            Console.WriteLine("5) Estudio");
            Console.WriteLine("6) Escritor");
            string option = Console.ReadLine();
            Console.Write("Escriba el nombre a buscar: ");
            string name = Console.ReadLine();
            List<Película> resultado = new List<Película>();

            if (option == "1")
            {
                foreach (Película película in Archivos.películasApp)
                {
                    if (película.titulo == name)
                    {
                        resultado.Add(película);
                    }
                }
            }
            if (option == "2")
            {
                foreach (Película película in Archivos.películasApp)
                {
                    if (película.categoria == name)
                    {
                        resultado.Add(película);
                    }
                }
            }
            if (option == "3")
            {
                foreach (Película película in Archivos.películasApp)
                {
                    if (película.director.Nombre == name)
                    {
                        resultado.Add(película);
                    }
                }
            }
            if (option == "4")
            {
                foreach (Película película in Archivos.películasApp)
                {
                    foreach (Person actor in película.actores)
                    {
                        if (actor.Nombre == name)
                        {
                            resultado.Add(película);
                        }
                    }
                }
            }
            if (option == "5")
            {
                foreach (Película película in Archivos.películasApp)
                {
                    if (película.estudio == name)
                    {
                        resultado.Add(película);
                    }
                }
            }
            return resultado;
        }
        public List<Playlist> buscar_playlist_por_tipo()
        {
            Console.WriteLine("Seleccione el filtro por el cual desea buscar");
            Console.WriteLine("1) Nombre");
            Console.WriteLine("2) Tipo de playlist");
            string option = Console.ReadLine();
            List<Playlist> resultado2 = new List<Playlist>();

            if (option == "1")
            {
                Console.Write("Escriba el nombre de playlist: ");
                string nombre_playlist = Console.ReadLine();
                foreach (Playlist playlist in Archivos.playlists_Canciones)
                {
                    if (playlist.Nombre == nombre_playlist)
                    {
                        resultado2.Add(playlist);
                    }
                }
                foreach (Playlist playlist in Archivos.playlists_Películas)
                {
                    if (playlist.Nombre == nombre_playlist)
                    {
                        resultado2.Add(playlist);
                    }
                }
            }
            else if (option == "2")
            {
                Console.Write("Escriba el tipo de playlist: ");
                Console.WriteLine("1) De Películas");
                Console.WriteLine("2) De Canciones");
                if (option == "1")
                {
                    Console.WriteLine(Archivos.playlists_Películas);
                }
                else if (option == "2")
                {
                    Console.WriteLine(Archivos.playlists_Canciones);
                }
            }
            else
            {
                Console.WriteLine("Criterio ingresado no válido");
            }
            return resultado2;
        }*/
    }
}

