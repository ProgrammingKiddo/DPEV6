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
        public Votaciones(string[] resultado)
        {
            InitializeComponent();

            
            

            Label label = new Label
            {
                Text = resultado[6] + "," + resultado[1] + "," + resultado[2] + "," + resultado[3],
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
            sl.Children.Add(label);
            part1.Text = resultado[1];
            part2.Text = resultado[2];
            part3.Text = resultado[3];


            part1.Clicked += (sendr, EventArgs) => { evento(sendr, EventArgs, resultado,1); };
            part2.Clicked += (sendr, EventArgs) => { evento(sendr, EventArgs, resultado, 2); };
            part3.Clicked += (sendr, EventArgs) => { evento(sendr, EventArgs, resultado, 3); };
        }

        public void evento(object sender, EventArgs e, string[] resultado,int res)
        {

            string[] datos = new string[100];

            datos[0] = resultado[0];           
            datos[1] = res.ToString();
          
            Conectar.Union(4,datos);

        }


    }
}