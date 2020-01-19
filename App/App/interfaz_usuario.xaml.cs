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
	public partial class interfaz_usuario : ContentPage
	{
        public interfaz_usuario()
        {
            InitializeComponent();
        }
            private async void Votaciones(object sender, EventArgs e)
        {
            try {
                    string[] resultado = new string[100];
                    resultado[0] = (string)App.Current.Properties["name"];

                    string a = Conectar.Union(3, resultado);
                    int poscoma = a.IndexOf(",");
                    int condicion = int.Parse(a.Substring(0, poscoma));
                    a = a.Substring(poscoma + 1);

                    if (condicion == 0) { await DisplayAlert("Alerta", "No hay votaciones", "ok"); }
                    else { await Navigation.PushAsync(new Votar(resultado, a, condicion, poscoma, 0, (string)App.Current.Properties["name"])); }

                
        }
            catch(Exception ex) { await DisplayAlert("Error en Votaciones: ", ex.Message, "ok"); }
        }
            private async void VotFut(object sender, EventArgs e)
            {
            try
            {
                    string[] resultado = new string[100];
                    resultado[0] = (string)App.Current.Properties["name"];
                    string a = Conectar.Union(5, resultado);
                    int poscoma = a.IndexOf(",");
                    int condicion = int.Parse(a.Substring(0, poscoma));
                    a = a.Substring(poscoma + 1);

                    if (condicion == 0) { await DisplayAlert("Alerta", "No hay votaciones", "ok"); }
                    else { await Navigation.PushAsync(new Votar(resultado, a, condicion, poscoma, 1, (string)App.Current.Properties["name"])); }
                
            }
            catch (Exception ex) { await DisplayAlert("Error en Votaciones: ", ex.Message, "ok"); }
        }
        
            private async void Result(object sender, EventArgs e)
            {
            try
            {
                    string[] resultado = new string[100];
                    resultado[0] = (string)App.Current.Properties["name"];
                    string a = Conectar.Union(6, resultado);
                    int poscoma = a.IndexOf(",");
                    int condicion = int.Parse(a.Substring(0, poscoma));
                    a = a.Substring(poscoma + 1);
                    if (condicion == 0) { await DisplayAlert("Alerta", "No hay votaciones", "ok"); }
                    else { await Navigation.PushAsync(new Votar(resultado, a, condicion, poscoma, 2, (string)App.Current.Properties["name"])); }
                
            }
            catch (Exception ex) { await DisplayAlert("Error en Votaciones: ", ex.Message, "ok"); }
        }
        }
    }
	
