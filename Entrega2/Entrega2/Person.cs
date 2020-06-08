using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entrega2
{
    public class Person
    {
        protected string nombre;
        protected string apellido;
        protected string sexo;
        protected int age;
        protected string nacionalidad;
        public Person(string nombre, string apellido, string sexo, int age, string nacionalidad)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Sexo = sexo;
            this.Age = age;
            this.Nacionalidad = nacionalidad;
        }
        public string Nombre
        {
            get => nombre; set => nombre = value;
        }
        public string Apellido
        {
            get => apellido; set => apellido = value;
        }
        public int Age
        {
            get => age; set => age = value;
        }
        public string Nacionalidad
        {
            get => nacionalidad; set => nacionalidad = value;
        }
        public string Sexo
        {
            get => sexo; set => nombre = sexo;
        }
    }

}
