using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entrega3Spotiflix.CustomsEvenArgs;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Windows.Forms.VisualStyles;

namespace Entrega3Spotiflix
{
    public partial class AppForm : Form
    {
        //Organizacion
        List<Panel> stackPanels = new List<Panel>();
        Dictionary<string, Panel> panels = new Dictionary<string, Panel>();
        string ruta;

        //Eventos
        public delegate bool RegisterEventHandler(object source, RegisterEventArgs args);
        public event RegisterEventHandler RegisterClicked;
        public delegate bool LoginEventHandler(object source, LoginEventArgs args);
        public event LoginEventHandler LoginButtonClicked;
        public event EventHandler<LoginEventArgs> UserChecked;
        public delegate bool AgregarCancionEventHandler(object source, AgregarCancionEventArgs args);
        public event AgregarCancionEventHandler AgregarCancionClicked;
        public event EventHandler<AgregarCancionEventArgs> CancionChecked;

        public AppForm()
        {
            InitializeComponent();
            string resolución = "256kbps";
            int reproducciones = 0;
            int Avg_calificacion = 0; //CANCION Y PELICULA
            List<int> Calificación = new List<int>(); //CANCION Y PELICULA
            //Una Cerveza-Ráfaga
            int duración1 = 240;
            Artista Ráfaga = new Artista("Ráfaga", "Ráfaga", "Masculino", 25, "Argentina");
            Album Una_cerveza = new Album("Foto_UnaCerveza.jpg", "Una cerveza", Ráfaga, 2016);
            List<string> genero_una_cerveza = new List<string>();
            string espacio = "3,78MB";
            genero_una_cerveza.Add("Cumbia");
            string url_una_cerveza = @"\una cerveza.mp3";
            Canción Una_Cerveza = new Canción("Una cerveza", Ráfaga, Una_cerveza, genero_una_cerveza, 2016, reproducciones, Avg_calificacion, duración1, resolución, espacio, url_una_cerveza);
            Archivos.cancionesApp.Add(Una_Cerveza);

            //Callaita-Bad Bunny
            int duración2 = 251;
            Artista Bad_Bunny = new Artista("Bad Bunny", "Bad Bunny", "Masculino", 26, "Puerto Rico");
            Album callaita = new Album("Foto_callaita.jpg", "Callaita", Bad_Bunny, 2019);
            List<string> genero_callaita = new List<string>();
            string espacio2 = "7,68MB";
            genero_una_cerveza.Add("Reggaeton");
            string url_callaita = @"\callaita.mp3";
            Canción Callaita = new Canción("Callaita", Bad_Bunny, callaita, genero_callaita, 2019, reproducciones, Avg_calificacion, duración2, resolución, espacio2, url_callaita);
            Archivos.cancionesApp.Add(Callaita);
            IniciarSerializacion();
            panels.Add("EntradaPanel", panelEntrada);
            panels.Add("LoginPanel", panelLogin);
            panels.Add("RegisterPanel", panelRegister);
            panels.Add("MenuPanel", panelMenu);
            panels.Add("ModificarCuentaPanel", panelModificarCuenta);
            panels.Add("CancionesPanel", panelCancciones);
            panels.Add("PelículasPanel", panelPelículas);
            panels.Add("BúsquedaPanel", panelBúsqueda);
            panels.Add("AgregarCancionpanel", panelAgregarCancion);
            foreach (Usuario usuario in Archivos.Usuarios)
            {
                if (usuario.Logeado == true)
                {
                    stackPanels.Add(panels["MenuPanel"]);
                    setNameUser(usuario.Nombre_usuario);
                    ShowLastPanel();
                }
            }
            if (stackPanels.Count() == 0)
            {
                stackPanels.Add(panels["EntradaPanel"]);
                ShowLastPanel();
            }
        }
        private void OnLoginButtonClicked(string username, string pass)
        {
            if (LoginButtonClicked != null)
            {
                bool result = LoginButtonClicked(this, new LoginEventArgs() { UsernameText = username, PasswordText = pass });
                if (!result)
                {
                    loginViewInvalidCredentialsAlert.Text = "Credenciales invalidas";
                    loginViewInvalidCredentialsAlert.Visible = true;
                }
                else
                {
                    loginViewInvalidCredentialsAlert.ResetText();
                    loginViewInvalidCredentialsAlert.Visible = false;
                    foreach (Usuario user in Archivos.Usuarios)
                    {
                        if (user.Nombre_usuario == username)
                        {
                            user.Logeado = true;
                            Serializacion();
                        }
                    }
                    OnUserChecked(username);
                }
            }
        }
        private void OnUserChecked(string username)
        {
            if (UserChecked != null)
            {
                UserChecked(this, new LoginEventArgs() { UsernameText = username });
                loginViewUserInput.ResetText();
                loginViewPassWordInput.ResetText();
                setNameUser(username);
                foreach (Usuario user in Archivos.Usuarios)
                {
                    if (user.Nombre_usuario == username)
                    {
                        user.Logeado = true;
                        Serializacion();
                    }
                }
                stackPanels.Add(panels["MenuPanel"]);
                ShowLastPanel();
            }
        }
        private void OnRegisterClicked(string username, string email, string pass, string tipo_usuario)
        {
            if ((tipo_usuario == "") || (username == "") || pass == "" || email == "")
            {
                registerViewInvalidCredentialsAlert.Text = "Agregue los valores que faltan";
                registerViewInvalidCredentialsAlert.Visible = true;
            }
            else
            {
                bool result = RegisterClicked(this, new RegisterEventArgs() { Username = username, Password = pass, Email = email, Tipo_usuario = tipo_usuario });
                if (!result)
                {
                    registerViewInvalidCredentialsAlert.Text = "Esta cuenta ya existe";
                    registerViewInvalidCredentialsAlert.Visible = true;
                }
                else
                {
                    registerViewInvalidCredentialsAlert.ResetText();
                    registerViewInvalidCredentialsAlert.Visible = false;
                    Serializacion();
                    OnUserChecked(username);
                }
            }
        }
        public void setNameUser(string username)
        {
            textBoxUsernamePerfil.Text = username;
        }
        private void OnAgregarCancionClicked(string nombre, string artista, string album, string genero, string AñoPublicacion, string reproducciones, string avg_calificacion, string duracion, string resolucion, string espacio, string ruta)
        {
            if (nombre == "" || artista == "" || album == "" || duracion == "" || espacio == "" || Convert.ToString(resolucion) == "" || ruta == "" || genero == "")
            {
                labelFaltanDatosCancion.Text = "No han ingresado todos los datos";
                labelFaltanDatosCancion.Visible = true;
            }
            else
            {
                bool result = AgregarCancionClicked(this, new AgregarCancionEventArgs() { Nombre = nombre, Artista = artista, Album = album, genero = genero, añoPublicacion = AñoPublicacion, Reproducciones = Convert.ToInt32(reproducciones), avg_calificacion = Convert.ToInt32(avg_calificacion), Duracion = duracion, Resolucion = resolucion, Espacio = espacio, URL = ruta });
                if (!result)
                {
                    labelFaltanDatosCancion.Text = "Esta cancion ya existe";
                    labelFaltanDatosCancion.Visible = true;
                }
                else
                {
                    labelFaltanDatosCancion.ResetText();
                    labelFaltanDatosCancion.Visible = false;
                    Serializacion();
                    OnCancionChecked(nombre);
                }
            }
        }
        private void OnCancionChecked(string nombre)
        {
            if (CancionChecked != null)
            {
                CancionChecked(this, new AgregarCancionEventArgs() { Nombre = nombre });
                textBoxNombreCancion.ResetText();
                textBoxArtistaCancion.ResetText();
                textBoxAlbumCancion.ResetText();
                textBoxDuracionCancion.ResetText();
                textBoxGeneroCancion.ResetText();
                textBoxEspacioCancion.ResetText();
                textBoxResoluciónCancion.ResetText();
                labelFaltanDatosCancion.ResetText();
                ; setNombreCancion(nombre);
                foreach (Canción cancion in Archivos.cancionesApp)
                {
                    if (cancion.Titulo == nombre)
                    {
                        cancion.Agregada = true;
                        Serializacion();
                    }
                }
                stackPanels.Add(panels["MenuPanel"]);
                ShowLastPanel();
            }
        }
        public void setNombreCancion(string nombre)
        {
            textBoxNombreCancion.Text = nombre;
        }
        private void ShowLastPanel()
        {
            foreach (Panel panel in panels.Values)
            {
                if (panel != stackPanels.Last())
                {
                    panel.Visible = false;
                }
                else
                {
                    panel.Dock = DockStyle.Fill;
                    panel.Visible = true;
                }
            }
        }

        private void buttonGoLogin_Click(object sender, EventArgs e)
        {
            loginViewUserInput.ResetText();
            loginViewPassWordInput.ResetText();
            loginViewInvalidCredentialsAlert.ResetText();
            loginViewInvalidCredentialsAlert.Visible = false;
            stackPanels.Add(panels["LoginPanel"]);
            ShowLastPanel();
        }

        private void buttonGoRegister_Click(object sender, EventArgs e)
        {
            registerViewInvalidCredentialsAlert.ResetText();
            registerViewInvalidCredentialsAlert.Visible = false;
            registerViewUserInput.ResetText();
            registerViewEmailInput.ResetText();
            registerViewPassInput.ResetText();
            comboBoxTipoUsuario.ResetText();
            stackPanels.Add(panels["RegisterPanel"]);
            ShowLastPanel();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void loginViewButton_Click(object sender, EventArgs e)
        {
            string username = loginViewUserInput.Text;
            string pass = loginViewPassWordInput.Text;
            OnLoginButtonClicked(username, pass);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void loginViewInvalidCredentialsAlert_TextChanged(object sender, EventArgs e)
        {

        }

        private void loginViewPassWordInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void loginViewUserInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonVolverDeLogin_Click(object sender, EventArgs e)
        {
            stackPanels.Add(panels["EntradaPanel"]);
            ShowLastPanel();
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            stackPanels.Add(panels["EntradaPanel"]);
            foreach (Usuario usuario in Archivos.Usuarios)
            {
                if (usuario.Logeado == true)
                {
                    usuario.Logeado = false;
                }
            }
            Serializacion();
            setNameUser("");
            ShowLastPanel();
        }

        private void registerViewButton_Click(object sender, EventArgs e)
        {
            string username = registerViewUserInput.Text;
            string email = registerViewEmailInput.Text;
            string pass = registerViewPassInput.Text;
            string tipo_usuario = comboBoxTipoUsuario.SelectedItem.ToString();
            OnRegisterClicked(username, email, pass, tipo_usuario);
        }

        private void buttonVolverDeRegister_Click(object sender, EventArgs e)
        {
            stackPanels.Add(panels["EntradaPanel"]);
            ShowLastPanel();
        }

        private void buttonModificarCuenta_Click(object sender, EventArgs e)
        {
            textBoxHacersePremium.Visible = false;
            label11.Visible = false;
            textBoxUsuarioCambioUsername.Visible = false;
            buttonConfirmarCambioUsername.Visible = false;
            textBoxCambiarUsername.Visible = false;
            textBoxContraseñaCambioContraseña.Visible = false;
            textBoxAntiguaContraseñaCambioContraseña.Visible = false;
            label10.Visible = false;
            label9.Visible = false;
            buttonConfirmarCambioContraseña.Visible = false;
            textBoxCambioContraseña.Visible = false;
            stackPanels.Add(panels["ModificarCuentaPanel"]);
            ShowLastPanel();
        }

        private void buttonVolverDeModificarCuenta_Click(object sender, EventArgs e)
        {
            stackPanels.Add(panels["MenuPanel"]);
            ShowLastPanel();
        }

        private void buttonCambiarContraseña_Click(object sender, EventArgs e)
        {
            label11.Visible = false;
            textBoxUsuarioCambioUsername.Visible = false;
            buttonConfirmarCambioUsername.Visible = false;
            textBoxContraseñaCambioContraseña.ResetText();
            textBoxAntiguaContraseñaCambioContraseña.ResetText();
            textBoxCambioContraseña.ResetText();
            textBoxHacersePremium.Visible = false;
            textBoxCambiarUsername.Visible = false;
            textBoxContraseñaCambioContraseña.Visible = true;
            textBoxAntiguaContraseñaCambioContraseña.Visible = true;
            label10.Visible = true;
            label9.Visible = true;
            buttonConfirmarCambioContraseña.Visible = true;
            textBoxCambioContraseña.Visible = true;
        }

        private void buttonCambiarUsername_Click(object sender, EventArgs e)
        {
            textBoxContraseñaCambioContraseña.Visible = false;
            textBoxAntiguaContraseñaCambioContraseña.Visible = false;
            label10.Visible = false;
            label9.Visible = false;
            buttonConfirmarCambioContraseña.Visible = false;
            textBoxCambioContraseña.Visible = false;
            textBoxHacersePremium.Visible = false;
            textBoxUsuarioCambioUsername.ResetText();
            textBoxCambiarUsername.ResetText();
            label11.Visible = true;
            textBoxUsuarioCambioUsername.Visible = true;
            buttonConfirmarCambioUsername.Visible = true;
            textBoxCambiarUsername.Visible = true;
        }

        private void buttonHacersePremium_Click(object sender, EventArgs e)
        {
            textBoxContraseñaCambioContraseña.Visible = false;
            textBoxAntiguaContraseñaCambioContraseña.Visible = false;
            label10.Visible = false;
            label9.Visible = false;
            buttonConfirmarCambioContraseña.Visible = false;
            textBoxCambioContraseña.Visible = false;
            label11.Visible = false;
            textBoxUsuarioCambioUsername.Visible = false;
            buttonConfirmarCambioUsername.Visible = false;
            textBoxCambioContraseña.Visible = false;
            textBoxCambiarUsername.Visible = false;
            textBoxHacersePremium.Visible = true;
            string usuario_ant = textBoxUsernamePerfil.Text;
            List<string> Valores = Archivos.GetData(usuario_ant);
            if (Valores[3].Contains("Premium"))
            {
                textBoxHacersePremium.Visible = true;
                textBoxHacersePremium.Text = "Este Usuario ya es premium";
            }
            else
            {
                foreach (Usuario usuario in Archivos.Usuarios)
                {
                    if (usuario.Nombre_usuario == usuario_ant)
                    {
                        textBoxHacersePremium.Visible = true;
                        usuario.Tipo_usuario = "Premium";
                        textBoxHacersePremium.Text = "Se ha realizado el cambio con éxito";
                    }
                    Serializacion();
                }
            }
        }

        private void buttonConfirmarCambioUsername_Click(object sender, EventArgs e)
        {
            string usuario_ant = textBoxUsernamePerfil.Text;
            string usuario_nuevo = textBoxUsuarioCambioUsername.Text;
            List<string> Valores = Archivos.GetData(usuario_ant);
            Valores[0] = usuario_nuevo;
            foreach (Usuario usuario in Archivos.Usuarios)
            {
                if (usuario.Nombre_usuario == usuario_ant)
                {
                    usuario.Nombre_usuario = usuario_nuevo;
                }
            }
            textBoxUsernamePerfil.Text = usuario_nuevo;
            textBoxCambiarUsername.Text = "Username cambiado con éxito";
            label11.Visible = false;
            textBoxUsuarioCambioUsername.Visible = false;
            buttonConfirmarCambioUsername.Visible = false;
            Serializacion();
        }

        private void buttonConfirmarCambioContraseña_Click(object sender, EventArgs e)
        {
            string user = textBoxUsernamePerfil.Text;
            string contraseña_ant = textBoxAntiguaContraseñaCambioContraseña.Text;
            string contraseña_nueva = textBoxContraseñaCambioContraseña.Text;
            List<string> Valores = Archivos.GetData(user);
            foreach (Usuario usuario in Archivos.Usuarios)
            {
                if (usuario.Nombre_usuario == user)
                {
                    if (contraseña_ant == usuario.Contraseña)
                    {
                        usuario.Contraseña = contraseña_nueva;
                        Valores[2] = contraseña_nueva;
                        textBoxCambioContraseña.Text = "Contraseña cambiada con éxito";
                    }
                    else
                    {
                        textBoxCambioContraseña.Text = "Contraseña Actual Incorrecta";
                    }
                    Serializacion();
                }
            }
            textBoxContraseñaCambioContraseña.Visible = false;
            textBoxAntiguaContraseñaCambioContraseña.Visible = false;
            label10.Visible = false;
            label9.Visible = false;
            buttonConfirmarCambioContraseña.Visible = false;
        }

        private void textBoxAntiguaContraseñaCambioContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBoxHacersePremium_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxCambioContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxCambiarUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxContraseñaCambioContraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBoxUsuarioCambioUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBoxUsernamePerfil_TextChanged(object sender, EventArgs e)
        {

        }

        private void FotoMenu_Click(object sender, EventArgs e)
        {

        }

        //Serialización

        private void Serializacion()
        {
            IFormatter formatter = new BinaryFormatter();

            Stream stream7 = new FileStream("películasApp.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            Stream stream8 = new FileStream("cancionesApp.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            Stream stream9 = new FileStream("playlists_Canciones.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            Stream stream10 = new FileStream("playlists_Películas.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            Stream stream11 = new FileStream("Usuarios.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            Stream stream12 = new FileStream("PersonasApp.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream7, Archivos.películasApp);
            //formatter.Serialize(stream8, Archivos.cancionesApp);
            formatter.Serialize(stream9, Archivos.playlists_Canciones);
            formatter.Serialize(stream10, Archivos.playlists_Películas);
            formatter.Serialize(stream11, Archivos.Usuarios);
            formatter.Serialize(stream12, Archivos.PersonasApp);
            stream7.Close();
            stream8.Close();
            stream9.Close();
            stream10.Close();
            stream11.Close();
            stream12.Close();
        }
        private void IniciarSerializacion()
        {
            Usuario grupo7 = new Usuario("username", "email", "contraseña", "Premium");
            Archivos.Usuarios.Add(grupo7);
            foreach (Usuario i in Archivos.Usuarios)
            {
                List<string> data = new List<string>()
                        { i.Nombre_usuario, i.Email, i.Contraseña, Convert.ToString(DateTime.Now), i.Tipo_usuario};
                Archivos.Lista_usuarios.Add(Archivos.Lista_usuarios.Count + 1, data);
            }

            IFormatter formatter = new BinaryFormatter();

            string urlpelículasApp = Directory.GetCurrentDirectory() + "\\películasApp.bin";
            string urlcancionesApp = Directory.GetCurrentDirectory() + "\\cancionesApp.bin";
            string urlplaylists_Canciones = Directory.GetCurrentDirectory() + "\\playlists_Canciones.bin";
            string urlplaylists_Películas = Directory.GetCurrentDirectory() + "\\playlists_Películas.bin";
            string urlUsuarios = Directory.GetCurrentDirectory() + "\\Usuarios.bin";
            string urlPersonasApp = Directory.GetCurrentDirectory() + "\\PersonasApp.bin";

            if (File.Exists(urlpelículasApp) && File.Exists(urlcancionesApp) && File.Exists(urlplaylists_Canciones) && File.Exists(urlplaylists_Películas) && File.Exists(urlUsuarios) && File.Exists(urlPersonasApp))
            {
                Stream stream1 = new FileStream("películasApp.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                Stream stream2 = new FileStream("cancionesApp.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                Stream stream3 = new FileStream("playlists_Canciones.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                Stream stream4 = new FileStream("playlists_Películas.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                Stream stream5 = new FileStream("Usuarios.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                Stream stream6 = new FileStream("PersonasApp.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                //try que Desterializa; catch mostrar mensaje; finally cierra archivo
                try
                {
                    List<Película> des = (List<Película>)formatter.Deserialize(stream1);
                    if (des.Count != 0)
                    {
                        Archivos.películasApp = des;
                    }
                }
                catch
                {
                }
                try
                {
                    List<Canción> des2 = (List<Canción>)formatter.Deserialize(stream2);
                    if (des2.Count != 0)
                    {
                        Archivos.cancionesApp.Clear();
                        Archivos.cancionesApp = des2;
                    }
                }
                catch
                {
                }
                try
                {
                    Archivos.playlists_Canciones = (List<Playlist>)formatter.Deserialize(stream3);
                }
                catch
                {
                }
                try
                {
                    Archivos.playlists_Películas = (List<Playlist>)formatter.Deserialize(stream4);
                }
                catch
                {
                }
                try
                {
                    Archivos.Usuarios = (List<Usuario>)formatter.Deserialize(stream5);
                    foreach (Usuario i in Archivos.Usuarios)
                    {
                        List<string> data = new List<string>()
                        { i.Nombre_usuario, i.Email, i.Contraseña, Convert.ToString(DateTime.Now), i.Tipo_usuario};
                        Archivos.Lista_usuarios.Add(Archivos.Lista_usuarios.Count + 1, data);
                    }
                }
                catch
                {
                }
                try
                {
                    Archivos.PersonasApp = (List<Person>)formatter.Deserialize(stream6);
                }
                catch
                {
                }
                finally
                {
                    stream1.Close();
                    stream2.Close();
                    stream3.Close();
                    stream4.Close();
                    stream5.Close();
                    stream6.Close();
                }
            }
        }

        private void panelCancciones_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonGoVerCanciones_Click(object sender, EventArgs e)
        {
            FotoCanciónMostrada.Visible = false;
            buttonAgregarCancionAPlaylist.Visible = false;
            buttonReproducir.Visible = false;
            buttonEvaluar.Visible = false;
            comboBoxCalificación.Visible = false;
            buttonConfirmarCalificación.Visible = false;
            CanciónSeleccionada.Visible = false;
            panel1.Visible = false;
            axWindowsMediaPlayer1.Visible = false;
            stackPanels.Add(panels["CancionesPanel"]);
            ShowLastPanel();
            listViewCanciones.Clear();
            VerCanciones(Archivos.cancionesApp);

        }
        private void VerCanciones(List<Canción> canciones)
        {
            ListViewGroup Canciones = new ListViewGroup("Canciones", HorizontalAlignment.Left);
            foreach (Canción canción in canciones)
            {
                listViewCanciones.Items.Add(new ListViewItem(canción.Titulo, Canciones));

            }
            listViewCanciones.Groups.Add(Canciones);
        }
        private void VerPelículas(List<Película> películas)
        {
            ListViewGroup Películas = new ListViewGroup("Películas", HorizontalAlignment.Left);
            foreach (Película película in películas)
            {
                listViewPelículas.Items.Add(new ListViewItem(película.Titulo, Películas));

            }
            listViewPelículas.Groups.Add(Películas);
        }

        private void listViewCanciones_MouseClick(object sender, MouseEventArgs e)
        {
            axWindowsMediaPlayer2.Ctlcontrols.pause();
            axWindowsMediaPlayer2.Visible = false;
            panel1.Visible = true;
            FotoCanciónMostrada.Visible = true;
            buttonAgregarCancionAPlaylist.Visible = true;
            buttonReproducir.Visible = true;
            buttonEvaluar.Visible = true;
            CanciónSeleccionada.Visible = true;
            CanciónSeleccionada.Text = listViewCanciones.SelectedItems[0].SubItems[0].Text;
            foreach (Canción canción in Archivos.cancionesApp)
            {
                if (canción.titulo == CanciónSeleccionada.Text)
                {
                    try
                    {
                        FotoCanciónMostrada.Image = Image.FromFile(canción.album.Imagen);
                        FotoCanciónMostrada.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch
                    {
                        string sin_foto = "Sin_Imagen.jpg";
                        FotoCanciónMostrada.Image = Image.FromFile(sin_foto);
                        FotoCanciónMostrada.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    AlbumCanciónSeleccionada.Text = canción.album.Nombre;
                    ArtistaCanciónSeleccionada.Text = canción.artista.Apellido;
                    DuraciónCanciónSeleccionada.Text = canción.duración.ToString();
                    ReproduccionesCanciónSeleccionada.Text = canción.reproducciones.ToString();
                    CalificaciónCanciónSeleccionada.Text = canción.Avg_calificacion.ToString();

                }
            }
        }

        private void CanciónSeleccionada_Click(object sender, EventArgs e)
        {

        }

        private void FotoCanciónMostrada_Click(object sender, EventArgs e)
        {

        }

        private void buttonVolverDeVerCanción_Click(object sender, EventArgs e)
        {
            stackPanels.Add(panels["MenuPanel"]);
            ShowLastPanel();
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void buttonVolverDeVerPelícula_Click(object sender, EventArgs e)
        {
            stackPanels.Add(panels["MenuPanel"]);
            ShowLastPanel();
            axWindowsMediaPlayer2.Ctlcontrols.pause();
        }

        private void listViewPelículas_MouseClick(object sender, MouseEventArgs e)
        {
            axWindowsMediaPlayer2.Visible = false;
            FotoPelícula.Visible = true;
            buttonAgregarPelículaAPlaylist.Visible = true;
            buttonReproducirPelícula.Visible = true;
            buttonEvaluarPelícula.Visible = true;
            buttonInfoPelícula.Visible = true;
            PelículaSeleccionada.Visible = true;
            PelículaSeleccionada.Text = listViewPelículas.SelectedItems[0].SubItems[0].Text;
            foreach (Película película in Archivos.películasApp)
            {
                if (película.titulo == PelículaSeleccionada.Text)
                {
                    try
                    {
                        FotoPelícula.Image = Image.FromFile(película.Imagen);
                        FotoPelícula.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch
                    {
                        string sin_foto = "Sin_Imagen.jpg";
                        FotoPelícula.Image = Image.FromFile(sin_foto);
                        FotoPelícula.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    TituloPelículaSeleccionada.Text = película.director.Nombre;
                    AñoPelículaSeleccionada.Text = película.añoPublicacion.ToString();
                    DuraciónPelículaSeleccionada.Text = película.duracion.ToString();
                    CalificaciónPelículaSeleccionada.Text = película.Avg_calificación.ToString();

                }
            }
        }

        private void buttonGoVerPelículas_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            axWindowsMediaPlayer2.Visible = false;
            FotoPelícula.Visible = false;
            comboBoxCalificaciónPelícula.Visible = false;
            buttonConfirmarCalificaciónPelícula.Visible = false;
            buttonAgregarPelículaAPlaylist.Visible = false;
            buttonReproducirPelícula.Visible = false;
            buttonEvaluarPelícula.Visible = false;
            buttonInfoPelícula.Visible = false;
            PelículaSeleccionada.Visible = false;
            stackPanels.Add(panels["PelículasPanel"]);
            ShowLastPanel();
            listViewPelículas.Clear();
            VerPelículas(Archivos.películasApp);
        }

        private void buttonReproducir_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Visible = true;
            foreach (Canción canción in Archivos.cancionesApp)
                if (canción.titulo == CanciónSeleccionada.Text)
                {
                    EncontrarArchivo(canción.titulo);
                    axWindowsMediaPlayer1.URL = this.ruta;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    canción.reproducciones += 1;
                }
        }
        public void EncontrarArchivo(string valor)
        {
            string carpeta = Directory.GetCurrentDirectory();
            foreach (Película película in Archivos.películasApp)
            {
                if (valor == película.titulo)
                {
                    this.ruta = película.Url;
                }
            }
            foreach (Canción canción in Archivos.cancionesApp)
            {
                if (valor == canción.titulo)
                {
                    this.ruta = canción.Url;
                }
            }
        }

        private void pictureBoxPausa_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void pictureBoxReproducir_Click(object sender, EventArgs e)
        {

        }

        private void buttonReproducirPelícula_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer2.Visible = true;
            foreach (Película película in Archivos.películasApp)
                if (película.titulo == PelículaSeleccionada.Text)
                {
                    EncontrarArchivo(película.titulo);
                    axWindowsMediaPlayer2.URL = this.ruta;
                    axWindowsMediaPlayer2.Ctlcontrols.play();
                    película.reproducciones += 1;
                }
        }

        private void buttonGoBuscar_Click(object sender, EventArgs e)
        {
            comboBoxCriterioPelícula.ResetText();
            comboBoxCriterioCanción.ResetText();
            comboBoxCriterioPlaylists.ResetText();
            comboBoxCriterio1.ResetText();
            comboBoxCriterioPlaylists.Visible = false;
            comboBoxCriterioCanción.Visible = false;
            comboBoxCriterioPelícula.Visible = false;
            labelFiltroBúsqueda.Visible = false;
            panelBúsquedaDeCriterio.Visible = false;
            string fotoBuscar = "iconoBuscar.jpg";
            pictureBoxBuscar.Image = Image.FromFile(fotoBuscar);
            pictureBoxBuscar.SizeMode = PictureBoxSizeMode.StretchImage;
            stackPanels.Add(panels["BúsquedaPanel"]);
            ShowLastPanel();
        }

        private void buttonVolverDeBúsqueda_Click(object sender, EventArgs e)
        {
            stackPanels.Add(panels["MenuPanel"]);
            ShowLastPanel();
        }

        private void comboBoxCriterio1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selección = comboBoxCriterio1.SelectedItem.ToString();
            if (selección == "Canciones")
            {
                comboBoxCriterioCanción.Visible = true;
                comboBoxCriterioPelícula.Visible = false;
                comboBoxCriterioPlaylists.Visible = false;
                labelFiltroBúsqueda.Visible = true;
                comboBoxCriterioCanción.ResetText();
            }
            if (selección == "Películas")
            {
                comboBoxCriterioPelícula.Visible = true;
                comboBoxCriterioPlaylists.Visible = false;
                comboBoxCriterioCanción.Visible = false;
                labelFiltroBúsqueda.Visible = true;
                comboBoxCriterioPelícula.ResetText();
            }
            if (selección == "PlayLists")
            {
                comboBoxCriterioPlaylists.Visible = true;
                comboBoxCriterioCanción.Visible = false;
                comboBoxCriterioPelícula.Visible = false;
                labelFiltroBúsqueda.Visible = true;
                comboBoxCriterioPlaylists.ResetText();
            }
        }
        private void Buscar(string criterio1, string criterio2, string filtro)
        {
            listViewBúsqueda.Clear();
            //List<String> resultado = new List<string>();
            if (criterio1 == "Canciones")
            {
                if (criterio2 == "Título")
                {
                    foreach (Canción canción in Archivos.cancionesApp)
                    {
                        List<Canción> resultado = new List<Canción>();
                        if (canción.titulo == filtro)
                        {
                            labelErrorBúsqueda.Visible = false;
                            resultado.Add(canción);
                            ListViewGroup Canciones = new ListViewGroup("Canciones", HorizontalAlignment.Left);
                            foreach (Canción canción1 in resultado)
                            {
                                listViewBúsqueda.Items.Add(new ListViewItem(canción1.Titulo, Canciones));

                            }
                            listViewBúsqueda.Groups.Add(Canciones);
                        }
                        if (resultado.Count == 0)
                        {
                            labelErrorBúsqueda.Visible = true;
                            labelErrorBúsqueda.Text = "No se encontraron resultados";
                        }
                    }
                }
                if (criterio2 == "Género")
                {
                    foreach (Canción canción in Archivos.cancionesApp)
                    {
                        foreach (String género in canción.genero)
                        {
                            List<Canción> resultado = new List<Canción>();
                            if (género == filtro)
                            {
                                labelErrorBúsqueda.Visible = false;
                                resultado.Add(canción);
                                ListViewGroup Canciones = new ListViewGroup("Canciones", HorizontalAlignment.Left);
                                foreach (Canción canción1 in resultado)
                                {
                                    listViewBúsqueda.Items.Add(new ListViewItem(canción1.titulo, Canciones));

                                }
                                listViewBúsqueda.Groups.Add(Canciones);
                            }
                            if (resultado.Count == 0)
                            {
                                labelErrorBúsqueda.Visible = true;
                                labelErrorBúsqueda.Text = "No se encontraron resultados";
                            }
                        }
                    }
                }
                if (criterio2 == "Álbum")
                {
                    foreach (Canción canción in Archivos.cancionesApp)
                    {
                        List<Canción> resultado = new List<Canción>();
                        if (canción.album.Nombre == filtro)
                        {
                            labelErrorBúsqueda.Visible = false;
                            resultado.Add(canción);
                            ListViewGroup Canciones = new ListViewGroup("Canciones", HorizontalAlignment.Left);
                            foreach (Canción canción1 in resultado)
                            {
                                listViewBúsqueda.Items.Add(new ListViewItem(canción1.Titulo, Canciones));

                            }
                            listViewBúsqueda.Groups.Add(Canciones);
                        }
                        if (resultado.Count == 0)
                        {
                            labelErrorBúsqueda.Visible = true;
                            labelErrorBúsqueda.Text = "No se encontraron resultados";
                        }
                    }
                }
                if (criterio2 == "Artista")
                {
                    foreach (Canción canción in Archivos.cancionesApp)
                    {
                        List<Canción> resultado = new List<Canción>();
                        if (canción.artista.Nombre == filtro)
                        {
                            labelErrorBúsqueda.Visible = false;
                            resultado.Add(canción);
                            ListViewGroup Canciones = new ListViewGroup("Canciones", HorizontalAlignment.Left);
                            foreach (Canción canción1 in resultado)
                            {
                                listViewBúsqueda.Items.Add(new ListViewItem(canción1.Titulo, Canciones));

                            }
                            listViewBúsqueda.Groups.Add(Canciones);
                        }
                        if (resultado.Count == 0)
                        {
                            labelErrorBúsqueda.Visible = true;
                            labelErrorBúsqueda.Text = "No se encontraron resultados";
                        }
                    }
                }
            }
            if (criterio1 == "Películas")
            {
                if (criterio2 == "Título")
                {
                    foreach (Película película in Archivos.películasApp)
                    {
                        List<Película> resultado = new List<Película>();
                        if (película.titulo == filtro)
                        {
                            labelErrorBúsqueda.Visible = false;
                            resultado.Add(película);
                            ListViewGroup Películas = new ListViewGroup("Películas", HorizontalAlignment.Left);
                            foreach (Película película1 in resultado)
                            {
                                listViewBúsqueda.Items.Add(new ListViewItem(película1.Titulo, Películas));

                            }
                            listViewBúsqueda.Groups.Add(Películas);
                        }
                        if (resultado.Count == 0)
                        {
                            labelErrorBúsqueda.Visible = true;
                            labelErrorBúsqueda.Text = "No se encontraron resultados";
                        }
                    }
                }
                if (criterio2 == "Categoría")
                {
                    foreach (Película película in Archivos.películasApp)
                    {
                        foreach (String categoría in película.categoria)
                        {
                            List<Película> resultado = new List<Película>();
                            if (categoría == filtro)
                            {
                                labelErrorBúsqueda.Visible = false;
                                resultado.Add(película);
                                ListViewGroup Películas = new ListViewGroup("Películas", HorizontalAlignment.Left);
                                foreach (Película película1 in resultado)
                                {
                                    listViewBúsqueda.Items.Add(new ListViewItem(película1.Titulo, Películas));

                                }
                                listViewBúsqueda.Groups.Add(Películas);
                            }
                            if (resultado.Count == 0)
                            {
                                labelErrorBúsqueda.Visible = true;
                                labelErrorBúsqueda.Text = "No se encontraron resultados";
                            }
                        }
                    }
                }
                if (criterio2 == "Director")
                {
                    foreach (Película película in Archivos.películasApp)
                    {
                        List<Película> resultado = new List<Película>();
                        if (película.director.Nombre == filtro)
                        {
                            labelErrorBúsqueda.Visible = false;
                            resultado.Add(película);
                            ListViewGroup Películas = new ListViewGroup("Películas", HorizontalAlignment.Left);
                            foreach (Película película1 in resultado)
                            {
                                listViewBúsqueda.Items.Add(new ListViewItem(película1.Titulo, Películas));

                            }
                            listViewBúsqueda.Groups.Add(Películas);
                        }
                        if (resultado.Count == 0)
                        {
                            labelErrorBúsqueda.Visible = true;
                            labelErrorBúsqueda.Text = "No se encontraron resultados";
                        }
                    }
                }
            }
            if (criterio1 == "PlayLists")
            {
                if (criterio2 == "De Canciones")
                {
                    foreach (Playlist playlist in Archivos.playlists_Canciones)
                    {
                        List<Playlist> resultado = new List<Playlist>();
                        if (playlist.Nombre == filtro)
                        {
                            labelErrorBúsqueda.Visible = false;
                            resultado.Add(playlist);
                            ListViewGroup PlayLists = new ListViewGroup("PlayLists", HorizontalAlignment.Left);
                            foreach (Playlist playlist1 in resultado)
                            {
                                listViewBúsqueda.Items.Add(new ListViewItem(playlist1.Nombre, PlayLists));

                            }
                            listViewBúsqueda.Groups.Add(PlayLists);
                        }
                        if (resultado.Count == 0)
                        {
                            labelErrorBúsqueda.Visible = true;
                            labelErrorBúsqueda.Text = "No se encontraron resultados";
                        }
                    }
                }
                if (criterio2 == "De Películas")
                {
                    foreach (Playlist playlist in Archivos.playlists_Películas)
                    {
                        List<Playlist> resultado = new List<Playlist>();
                        if (playlist.Nombre == filtro)
                        {
                            labelErrorBúsqueda.Visible = false;
                            resultado.Add(playlist);
                            ListViewGroup PlayLists = new ListViewGroup("PlayLists", HorizontalAlignment.Left);
                            foreach (Playlist playlist1 in resultado)
                            {
                                listViewBúsqueda.Items.Add(new ListViewItem(playlist1.Nombre, PlayLists));

                            }
                            listViewBúsqueda.Groups.Add(PlayLists);
                        }
                        if (resultado.Count == 0)
                        {
                            labelErrorBúsqueda.Visible = true;
                            labelErrorBúsqueda.Text = "No se encontraron resultados";
                        }
                    }
                }
            }
        }

        private void comboBoxCriterioPelícula_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBúsquedaDeCriterio.Visible = true;
            labelErrorBúsqueda.Visible = false;
            labelErrorBúsqueda.ResetText();
            textBoxFiltroBúsqueda.ResetText();
        }

        private void comboBoxCriterioCanción_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBúsquedaDeCriterio.Visible = true;
            labelErrorBúsqueda.Visible = false;
            labelErrorBúsqueda.ResetText();
            textBoxFiltroBúsqueda.ResetText();
        }

        private void pictureBoxBuscar_Click(object sender, EventArgs e)
        {
            string selección1 = comboBoxCriterio1.SelectedItem.ToString();
            labelFiltroBúsqueda.Visible = true;
            string selección2;

            if (selección1 == "Canciones")
            {
                selección2 = comboBoxCriterioCanción.SelectedItem.ToString();
                string filtro = textBoxFiltroBúsqueda.Text;
                if (filtro == "")
                {
                    labelErrorBúsqueda.Visible = true;
                    labelErrorBúsqueda.Text = "Debe ingresar un valor de búsqueda";
                }
                else
                {
                    Buscar(selección1, selección2, filtro);
                }
            }
            if (selección1 == "PlayLists")
            {
                selección2 = comboBoxCriterioPlaylists.SelectedItem.ToString();
                string filtro = textBoxFiltroBúsqueda.Text;
                if (filtro == "")
                {
                    labelErrorBúsqueda.Visible = true;
                    labelErrorBúsqueda.Text = "Debe ingresar un valor de búsqueda";
                }
                else
                {
                    Buscar(selección1, selección2, filtro);
                }
            }
            if (selección1 == "Películas")
            {
                selección2 = comboBoxCriterioPelícula.SelectedItem.ToString();
                string filtro = textBoxFiltroBúsqueda.Text;
                if (filtro == "")
                {
                    labelErrorBúsqueda.Visible = true;
                    labelErrorBúsqueda.Text = "Debe ingresar un valor de búsqueda";
                }
                else
                {
                    Buscar(selección1, selección2, filtro);
                }
            }
        }

        private void comboBoxCriterioPlaylists_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelBúsquedaDeCriterio.Visible = true;
            labelErrorBúsqueda.Visible = false;
            labelErrorBúsqueda.ResetText();
            textBoxFiltroBúsqueda.ResetText();
        }

        private void axWindowsMediaPlayer2_Enter(object sender, EventArgs e)
        {

        }

        private void buttonConfirmarCalificación_Click(object sender, EventArgs e)
        {
            string nota1 = comboBoxCalificación.SelectedItem.ToString();
            if (nota1 == "")
            {
                MessageBox.Show("Por favor seleccione una nota antes de evaluar");
            }
            else
            {
                foreach (Canción canción in Archivos.cancionesApp)
                {
                    if (canción.titulo == CanciónSeleccionada.Text)
                    {
                        int nota2 = Convert.ToInt32(nota1);
                        canción.calificación.Add(nota2);
                        string Canción = "Canción";
                        Rankear(canción.titulo, canción.calificación, Canción);
                        MessageBox.Show("Se ha calificado la canción");
                        CalificaciónCanciónSeleccionada.Text = canción.Avg_calificacion.ToString();
                    }
                }
            }
            comboBoxCalificación.Visible = false;
            buttonConfirmarCalificación.Visible = false;
        }

        private void buttonEvaluar_Click(object sender, EventArgs e)
        {
            comboBoxCalificación.ResetText();
            comboBoxCalificación.Visible = true;
            buttonConfirmarCalificación.Visible = true;
        }
        public void Rankear(string nombre, List<int> calificaciones, string tipo)
        {
            if (tipo == "Canción")
            {
                foreach (Canción canción in Archivos.cancionesApp)
                {
                    if (nombre == canción.titulo)
                    {
                        int sum = calificaciones.Sum();
                        int valor = (sum / calificaciones.Count());
                        canción.Avg_calificacion = valor;
                    }
                }
            }
            else if (tipo == "Película")
            {
                foreach (Película película in Archivos.películasApp)
                {
                    if (nombre == película.titulo)
                    {
                        int sum = calificaciones.Sum();
                        int valor = (sum / calificaciones.Count());
                        película.Avg_Ranking = valor;
                    }
                }
            }
        }

        private void buttonEvaluarPelícul_Click(object sender, EventArgs e)
        {

        }

        private void buttonEvaluarPelícula_Click(object sender, EventArgs e)
        {
            comboBoxCalificaciónPelícula.ResetText();
            comboBoxCalificaciónPelícula.Visible = true;
            buttonConfirmarCalificaciónPelícula.Visible = true;
        }

        private void buttonConfirmarCalificaciónPelícula_Click(object sender, EventArgs e)
        {
            string nota1 = comboBoxCalificaciónPelícula.SelectedItem.ToString();
            if (nota1 == "")
            {
                MessageBox.Show("Por favor seleccione una nota antes de evaluar");
            }
            else
            {
                foreach (Película película in Archivos.películasApp)
                {
                    if (película.titulo == PelículaSeleccionada.Text)
                    {
                        int nota2 = Convert.ToInt32(nota1);
                        película.calificación.Add(nota2);
                        string Película = "Película";
                        Rankear(película.titulo, película.calificación, Película);
                        MessageBox.Show("Se ha calificado la película");
                        CalificaciónPelículaSeleccionada.Text = película.Avg_calificación.ToString();
                    }
                }
            }
            comboBoxCalificaciónPelícula.Visible = false;
            buttonConfirmarCalificaciónPelícula.Visible = false;
        }
        string[] archivo, Ruta;

        private void buttonVolverAgregarCancion_Click(object sender, EventArgs e)
        {
            stackPanels.Add(panels["MenuPanel"]);
            ShowLastPanel();
        }

        private void buttonConfrimarAgregarCancion_Click(object sender, EventArgs e)
        {
            string nombre = textBoxNombreCancion.Text;
            string artista = textBoxArtistaCancion.Text;
            string album = textBoxAlbumCancion.Text;
            string duracion = textBoxDuracionCancion.Text;
            string genero = textBoxGeneroCancion.Text;
            string espacio = textBoxEspacioCancion.Text;
            string resolucion = textBoxResoluciónCancion.Text;
            if (labelRutaCancion.Visible == true)
            {
                string ruta = labelRutaCancion.Text;
                OnAgregarCancionClicked(nombre, artista, album, genero, "2016", "0", "0", duracion, resolucion, espacio, ruta);
            }
            else
            {
                labelDebeAgregarArchivoCancion.Text = "Debe ingresgar un archivo para subir la canción";
                labelDebeAgregarArchivoCancion.Visible = true;
            }
        }

        private void buttonIrAgregarCancion_Click(object sender, EventArgs e)
        {
            labelRutaCancion.Visible = false;
            labelFaltanDatosCancion.Visible = false;
            labelDebeAgregarArchivoCancion.Visible = false;
            stackPanels.Add(panels["AgregarCancionpanel"]);
            ShowLastPanel();
        }

        private void btnBuscarArchivoCancion_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                archivo = openFileDialog.SafeFileNames;
                Ruta = openFileDialog.FileNames;

                for (int i = 0; i < archivo.Length; i++)
                {
                    ruta = Ruta[i];
                    labelRutaCancion.Text = Ruta[i];
                    labelRutaCancion.Visible = true;
                }
            }
        }
    }
}
