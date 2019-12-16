using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;



namespace App
{


    public partial class MainPage : ContentPage
    {
        

        public ICommand TapCommand => new Command<string>(OpenBrowser);

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }



        void OpenBrowser(string url)
        {
            Device.OpenUri(new Uri(url));
        }
       
        
            private async void Btnpage1_Clicked(object sender, EventArgs e)//boton acceder
            {
                try
                {
                string[] envio = new string[100];
                string[] acceso = new string[100];
                envio[0] =Cifrado.Cifrar(PLCusuario.Text);//usuario                       
                    envio[1] = Cifrado.Cifrar(btncontrasena.Text); //contraseña     
                
                     acceso = Conectar.Union(1, envio); //llama a la funcion Union el 1 es para ldap
                    int c = int.Parse(acceso[0]);
                    if (c == 1)
                    {
                        await Navigation.PushAsync(new MenuPage());
                    }
                    else
                    {
                        await DisplayAlert("Alerta", "el usuario o la contraseña no son correctos", "ok");
                    }
                }
                catch (Exception e2)
                {
                    await DisplayAlert("Alerta", e2.Message, "ok");
                }
            }
        }
    }


    




