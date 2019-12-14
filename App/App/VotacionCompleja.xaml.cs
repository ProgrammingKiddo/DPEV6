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
    public partial class VotacionCompleja : ContentPage
    {
        public VotacionCompleja()
        {
            InitializeComponent();
            BindingContext = this;
        }
        private async void Btnpage1_Clicked(object sender, EventArgs e)
        {
            string[] envio = new string[100];
            
            envio[0] = PLCnombre.Text;//nombre votacion
            envio[1] = "A FAVOR";//.Text;  //nombre opcion1
            envio[2] ="EN CONTRA" ;//.Text;  //nombre opcion2
            envio[3] ="ABSTENCION" ;// .Text;  //nombre opcion3                                   
            envio[4] = PLCfechaini.Text;//fecha inicio            
            envio[5] = "21/21/2121" ;//.Text;     //fecha fin        
            envio[6] ="ESI" ;// .Text;     //facultad

            string acceso = Conectar.Union(2,envio);


    }
    }
}