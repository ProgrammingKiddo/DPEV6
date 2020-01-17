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
        List<string> carreras;
        public VotacionSimple()
        {
            InitializeComponent();
            InitMenuDesplegable();
        }
        private void InitMenuDesplegable()
        {
            try
            {

                carreras = new List<string>();
                carreras.Add("GRADO EN INGENIERÍA INFORMÁTICA");
                carreras.Add("GRADO EN INGENIERÍA EN TECNOLOGÍAS INDUSTRIALES");
                carreras.Add("GRADO EN INGENIERÍA ELECTRÓNICA INDUSTRIAL");
                carreras.Add("GRADO EN INGENIERÍA AEROESPACIAL");
                carreras.Add("GRADO EN INGENIERÍA MECÁNICA");
                carreras.Add("GRADO EN INGENIERÍA DEL DISEÑO INDUSTRIAL Y DESARROLLO DEL PRODUCTO");
                carreras.Add("GRADO EN INGENIERÍA ELÉCTRICA");
                carreras.Add("DOBLE GRADO EN INGENIERÍA ELÉCTRICA E INGENIERÍA ELECTRÓNICA INDUSTRIAL");
                carreras.Add("DOBLE GRADO EN INGENIERÍA MECÁNICA E INGENIERÍA ELÉCTRICA");
                carreras.Add("DOBLE GRADO EN INGENIERÍA MECÁNICA E INGENIERÍA EN DISEÑO INDUSTRIAL Y DESARROLLO DEL PRODUCTO");
                //carreras.Add((string)App.Current.Properties["name"]);

                foreach (var degree in carreras)
                {
                    pickercarrera.Items.Add(degree);
                }
            }
            catch { }
        }





        private async void PickerCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
        }


        private async void Btnpage1_Clicked(object sender, EventArgs e)//boton crear votacion
        {
            var answer = await DisplayAlert("Alerta", "¿Estás seguro de que quieres crear la votación?", "Si", "No");
            if (answer == true)
            {
                int positionCarrera = pickercarrera.SelectedIndex;
                string[] envio = new string[100];
                string acceso = null;
                envio[0] = PLCnombre.Text;//nombre votacion
                envio[1] = "A FAVOR";  //nombre opcion1
                envio[2] = "EN CONTRA";  //nombre opcion2
                envio[3] = "ABSTENCION";   //nombre opcion3                      
                envio[4] = PLCfechaini.Date.ToShortDateString();//fecha inicio            
                envio[5] = PLCfechafin.Date.ToShortDateString();//fecha fin        
                envio[6] = carreras[positionCarrera];
                envio[7] = (string)App.Current.Properties["name"];
                envio[8] = PLChorafin.ToString();


                acceso = Conectar.Union(2, envio);// el 2 es para la informacion votacion
                await DisplayAlert("", "Su votación se ha creado correctamente", "Aceptar");
                await Navigation.PushAsync(new MenuPage(1));
            }

        }
    }
}