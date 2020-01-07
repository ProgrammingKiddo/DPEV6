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



            Label label = new Label
            {
                Text = resultado[6] + "," + resultado[1] + "," + resultado[2] + "," + resultado[3],
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            sl.Children.Add(label);

        }
        public void evento(object sender, EventArgs e, string[] resultado, int res)
        {

            string[] datos = new string[100];

            datos[0] = resultado[0];
            datos[1] = res.ToString();

            Conectar.Union(6, datos);
        }

    }
}