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
	public partial class VotacionSimple : ContentPage
	{
       
		public VotacionSimple ()
		{
			InitializeComponent ();
        }

        private async void MostrarEditarOpciones(object sender, EventArgs e) //Manda a la lista de opciones
        {
            await Navigation.PushAsync(new MEopciones());
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
            envio[1] = //.Text;  //nombre opcion                        
            envio[3] = PLCfechaini.ToString();//fecha inicio            
            envio[4] = PLCfechafin.ToString();//fecha fin        
            envio[5] = "ESI";// .Text;     //facultad

            string acceso = Conectar.Union(2, envio);// el 2 es para la informacion votacion


        }
    }
}