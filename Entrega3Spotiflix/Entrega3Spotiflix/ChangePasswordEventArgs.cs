using System;
using System.Collections.Generic;
using System.Text;

namespace Entrega3Spotiflix
{
    public class ChangePasswordEventArgs : EventArgs
    {
        public string Email { get; set; }
        public string Number { get; set; }
        public string Username { get; set; }
    }
}
