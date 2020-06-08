using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entrega3Spotiflix.CustomsEvenArgs;

namespace Entrega3Spotiflix
{
    public class App
    {
        public List<Canción> top_Canciones()
        {
            List<Canción> ranking = new List<Canción>();
            List<Canción> canciones = new List<Canción>();
            foreach (Canción canción in Archivos.cancionesApp)
            {
                canciones.Add(canción);
            }
            var canciones_ordenadas = canciones.OrderByDescending(canción => canción.calificacion).ToList();
            for (int i = 0; i < 10; i++)
            {
                ranking.Add(canciones_ordenadas[i]);
            }
            return ranking;
        }

        public void AddQueue(Canción canciones)
        {
            List<Canción> cola = new List<Canción>();
            cola.Add(canciones);
        }
        public void AddQueue2(Película película)
        {
            List<Película> cola = new List<Película>();
            cola.Add(película);
        }
        public List<Película> top_Películas()
        {
            List<Película> ranking = new List<Película>();
            List<Película> películas = new List<Película>();
            foreach (Película película in Archivos.películasApp)
            {
                películas.Add(película);
            }
            var películas_ordenadas = películas.OrderByDescending(película => película.Rating).ToList();
            for (int i = 0; i < 10; i++)
            {
                ranking.Add(películas_ordenadas[i]);
            }
            return ranking;

        }

        public delegate void RegisterEventHandler(object source, RegisterEventArgs args);
        public event RegisterEventHandler Registered;

        protected virtual void OnRegistered(string username, string password, string email)
        {
            // Verifica si hay alguien suscrito al evento
            if (Registered != null)
            {
                // Engatilla el evento
                Registered(this, new RegisterEventArgs() { Password = password, Username = username, Email = email });
            }
        }

        // Paso 1: Creamos el delegate para el evento del cambio de contrasena
        public delegate void ChangePasswordEventHandler(object source, ChangePasswordEventArgs args);
        // Paso 2: Creamos el evento que se engatilla cuando se cambia la contrasena
        public event ChangePasswordEventHandler PasswordChanged;
        // Paso 3: Publicamos el evento. Notar que cuando se quiere engatillar el evento, se llama OnPasswordChanged(). 
        // Por definicion, debe ser protected virtual. Los parametros que recibe son los necesarios para crear una instancia
        // de la clase ChangePasswordEventArgs
        protected virtual void OnPasswordChanged(string username, string email)
        {
            if (PasswordChanged != null)
            {
                PasswordChanged(this, new ChangePasswordEventArgs() { Username = username, Email = email });
            }
        }
        public bool Register()
        {
            // Pedimos todos los datos necesarios
            Console.Write("Bienvenido! Ingrese sus datos de registro\nUsuario: ");
            string usr = Console.ReadLine();
            Console.Write("Correo: ");
            string email = Console.ReadLine();
            Console.Write("Contraseña: ");
            string psswd = Console.ReadLine();

            Console.WriteLine("¿Qué tipo de usuario quieres crear?");
            Console.WriteLine("1) Gratis");
            Console.WriteLine("2) Premium");
            string input = Console.ReadLine();
            string tipo_usuario;
            if (input == "1")
            {
                tipo_usuario = "Gratis";
            }
            else if (input == "2")
            {
                tipo_usuario = "Premium";
            }
            else
            {
                Console.WriteLine("El criterio ingresado no es válido");
                return false;
            }

            // Genera el link de verificacion para el usuario
            string verificationLink = GenerateLink(usr);
            // Intenta agregar el usuario a la bdd. Si retorna null, se registro correctamente,
            // sino, retorna un string de error, que es el que se muestra al usuario
            string result = Archivos.AddUser(new List<string>()
                {usr, email, psswd, verificationLink, Convert.ToString(DateTime.Now), tipo_usuario});
            if (result == null)
            {
                // Disparamos el evento

                OnRegistered(usr, psswd, email: email);
                OnEmailSent(new Object(), new EventArgs());
                return true;
            }
            else
            {
                // Mostramos el error
                Console.WriteLine("[!] ERROR: " + result + "\n");
                return false;
            }
        }

        // Realiza el cambio de contrasena
        public void ChangePassword()
        {
            // Pedimos todos los datos necesarios
            Console.WriteLine("Ingresa tu nombre de usuario: ");
            string usr = Console.ReadLine();
            Console.WriteLine("Ingresa tu contrasena: ");
            string pswd = Console.ReadLine();
            // Intenta realizar el login, si retorna null se logeo correctamente,
            // sino, retorna un string de error que se le muestra al usuario
            string result = Archivos.LogIn(usr, pswd);
            if (result == null)
            {
                // Pedimos y cambiamos la contrasena
                Console.Write("Ingrese la nueva contrasena: ");
                string newPsswd = Console.ReadLine();
                Archivos.ChangePassword(usr, newPsswd);
                // Obtenemos los datos del usuario que se logueo y disparamos el evento con los datos necesarios
                List<string> data = Archivos.GetData(usr);
                OnPasswordChanged(data[0], data[1]);
            }
            else
            {
                // Mostramos el error
                Console.WriteLine("[!] ERROR: " + result + "\n");
            }
        }

        // Metodo que genera un link de verificacion, dado un usuario
        private string GenerateLink(string usr)
        {
            Random rnd = new Random();
            string result = "";
            for (int ctr = 0; ctr <= 99; ctr++)
            {
                char rndom = (char)rnd.Next(33, 126);
                result += rndom;
            }
            return "http://spotiflix.com/verificar-correo.php?=" + usr + "_" + result;
        }
        public void OnEmailVerified(object source, EventArgs args)
        {
            Thread.Sleep(2000);
            Console.WriteLine("Se verificó correctamente el correo");
            Thread.Sleep(2000);
        }
        public void OnEmailSent(object source, EventArgs args)
        {

        }
        public void Rankear(string nombre, List<int> rate, int pelicula_o_cancion)
        {
            if (pelicula_o_cancion == 2)
            {
                foreach (Canción canción in Archivos.cancionesApp)
                {
                    if (nombre == canción.titulo)
                    {
                        int sum = rate.Sum();
                        int value = (sum / rate.Count());
                        canción.Avg_calificacion = value;
                    }
                }
            }
            else if (pelicula_o_cancion == 1)
            {
                foreach (Película película in Archivos.películasApp)
                {
                    if (nombre == película.titulo)
                    {
                        int sum = rate.Sum();
                        int valor = (sum / rate.Count());
                        película.Avg_Ranking = valor;
                    }
                }
            }

            else
            {
                Console.WriteLine("Criterio ingresado no válido");
            }
        }
        public void Ver_nombre_canciones(Usuario usuario, App app, List<Canción> canción)
        {
            Console.WriteLine("Canciones: ");
            for (int i = 0; i < canción.Count; i++)
            {
                Console.WriteLine(canción[i].titulo);
            }
            Console.WriteLine("¿Quiere elegir una?");
            Console.WriteLine("1) si");
            Console.WriteLine("2) no");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                Elegir_canción(usuario, app, canción);
            }
            else if (input == 2)
            {
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Criterio ingresado no válido");
            }
        }
        public void Ver_nombre_películas(Usuario usuario, App app, List<Película> película)
        {
            Console.WriteLine("Películas");
            for (int i = 0; i < película.Count; i++)
            {
                Console.WriteLine(película[i].titulo);
            }
            Console.WriteLine("¿Quiere elegir una?");
            Console.WriteLine("1) si");
            Console.WriteLine("2) no");
            int input1 = Convert.ToInt32(Console.ReadLine());
            if (input1 == 1)
            {
                Elegir_película(usuario, app, película);
            }
            else if (input1 == 2)
            {
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Criterio ingresado no válido");
            }
        }


        public void Elegir_canción(Usuario usuario, App app, List<Canción> canciones)
        {
            Dictionary<int, Canción> dic = new Dictionary<int, Canción>();
            Console.WriteLine("Escoja una canción: ");
            int índice = 1;
            foreach (Canción canción in canciones)
            {
                Console.WriteLine("índice" + ") " + canción.titulo);
                dic.Add(índice, canción);
                índice += 1;
            }
            int input = Convert.ToInt32(Console.ReadLine());
            Canción canción1 = dic[input];
            Console.WriteLine("1) Reproducir");
            Console.WriteLine("2) Calificar Canción");
            Console.WriteLine("3) Agregarla a mi playlist");
            Console.WriteLine("4) Ver información de la canción");
            Console.WriteLine("5) Ver otras canciones");
            Console.WriteLine("6) Agregar a la cola");
            string input1 = Console.ReadLine();
            if (input1 == "1")
            {
                canción1.Play();
            }
            else if (input1 == "2")
            {
                Console.WriteLine("¿Con qué nota le pondría a esta canción? (1 a 10)");
                int nota = Convert.ToInt32(Console.ReadLine());
                canción1.calificacion.Add(nota);
                Rankear(canción1.titulo, canción1.calificacion, 2);
                Thread.Sleep(1000);
            }
            else if (input1 == "3")
            {
                if (usuario.Tipo_usuario == "Premium")
                {
                    Dictionary<int, Playlist> dic2 = new Dictionary<int, Playlist>();
                    Console.WriteLine("Playlists: ");
                    int índice1 = 1;
                    foreach (Playlist playlist in usuario.Favoritos)
                    {
                        Console.WriteLine(índice1 + ") " + playlist.Nombre);
                        dic2.Add(índice1, playlist);
                        índice1 += 1;
                    }
                    int input2 = Convert.ToInt32(Console.ReadLine());
                    if (input != 0)
                    {
                        Playlist playlist1 = dic2[input2];
                        playlist1.playlist_Canciones.Add(canción1);
                        usuario.Favoritos.RemoveAt(input2 - 1);
                        usuario.Favoritos.Insert(input2 - 1, playlist1);
                    }
                }
                else if (usuario.Tipo_usuario == "Gratis")
                {
                    Console.WriteLine("Usuario gratis no permite tener listas, para poder hacerlo debe suscribirse");
                }
            }
            else if (input1 == "4")
            {
                Console.WriteLine(canción1.titulo);
            }
            else if (input1 == "5")
            {
                Ver_Canciones(usuario, app);
            }
            else if (input1 == "6")
            {
                AddQueue(canción1);
            }
            else
            {
                Console.WriteLine("Criterio ingresado no válido");
            }
        }
        public void hacerse_premium()
        {
            Console.WriteLine("Nombre de usuario: ");
            string input = Console.ReadLine();
            Console.WriteLine("Contrasena: ");
            string input2 = Console.ReadLine();
            string result = Archivos.LogIn(input, input2);
            if (result == null)
            {
                foreach (List<string> user in Archivos.Lista_usuarios.Values)
                {
                    if (user[0] == input && user[2] == input2)
                    {
                        user[3] = "Premium";

                    }
                }
                foreach (Usuario user in Archivos.Usuarios)
                {
                    if (user.Nombre_usuario == input && user.Contraseña == input2)
                    {
                        user.Tipo_usuario = "Premium";
                    }
                }
                Console.WriteLine("Su cuenta ha cambiado a ser premium");
            }
            else
            {
                Console.WriteLine("[!] ERROR: " + result);
                Console.WriteLine("Criterio ingresado no válido");
            }
        }
        public void Ver_Canciones(Usuario usuario, App app)
        {
            Dictionary<int, Canción> dic = new Dictionary<int, Canción>();
            Console.WriteLine("Canciones: ");
            int índice = 1;
            foreach (Canción canción in Archivos.cancionesApp)
            {
                Console.WriteLine(índice + ")" + canción.titulo);
                dic.Add(índice, canción);
                índice += 1;
            }
            int input = Convert.ToInt32(Console.ReadLine());
            Canción canción1 = dic[input];
            Console.WriteLine("(a) Reproducir\n(b) Valorar Canción\n(c) Agregar a una Playlist\n(d) Seleccionar otra canción\n(e) Nada (Solo quería ver la información de la Canción)");
            Console.WriteLine("1) Reproducir");
            Console.WriteLine("2) Calificar Canción");
            Console.WriteLine("3) Agregarla a mi playlist");
            Console.WriteLine("4) Ver información de la canción");
            Console.WriteLine("5) Ver otras canciones");
            Console.WriteLine("6) Agregar a la cola");
            string input4 = Console.ReadLine();
            if (input4 == "1")
            {
                canción1.Play();
            }
            else if (input4 == "2")
            {
                Console.WriteLine("¿Con qué nota le pondría a esta canción? (1 a 10)");
                int nota = Convert.ToInt32(Console.ReadLine());
                canción1.calificacion.Add(nota);
                Rankear(canción1.titulo, canción1.calificacion, 2);
                Thread.Sleep(1000);
            }
            else if (input4 == "3")
            {
                if (usuario.Tipo_usuario == "Premium")
                {
                    Dictionary<int, Playlist> dic2 = new Dictionary<int, Playlist>();
                    Console.WriteLine("Playlists: ");
                    int índice1 = 1;
                    foreach (Playlist playlist in usuario.Favoritos)
                    {
                        Console.WriteLine(índice1 + ") " + playlist.Nombre);
                        dic2.Add(índice1, playlist);
                        índice1 += 1;
                    }
                    int input5 = Convert.ToInt32(Console.ReadLine());
                    if (input != 0)
                    {
                        Playlist playlist1 = dic2[input5];
                        playlist1.playlist_Canciones.Add(canción1);
                        usuario.Favoritos.RemoveAt(input5 - 1);
                        usuario.Favoritos.Insert(input5 - 1, playlist1);
                    }
                }
                else if (usuario.Tipo_usuario == "Gratis")
                {
                    Console.WriteLine("Usuario gratis no permite tener listas, para poder hacerlo debe suscribirse");
                }
            }
            else if (input4 == "4")
            {
                Console.WriteLine(canción1.titulo);
            }
            else if (input4 == "5")
            {
                Ver_Canciones(usuario, app);
            }
            else if (input4 == "6")
            {
                AddQueue(canción1);
            }
            else
            {
                Console.WriteLine("Criterio ingresado no válido");
            }


        }
        public void Ver_Películas(Usuario usuario, App app)
        {
            Dictionary<int, Película> dic = new Dictionary<int, Película>();
            Console.WriteLine("Películas: ");
            int índice = 1;
            foreach (Película película in Archivos.películasApp)
            {
                Console.WriteLine(índice + ")" + película.titulo);
                dic.Add(índice, película);
                índice += 1;
            }
            int input = Convert.ToInt32(Console.ReadLine());
            Película película1 = dic[input];
            Console.WriteLine("1) Reproducir");
            Console.WriteLine("2) Calificar Canción");
            Console.WriteLine("3) Agregarla a mi playlist");
            Console.WriteLine("4) Ver información de la canción");
            Console.WriteLine("5) Ver otras canciones");
            Console.WriteLine("6) Agregar a la cola");
            string input4 = Console.ReadLine();
            if (input4 == "1")
            {
                //película1.Play();
            }
            else if (input4 == "2")
            {
                Console.WriteLine("¿Con qué nota le pondría a esta canción? (1 a 10)");
                int nota = Convert.ToInt32(Console.ReadLine());
                película1.Rating.Add(nota);
                Rankear(película1.titulo, película1.Rating, 1);
                Thread.Sleep(1000);
            }
            else if (input4 == "3")
            {
                if (usuario.Tipo_usuario == "Premium")
                {
                    Dictionary<int, Playlist> dic2 = new Dictionary<int, Playlist>();
                    Console.WriteLine("Playlists: ");
                    int índice1 = 1;
                    foreach (Playlist playlist in usuario.Favoritos)
                    {
                        Console.WriteLine(índice1 + ") " + playlist.Nombre);
                        dic2.Add(índice1, playlist);
                        índice1 += 1;
                    }
                    int input5 = Convert.ToInt32(Console.ReadLine());
                    if (input != 0)
                    {
                        Playlist playlist1 = dic2[input5];
                        playlist1.playlist_Películas.Add(película1);
                        usuario.Favoritos.RemoveAt(input5 - 1);
                        usuario.Favoritos.Insert(input5 - 1, playlist1);
                    }
                }
                else if (usuario.Tipo_usuario == "Gratis")
                {
                    Console.WriteLine("Usuario gratis no permite tener listas, para poder hacerlo debe suscribirse");
                }
            }
            else if (input4 == "4")
            {
                Console.WriteLine(película1.titulo);
            }
            else if (input4 == "5")
            {
                Ver_Películas(usuario, app);
            }
            else if (input4 == "6")
            {
                AddQueue2(película1);
            }
            else
            {
                Console.WriteLine("Criterio ingresado no válido");
            }


        }
        public void Ver_opciones_playlist(Usuario usuario, App app)
        {
            if (usuario.Tipo_usuario == "Premium")
            {
                Dictionary<int, Playlist> dic = new Dictionary<int, Playlist>();
                Console.WriteLine("Playlists");
                int índice = 1;
                foreach (Playlist playlist in usuario.Favoritos)
                {
                    Console.WriteLine(índice + ")" + playlist.Nombre);
                    dic.Add(índice, playlist);
                    índice += 1;
                }
                if (usuario.Favoritos.Count() > 0)
                {
                    int input = Convert.ToInt32(Console.ReadLine());
                    usuario.Favoritos[input - 1].VerPlaylist();
                    Console.WriteLine("(a) Poner una Canción/Película en específico\n(b) Reproducir Canción/Película Aleatoria");
                    Console.WriteLine("1) Agregar Canción");
                    int input2 = Convert.ToInt32(Console.ReadLine());
                    if (input2 == 1)
                    {
                        if (usuario.Favoritos[input2 - 1].playlist_Películas.Count() > 0)
                        {
                            if (usuario.Favoritos[input2 - 1].Tipo_playlist == "película")
                            {
                                Dictionary<int, Película> dic3 = new Dictionary<int, Película>();
                                Console.WriteLine("Películas: ");
                                int índice2 = 1;
                                for (int i = 0; i < usuario.Favoritos[índice2 - 1].playlist_Películas.Count; i++)
                                {
                                    Console.WriteLine(índice2 + ")" + usuario.Favoritos[input - 1].playlist_Películas[i].titulo);
                                    dic3.Add(índice2, usuario.Favoritos[input - 1].playlist_Películas[i]);
                                    índice2 += 1;
                                }
                                int option = Convert.ToInt32(Console.ReadLine());
                                //usuario.Favoritos[input - 1].playlist_Películas[option - 1].Play();
                            }
                        }
                        if (usuario.Favoritos[input - 1].playlist_Canciones.Count() > 0)
                        {
                            if (usuario.Favoritos[input - 1].Tipo_playlist == "canción")
                            {
                                Dictionary<int, Canción> dic4 = new Dictionary<int, Canción>();
                                Console.WriteLine("Canciones: ");
                                int índice3 = 1;
                                for (int i = 0; i < usuario.Favoritos[input - 1].playlist_Canciones.Count; i++)
                                {
                                    Console.WriteLine(índice3 + ")" + usuario.Favoritos[input - 1].playlist_Canciones[i].titulo);
                                    dic4.Add(índice3, usuario.Favoritos[input - 1].playlist_Canciones[i]);
                                    índice3 += 1;
                                }
                                int option1 = Convert.ToInt32(Console.ReadLine());
                                usuario.Favoritos[input - 1].playlist_Canciones[option1 - 1].Play();
                            }
                        }

                    }
                    else if (input2 == 2)
                    {
                        if (usuario.Favoritos[input - 1].playlist_Películas.Count() > 0)
                        {
                            if (usuario.Favoritos[input - 1].Tipo_playlist == "película")
                            {
                                Random rdn = new Random();
                                int numero = rdn.Next(usuario.Favoritos[input - 1].playlist_Películas.Count);
                                //usuario.Favoritos[input - 1].playlist_Películas[numero].Play();
                            }
                        }
                        if (usuario.Favoritos[input - 1].playlist_Canciones.Count() > 0)
                        {
                            if (usuario.Favoritos[input - 1].Tipo_playlist == "canción")
                            {
                                Random rdn = new Random();
                                int numero = rdn.Next(usuario.Favoritos[input - 1].playlist_Canciones.Count);
                                usuario.Favoritos[input - 1].playlist_Canciones[numero].Play();
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("Criterio ingresado no válido");
                    }
                }
                else
                {
                    Console.WriteLine("No tiene playlist agregadas");
                    Thread.Sleep(1000);
                }
            }

            else if (usuario.Tipo_usuario == "Gratis")
            {
                Console.WriteLine("Función no disponible para usuario Gratis");
            }
        }
        public void Elegir_película(Usuario usuario, App app, List<Película> películas)
        {
            Dictionary<int, Película> dic = new Dictionary<int, Película>();
            Console.WriteLine("Escoja una película: ");
            int índice = 1;
            foreach (Película película in películas)
            {
                Console.WriteLine("índice" + ") " + película.titulo);
                dic.Add(índice, película);
                índice += 1;
            }
            int input = Convert.ToInt32(Console.ReadLine());
            Película película1 = dic[input];
            Console.WriteLine("1) Reproducir");
            Console.WriteLine("2) Calificar película");
            Console.WriteLine("3) Agregarla a mi playlist");
            Console.WriteLine("4) Ver información de la película");
            Console.WriteLine("5) Ver otras películas");
            Console.WriteLine("6) Agregar a la cola");
            string input1 = Console.ReadLine();
            if (input1 == "1")
            {
                //película1.Play();
            }
            else if (input1 == "2")
            {
                Console.WriteLine("¿Qué nota le pondría a esta película? (1 a 10)");
                int nota = Convert.ToInt32(Console.ReadLine());
                película1.Rating.Add(nota);
                Rankear(película1.titulo, película1.Rating, 1);
                Thread.Sleep(1000);
            }
            else if (input1 == "3")
            {
                if (usuario.Tipo_usuario == "Premium")
                {
                    Dictionary<int, Playlist> dic2 = new Dictionary<int, Playlist>();
                    Console.WriteLine("Playlists: ");
                    int índice1 = 1;
                    foreach (Playlist playlist in usuario.Favoritos)
                    {
                        Console.WriteLine(índice1 + ") " + playlist.Nombre);
                        dic2.Add(índice1, playlist);
                        índice1 += 1;
                    }
                    int input2 = Convert.ToInt32(Console.ReadLine());
                    if (input != 0)
                    {
                        Playlist playlist1 = dic2[input2];
                        playlist1.playlist_Películas.Add(película1);
                        usuario.Favoritos.RemoveAt(input2 - 1);
                        usuario.Favoritos.Insert(input2 - 1, playlist1);
                    }
                }
                else if (usuario.Tipo_usuario == "Gratis")
                {
                    Console.WriteLine("Usuario gratis no permite tener listas, para poder hacerlo debe suscribirse");
                }
            }
            else if (input1 == "4")
            {
                Console.WriteLine(película1.titulo);
            }
            else if (input1 == "5")
            {
                Ver_Canciones(usuario, app);
            }
            else if (input1 == "6")
            {
                AddQueue2(película1);
            }
            else
            {
                Console.WriteLine("Criterio ingresado no válido");
            }
        }
    }
}