using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelCursos : ContentPage
    {
        public SelCursos()
        {
            InitializeComponent();
            InicializarCursos();
        }

        void InicializarCursos()
        {
            List<Cursos> cursos = new List<Cursos>
        {
                new Cursos{ Numero = "Primer Curso"},
                new Cursos{ Numero = "Segundo Curso"},
                new Cursos{ Numero = "Tercer Curso"},
                new Cursos{ Numero = "Cuarto Curso"},
                new Cursos{ Numero = "Quinto Curso"},
                new Cursos{ Numero = "Sexto Curso"}
        };

            ListaCursos.ItemsSource = cursos;
        }

        async void ConfirmarCursos(object sender, EventArgs e)
        {
            //Guardar todos los seleccionados
            var answer = await DisplayAlert("Alerta", "¿Estás seguro de que quieres seleccionar estos cursos?", "Si", "No");
            if (answer == true)
            {
                await Navigation.PopAsync();
            }
        }
    }

    public class Cursos
    {
        public string Numero { get; set; }
    }
}