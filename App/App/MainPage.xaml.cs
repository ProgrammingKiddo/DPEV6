using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Novell.Directory.Ldap;



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
            string usuario = PLCusuario.Text;
            string contraseña = btncontrasena.Text;

            try
            {
                string ldapHost = "ldap.uca.es";
                int ldapPort = 389;
                string loginDN = "CN=" + usuario + ",dc=uca,dc=es";
                LdapConnection conn = new LdapConnection();

                conn.Connect(ldapHost, ldapPort);
                conn.Bind(loginDN, contraseña);
                if(conn.Bound==true)
                await Navigation.PushAsync(new Page1());
                else
                    await DisplayAlert("Alerta", "Tu usuario o contraseña es incorrecto, vuelva a escribirlo", "Aceptar");

            }
            catch (LdapException e1)
            {
                await DisplayAlert("Alerta", "Tu usuario o contraseña es incorrecto, vuelva a escribirlo", "Aceptar");

                return;
            }
    }
        /* private void Btnpage1_Clicked(object sender, EventArgs e)
         {
             lblRes.Text = "Hola" + PLCusuario.Text;

         }*/
    }
   

}


