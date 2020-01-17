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
            #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            Device.OpenUri(new Uri(url));           //LOS COMENTARIOS DE ARRIBA Y ABAJO SON PARA QUE NO SALTE 
                                                    //EL WARNING YA QUE ESTA OBSOLETA ESTA FORMA DE HACERLO
            #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        }
       
        
            private async void Btnpage1_Clicked(object sender, EventArgs e)//boton acceder
            {
                try
                {
                    string[] envio = new string[100];
                    string acceso = null;                    
                    string  usu = PLCusuario.Text;
                
                    envio[0] = Cifrado.Cifrar(PLCusuario.Text);//usuario                       
                    envio[1] = Cifrado.Cifrar(btncontrasena.Text); //contraseña     
                  
                    acceso = Conectar.Union(1, envio); //llama a la funcion Union el 1 es para ldap
                    int c = int.Parse(acceso);
                    if (c == 1||c==2)
                    {       App.Current.Properties["name"] = PLCusuario.Text;
                                
                             if (c==1)
                                await Navigation.PushAsync(new Entry_form());
                            else
                            {
                                if (int.Parse(PLCusuario.Text.Substring(8)) == 0)
                                    await Navigation.PushAsync(new MenuPage(0));
                                else
                                    await Navigation.PushAsync(new MenuPage(1));
                            }
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


    




