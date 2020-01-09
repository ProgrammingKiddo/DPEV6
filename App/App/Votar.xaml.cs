using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Votar : ContentPage
    {
        public Votar(string[] resultado, string a, int condicion, int poscoma,int Nv)
        {
            InitializeComponent();
            try
            {
              
                if (condicion >= 0)
                {
                    int i = 0;
                    while (i < 7)
                    {
                        poscoma = a.IndexOf(",");
                        resultado[i] = a.Substring(0, poscoma);
                        a = a.Substring(poscoma + 1);
                        i++;
                    }

                    Button siguiente = new Button {Text = "siguiente" };
                    Button button = new Button
                    {
                        Text = resultado[6] + " " + resultado[4] + " " + resultado[5],

                    };
                    if (Nv == 0)
                    {
                        button.Clicked += async (sender, args) => await Navigation.PushAsync(new Votaciones(resultado));
                    }

                    if (Nv == 2) { button.Clicked += async (sender, args) => await Navigation.PushAsync(new chartpage(resultado)); }

                    if (condicion > 1)
                    {
                        
                        siguiente.Clicked += async (sender, args) => await Navigation.PushAsync(new Votar(resultado, a, condicion, poscoma, Nv));

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


    }
}

