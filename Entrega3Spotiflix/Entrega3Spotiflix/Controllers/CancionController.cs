using Entrega3Spotiflix.CustomsEvenArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entrega3Spotiflix.Controllers
{
    class CancionController
    {
        AppForm view;

        public CancionController(Form view)
        {
            initialize();
            this.view = view as AppForm;
            this.view.AgregarCancionClicked += OnAgregarCancionClicked;
            this.view.CancionChecked += OnCancionChecked;
        }
        public void initialize()
        {
            //Serialización
        }
        public bool OnAgregarCancionClicked(object sender, AgregarCancionEventArgs e)
        {
            string result = Archivos.AddCancion(new List<string>()
                {e.Nombre, e.Artista, e.Album, e.genero , e.añoPublicacion, Convert.ToString(e.Reproducciones),Convert.ToString(e.avg_calificacion), e.Duracion, e.Resolucion, e.Espacio, e.URL});
            if (result == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void OnCancionChecked(object sender, AgregarCancionEventArgs e)
        {
            Canción cancion = null;
            cancion = Archivos.cancionesApp.Where(t =>
                t.Titulo.ToUpper().Contains(e.Nombre.ToUpper())).FirstOrDefault();
        }
    }
}
