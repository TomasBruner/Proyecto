using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3Spotiflix
{
    static class Archivos
    {

        public static List<Usuario> Usuarios = new List<Usuario>();
        public static Dictionary<int, List<string>> lista_usuarios = new Dictionary<int, List<string>>();
        public static List<Película> películasApp = new List<Película>();
        public static List<Canción> cancionesApp = new List<Canción>();
        public static List<Playlist> playlists_Canciones = new List<Playlist>();
        public static List<Playlist> playlists_Películas = new List<Playlist>();
        private static List<Person> personasApp = new List<Person>();
        public static List<Person> PersonasApp
        {
            get => personasApp; set => personasApp = value;
        }
        public static Dictionary<int, List<string>> Lista_usuarios
        {
            get => lista_usuarios; set => lista_usuarios = value;
        }
        public static void ChangePassword(string usr, string newpsswd)
        {
            foreach (List<string> user in Lista_usuarios.Values)
            {
                if (user[0] == usr)
                {
                    user[2] = newpsswd;
                }
            }
        }

        // Metodo para agregar un nuevo usuario, verificando ademas que no exista
        public static string AddUser(List<string> data)
        {
            string description = null;
            foreach (List<string> value in Lista_usuarios.Values)
            {
                if (data[0] == value[0])
                {
                    description = "El nombre de usuario especificado ya existe";
                }
                else if (data[1] == value[1])
                {
                    description = "El correo ingresado ya existe";
                }
            }
            if (description == null)
            {
                Lista_usuarios.Add(Lista_usuarios.Count + 1, data);
                Usuario premium = new Usuario(data[0], data[1], data[2], data[3]); 
                Usuarios.Add(premium);
            }
            return description;
        }

        // Metodo para obtener los datos de usr
        public static List<string> GetData(string usr)
        {
            foreach (List<string> user in Lista_usuarios.Values)
            {
                if (user[0] == usr)
                {
                    return user;
                }
            }

            return new List<string>();
        }

        // Metodo para realizar el LogIn
        public static string LogIn(string usrname, string password)
        {
            string description = null;
            foreach (List<string> user in Lista_usuarios.Values)
            {
                if (user[0] == usrname && user[2] == password)
                {
                    return description;
                }
            }
            return "Usuario o contrasena incorrecta";
        }


    }
}

