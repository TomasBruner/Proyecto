using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entrega3Spotiflix
{
    public class Artista : Person
    {
        private int cantidad_reproducciones = 0;
        public Artista(string nombre, string apellido, string sexo, int age, string nacionalidad) : base(nombre, apellido, sexo, age, nacionalidad)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Sexo = sexo;
            this.Age = age;
            this.Nacionalidad = nacionalidad;
        }
        public int Cantidad_reproducciones
        {
            get => cantidad_reproducciones; set => Cantidad_reproducciones = value;
        }
    }
}