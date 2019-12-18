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
                new Grados{ Nombre = "Grado en Ingeniería Informática", Codigo = "21714", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingeniería Aeroespacial", Codigo = "21716", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingeniería en Diseño Industrial y Desarrollo del Producto", Codigo = "21717", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingeniería Eléctrica", Codigo = "21718", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingeniería en Electrónica Industrial", Codigo = "21719", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingenieria Mecánica", Codigo = "21720", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "Grado en Ingeniería en Tecnologías Industriales", Codigo = "21721", Facultad = "Escuela Superior de Ingeniería"},
                new Grados{ Nombre = "", Codigo = "", Facultad = ""},
                new Grados{ Nombre = "", Codigo = "", Facultad = ""}
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