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
	public partial class Entry_form : ContentPage
	{
        List<string> cursos;
        List<string> carreras;

        public Entry_form()
		{
			InitializeComponent();
            InitMenuDesplegable();
		}

        private void InitMenuDesplegable()
        {
            try
            {
                cursos = new List<string>();
                cursos.Add("Primer Curso");
                cursos.Add("Segundo Curso");
                cursos.Add("Tercer Curso");
                cursos.Add("Cuarto Curso");
                cursos.Add("+Cuarto Curso");

                foreach (var curse in cursos)
                {
                    pickercurso.Items.Add(curse);
                }

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
                    pickercarrera.SelectedIndex = 0;
                }
            }
            catch { }
        }

        private async void PickerCurso_SelectedIndexChanged(object sender, System.EventArgs e)
        {
           // throw new NotImplementedException();
        }

        private async void PickerCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        private async void SendButton_Clicked(object sender, EventArgs e)
        {
            try { 
            string[] envio = new string[100];
            string acceso = null;
            int positionCurso = pickercurso.SelectedIndex;
            int positionCarrera = pickercarrera.SelectedIndex;

            if (positionCurso > -1 && positionCarrera > -1)
            {
                envio[0] = carreras[positionCarrera];
                envio[1] = cursos[positionCurso];

                //Console.WriteLine(envio[0]);
                //Console.WriteLine(envio[1]);

            }
            
            acceso = Conectar.Union(9, envio);
            if (int.Parse(acceso.Substring(8)) == 0)
            {
                await Navigation.PushAsync(new MenuPage(0));
            }
            else
            {
                await Navigation.PushAsync(new MenuPage(1));
            }
        }
            catch(Exception ed) { await DisplayAlert("Alerta-mainpage", ed.Message, "ok"); }

        }
    }
}