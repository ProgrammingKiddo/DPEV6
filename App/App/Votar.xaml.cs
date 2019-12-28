using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Votar : ContentPage
    {
        public Votar()
        {
            InitializeComponent();
            hola();
        }
        public void hola()
        {

            try
            {
                string[] resultado = new string[100];
               
                int condicion = 2;
                string a = Conectar.Union(3, null);
                int poscoma = a.IndexOf(",");
                condicion = int.Parse(a.Substring(0, poscoma)) / 10;
                a = a.Substring(poscoma + 1);

                while (condicion >= 0)
                {
                    int i = 0;
                    while (i < 7)
                    {
                        poscoma = a.IndexOf(",");
                        resultado[i] = a.Substring(0, poscoma);
                        a = a.Substring(poscoma + 1);
                        i++;
                    }
                    
                    Button button = new Button
                    {
                        Text = resultado[6] + " " + resultado[4] + " " + resultado[5],
                       
                    };
                    button.Clicked+= async (sender, args) => await Navigation.PushAsync(new Votaciones(resultado));
                    sl.Children.Add(button, 0, condicion);
                    
                    condicion--;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("a", ex.Message, "ok");
            }
        }

        

       


    }
}

