using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entrega3Spotiflix.CustomsEvenArgs;

namespace Entrega3Spotiflix.Controllers
{
    class UserController
    {
        AppForm view;

        public UserController(Form view)
        {
            initialize();
            this.view = view as AppForm;
            this.view.LoginButtonClicked += OnLoginButtonClicked;
            this.view.RegisterClicked += OnRegisterClicked;
            this.view.UserChecked += OnUserChecked;
        }

        public bool OnRegisterClicked(object sender, RegisterEventArgs e)
        {
            string result = Archivos.AddUser(new List<string>()
                {e.Username, e.Email, e.Password, Convert.ToString(DateTime.Now),e.Tipo_usuario });
            if (result == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool OnLoginButtonClicked(object sender, LoginEventArgs e)
        {
            Usuario result = null;
            result = Archivos.Usuarios.Where(t =>
               t.Nombre_usuario.ToUpper().Contains(e.UsernameText.ToUpper())).FirstOrDefault();
            if (result is null)
            {
                return false;
            }
            else
            {
                return result.CheckCredentials(e.UsernameText, e.PasswordText);
            }
        }

        public void OnUserChecked(object sender, LoginEventArgs e)
        {
            Usuario user = null;
            user = Archivos.Usuarios.Where(t =>
               t.Nombre_usuario.ToUpper().Contains(e.UsernameText.ToUpper())).FirstOrDefault();
        }

        public void initialize()
        {
            //Serialización
        }
    }
}
