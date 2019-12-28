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
	public partial class Votaciones : ContentPage
	{
		public Votaciones (string[]resultado)
		{
			InitializeComponent ();
            Label label = new Label
            {
                Text =resultado[6]+ ","+resultado[1] + "," + resultado[2] + "," + resultado[3] ,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            sl.Children.Add(label);
        }
	}
}