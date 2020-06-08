using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entrega2
{
    public class UsuarioPremium : Usuario
    {
        public UsuarioPremium(string nombre_usuario, string mail, string contraseña, string tipo_usuario) : base(nombre_usuario, mail, contraseña, tipo_usuario)
        {
            this.Mail = mail;
            this.Contraseña = contraseña;
            this.Nombre_usuario = nombre_usuario;
            this.Tipo_usuario = "Premium";
        }
    }
    
}
