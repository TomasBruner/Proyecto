using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Entrega2
{
    [Serializable]
    public class Usuario
    {
        protected string mail;
        protected string contraseña;
        protected string nombre_usuario;
        protected string tipo_usuario;
        public List<Playlist> favoritos = new List<Playlist>();
        public Usuario(string nombre_usuario, string mail, string contraseña, string tipo_usuario)
        {
            this.Mail = mail;
            this.Contraseña = contraseña;
            this.Nombre_usuario = nombre_usuario;
            this.Tipo_usuario = tipo_usuario;
        }
        public string Nombre_usuario
        {
            get => nombre_usuario; set => nombre_usuario = value;
        }
        public string Mail
        {
            get => mail; set => mail = value;
        }
        public string Contraseña
        {
            get => contraseña; set => contraseña = value;
        }
        public string Tipo_usuario
        {
            get => tipo_usuario; set => tipo_usuario = value;
        }
        public List<Playlist> Favoritos
        {
            get => favoritos; set => favoritos = value;
        }
        public delegate void VerifiedEmailEventHandler(object source, EventArgs args);
        public event VerifiedEmailEventHandler EmailVerified;
        public void OnEmailVerified(object source, EventArgs args)
        {
            EmailVerified(this, new EventArgs());
        }
        public void OnEmailSent(object source, EventArgs args)
        {
            Console.WriteLine("¿Desea revisar su correo?");
            Console.WriteLine("a)si");
            Console.WriteLine("b)no");
            string input_usuario = Console.ReadLine();
            while (true)
            {
                if (input_usuario == "a")
                {
                    EmailVerified(new object(), new EventArgs());
                    break;
                }
                else if (input_usuario == "b")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("El criterio ingresado no es válido");
                }
            }
        }
        public bool Agregar_Película(Película película)
        {
            for (int i = 0; i < Archivos.películasApp.Count; i++)
            {
                if (Archivos.películasApp[i] == película)
                {
                    return false;
                }
            }
            Archivos.películasApp.Add(película);
            return true;
        }
        public bool Agregar_Canción(Canción canción)
        {
            for (int i = 0; i < Archivos.cancionesApp.Count; i++)
            {
                if (Archivos.cancionesApp[i] == canción)
                {
                    return false;
                }
            }
            Archivos.cancionesApp.Add(canción);
            return true;
        }
        public Playlist CrearPlaylist()
        {
            Console.WriteLine("¿Qué tipo de playlist desea crear?");
            Console.WriteLine("1) De canciones");
            Console.WriteLine("2) De películas");
            string option = Console.ReadLine();
            if (option == "1")
            {
                Console.Write("Nombre de la playlist: ");
                string nombre = Console.ReadLine();
                string tipo_playlist = "canciones";
                Playlist playlists = new Playlist(nombre, tipo_playlist);
                Archivos.playlists_Canciones.Add(playlists);
                return playlists;
            }
            else if (option == "2")
            {
                Console.Write("Nombre de la playlist: ");
                string nombre = Console.ReadLine();
                string tipo_playlist = "películas";
                Playlist playlists = new Playlist(nombre, tipo_playlist);
                Archivos.playlists_Películas.Add(playlists);
                return playlists;
            }
            else
            {
                Console.WriteLine("Criterio ingresado no válido, por favor escoge uno que si lo sea");
            }
            return null;
        }
        public Usuario() { }
    }
}