using Entrega3Spotiflix.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entrega3Spotiflix
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Archivos.Usuarios.Add(new Usuario("aa", "aa", "aa", "Gratis"));
            Archivos.Usuarios.Add(new Usuario("bb", "bb", "bb", "Premium"));
            Archivos.Lista_usuarios[0] = new List<string>() { "aa", "aa", "aa", Convert.ToString(DateTime.Now), "Gratis" };
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppForm appform = new AppForm();
            UserController userController = new UserController(appform);
            CancionController cancionController = new CancionController(appform);
            Application.Run(appform);
        }
    }
}