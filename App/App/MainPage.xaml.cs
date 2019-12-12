using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Novell.Directory.Ldap;
using System.Net;
using System.Net.Sockets;



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

        private async void Btnpage1_Clicked(object sender, EventArgs e)
        {
            try
            {
                string usuario = PLCusuario.Text;
                string contraseña = btncontrasena.Text;
                /*string acceso =*/ Conectar.Union(usuario, contraseña);
                int a = usuario.IndexOf("u");
                int b = contraseña.IndexOf("c");
                /*int a = int.Parse(acceso);
                if (a == 1)*/
                if (a!=-1 && b!=-1)
                await Navigation.PushAsync(new MenuPage());
                /*else
                {
                    await DisplayAlert("Alerta", "el usuario o la contraseña no son correctos", "ok");
                }*/
            }catch(Exception e2)
            {
        
                await DisplayAlert("Alerta", "el usuario o la contraseña no son correctos", "ok");
            }
        }
        /* private void Btnpage1_Clicked(object sender, EventArgs e)
         {
             lblRes.Text = "Hola" + PLCusuario.Text;

         }*/
    }
    public class Conectar
    {
        public static /*string*/void  Union(string usuario,string contraseña)
        {
            //string acceso="";
            try
            {
                Socket listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
                IPEndPoint connect = new IPEndPoint(IPAddress.Parse("192.168.1.81"), 5000);

                listen.Connect(connect);
                
                //enviar
                byte[] enviar_info = new byte[100];
                string data = usuario+","+contraseña;
               
                enviar_info = Encoding.Default.GetBytes(data); 
                 listen.Send(enviar_info);
                //recibir
               /* byte[] recibir_info = new byte[100];
                int array_size = 0;

                array_size = listen.Receive(recibir_info, 0, recibir_info.Length, 0);
                 Array.Resize(ref recibir_info, array_size);
                data = Encoding.Default.GetString(recibir_info);

               
                int poscoma = data.IndexOf(",");
                 acceso = data.Substring(0, poscoma);
            
                return acceso;
                */
                Console.ReadKey();
            }catch(Exception e)
            {
                Console.WriteLine("quiere petar");
            }
           // return acceso;
        }

    }

}


