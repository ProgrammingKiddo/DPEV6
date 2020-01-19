using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Votar : ContentPage
    {
        public Votar(string[] resultado, string a, int condicion, int poscoma,int Nv,string usu)
        {
            InitializeComponent();
            try
            {
              
                if (condicion >= 0)
                {
                    int i = 0;
                    while (i < 10)
                    {
                        poscoma = a.IndexOf(",");
                        resultado[i] = a.Substring(0, poscoma);
                        a = a.Substring(poscoma + 1);
                        i++;
                    }
                    resultado[9]=usu;

                    Button siguiente = new Button {Text = "siguiente" };
                    Button edit = new Button { Text = "editar votación" };
                    Button delete = new Button { Text = "eliminar votación" };
                    //Button anterior = new Button { Text = "anterior" };
                    Button button = new Button
                    {
                        Text = resultado[6] + " " + resultado[4] + " " + resultado[5],
                    };
                    
                    if (Nv == 0) {button.Clicked += async (sender, args) => await Navigation.PushAsync(new Votaciones(resultado));}

                    if (Nv == 2) {button.Clicked += async (sender, args) => await Navigation.PushAsync(new chartpage(resultado));}

                    if (Nv < 2)
                    {
                        if ((resultado[9].Equals(resultado[8])) || (resultado[9].Substring(resultado[9].Length-1,1) == "0"))
                        {
                            sl.Children.Add(edit, 0, 8);
                            sl.Children.Add(delete, 0, 7);
                            edit.Clicked += async (sender, args) => await Navigation.PushAsync(new EditVotaciones(resultado));
                            delete.Clicked += (sendr, EventArgs) => { Delete_Clicked(sendr, EventArgs, resultado); };
                        }
                    }

                    if (condicion > 1)
                    {
                        
                        siguiente.Clicked += async (sender, args) => await Navigation.PushAsync(new Votar(resultado, a, condicion, poscoma, Nv, (string)App.Current.Properties["name"]));
                        //anterior.Clicked 

                        sl.Children.Add(siguiente, 0, 1);
                            condicion--;
                    }
                                  
                        sl.Children.Add(button);
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("a", ex.Message, "ok");
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e, string[] resultado)
        {
            var answer = await DisplayAlert("Alerta", "¿Está seguro de que desea eliminar la votación permanentemente?", "Sí", "No");
            if(answer == true)
            {
                string[] envio = new string[100];
                string acceso = null;
                envio[0] = resultado[0]; //IdVotacion

                acceso = Conectar.Union(11, envio);
                await DisplayAlert("", "La votación ha sido eliminada", "Aceptar");
                await Navigation.PushAsync(new MenuPage(1));
            }
        }
    }
}

