using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3Spotiflix
{
    public class UsuarioPremium : Usuario
    {
        public UsuarioPremium(string nombre_usuario, string email, string contraseña, string tipo_usuario) : base(nombre_usuario, email, contraseña, tipo_usuario)
        {
            this.Email = email;
            this.Contraseña = contraseña;
            this.Nombre_usuario = nombre_usuario;
            this.Tipo_usuario = "Premium";
        }
    }

}
