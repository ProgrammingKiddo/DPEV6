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
    public partial class VotacionesActivas : ContentPage
    {
        public VotacionesActivas()
        {
            InitializeComponent();
            hola();
        }
        public void hola()
        {
            string a = Conectar.Union(3, null);


            Button btnCliente = new Button();
            btnCliente.Text = a;
            sl.Children.Add(btnCliente);
            //Aquí quiero mostrar mi botón en la pantalla

        }
    }
}