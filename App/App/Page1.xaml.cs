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
    public partial class Page1 : ContentPage
    {


        public Page1()
        {
            InitializeComponent();
        }

        private async void Votaciones(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CrearVotacion());
        }
        private async void VotAct(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VotacionesActivas());
        }
        private async void VotFut(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VotacionesFuturas());
        }
        private async void Result(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Resultados());
        }
    }
}