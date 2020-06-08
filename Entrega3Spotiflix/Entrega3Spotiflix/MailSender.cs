using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Entrega3Spotiflix.CustomsEvenArgs;

namespace Entrega3Spotiflix
{
    public class MailSender
    {
        public void OnRegistered(object source, RegisterEventArgs e)
        {
            Thread.Sleep(2000);
            Console.WriteLine($"\n{e.Email}: \n Gracias por registrarte, {e.Username}!\n");
            Thread.Sleep(2000);
        }

        public void OnPasswordChanged(object source, ChangePasswordEventArgs e)
        {
            Thread.Sleep(2000);
            Console.WriteLine($"\nCorreo enviado a {e.Email}:  \n {e.Username}, te notificamos que la contrasena de tu cuenta PlusCorp ha sido cambiada. \n");
            Thread.Sleep(2000);
        }
        public delegate void SentEmailEventHandler(object source, EventArgs args);
        public event SentEmailEventHandler EmailSent;
        protected virtual void OnEmailSent()
        {
            EmailSent(this, new EventArgs());
        }
    }
}