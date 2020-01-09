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
	public partial class Resultados : ContentPage
	{
		public Resultados (string[] resultado)
		{
			InitializeComponent ();



            Button button = new Button
            {
                Text = "Viva San Fernando"

            };
            

            button.Clicked += async (sender, args) => await Navigation.PushAsync(new MainPage());
            sl.Children.Add(button);
        }
    }
}