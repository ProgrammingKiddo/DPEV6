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
	public partial class Votar : ContentPage
	{
		public Votar ()
		{
			InitializeComponent ();
             
        }

        private async void crear(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CrearVotacion());
        }
    }
}