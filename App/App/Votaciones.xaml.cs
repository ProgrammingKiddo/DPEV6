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
            try
            {
                InitializeComponent();

                /* Necesario para obtener el rol del user que usa la app: Alumno, PDI, PAS */
                string[] rol = new string[100];
                rol[0] = resultado[9];
                string rolUser = Conectar.Union(12, rol);
                rolUser = rolUser.Remove(rolUser.Length - 1); //removemos la coma
                resultado[10] = rolUser;
                //DisplayAlert("Atención",resultado[10],"Aceptar");
                /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */



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

                    string a = Conectar.Union(7, resultado);
                    //DisplayAlert("1", a.Substring(0,1), "ok");
                    if (a.Substring(0, 1) == "2")
                    {
                    part1.Clicked += (sendr, EventArgs) => { evento(sendr, EventArgs, resultado, 1); };
                    part2.Clicked += (sendr, EventArgs) => { evento(sendr, EventArgs, resultado, 2); };
                    part3.Clicked += (sendr, EventArgs) => { evento(sendr, EventArgs, resultado, 3); };

                }
                else { funcion(resultado); }
            }
            catch(Exception e) { DisplayAlert("Error en Votaciones: ",e.Message,"ok" ); }
        }

        public async void evento(object sender, EventArgs e, string[] resultado,int res)
        {

            string[] datos = new string[100];

            datos[0] = resultado[0];           
            datos[1] = res.ToString();
            datos[2] = resultado[9];
            datos[3] = resultado[10];
            Conectar.Union(4,datos);
            if (resultado[9] != "u00000000")
                await Navigation.PushAsync(new MenuPage(0));
            else
                await Navigation.PushAsync(new MenuPage(1));
        }


        public async void funcion(string[]resultado)
        {
            await DisplayAlert("Aviso", "Usted ya ha votado en esta votacion", "ok");
            if (resultado[7]!= "u00000000")
                await Navigation.PushAsync(new MenuPage(0));
            else
                await Navigation.PushAsync(new MenuPage(1));
        }

    }
}