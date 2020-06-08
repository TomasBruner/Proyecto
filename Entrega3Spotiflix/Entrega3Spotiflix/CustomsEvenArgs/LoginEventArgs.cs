using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3Spotiflix.CustomsEvenArgs
{
    public class LoginEventArgs : EventArgs
    {
        public string UsernameText { get; set; }
        public string PasswordText { get; set; }
    }
}

