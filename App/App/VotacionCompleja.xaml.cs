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

        private async void SeleccionGrados(object sender, EventArgs e) //Manda a la lista de seleccion de grados
        {
            await Navigation.PushAsync(new SelGr());
        }

        private async void SeleccionCursos(object sender, EventArgs e) //Manda a la lista de seleccion de cursos
        {
            await Navigation.PushAsync(new SelCursos());
        }

        private async void Btnpage1_Clicked(object sender, EventArgs e)//boton crear votacion
        {
            string[] envio = new string[100];
            
            envio[0] = PLCnombre.Text;//nombre votacion
            envio[1] = PLCcandidato1.Text;//.Text;  //nombre opcion1
            envio[2] = PLCcandidato2.Text;//.Text;  //nombre opcion2
            envio[3] = PLCcandidato3.Text;// .Text;  //nombre opcion3                                   
            envio[4] = PLCfechaini.ToString();//fecha inicio            
            envio[5] = PLCfechafin.ToString();//fecha fin        
            envio[6] ="ESI" ;// .Text;     //facultad

            string acceso = Conectar.Union(2,envio);// el 2 es para la informacion votacion


        }
    }
}