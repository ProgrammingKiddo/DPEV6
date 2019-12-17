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
    public partial class SelGr : ContentPage
    {
        public SelGr()
        {
            InitializeComponent();
            InicializarGrados();
        }

        void InicializarGrados()
        {
            List<Grados> grados = new List<Grados>
        {
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Informatica", Codigo = "1725", Facultad = "Escuela Superior de Ingeniería"}
        };

            ListaGrados.ItemsSource = grados;
        }

        async void ConfirmarGrados(object sender, EventArgs e)
        {
            //Guardar todos los seleccionados
            var answer = await DisplayAlert("Alerta", "¿Estás seguro de que quieres seleccionar estos grados?", "Si", "No");
            if (answer == true)
            {
                await Navigation.PopAsync();
            }
        }

    }

    public class Grados
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Facultad { get; set; }
    }


}