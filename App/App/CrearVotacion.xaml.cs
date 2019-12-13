using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CrearVotacion : ContentPage
	{
		public CrearVotacion ()
		{
			InitializeComponent ();
        }

        private async void VotSimple(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VotacionSimple());
        }
        private async void VotCompleja(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VotacionCompleja());
        }
    }
}