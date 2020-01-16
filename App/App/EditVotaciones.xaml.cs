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
	public partial class EditVotaciones : ContentPage
	{
		public EditVotaciones (string[] resultado)
		{
            try
            {
                InitializeComponent();

                Label tituloEdit = new Label { Text = "  Editar Votación" };
                tituloEdit.FontSize = 24;
                tituloEdit.FontAttributes = FontAttributes.Bold;
                Label descripcionEdit = new Label { Text = "   Introduzca la nueva la Fecha de Finalización" };
                descripcionEdit.FontSize = 16;
                Label nombreVotacion1 = new Label { Text = "   Votación: "};
                nombreVotacion1.FontAttributes = FontAttributes.Bold;
                Label nombreVotacion2 = new Label { Text = "                       " + resultado[6] };
                Label fechaActualFinal = new Label{Text = "   La actual fecha final de la votación es: " + resultado[5]};
                Label fechaNuevaFinal = new Label { Text = "   Nueva fecha a introducir: " };
                sl.Children.Add(tituloEdit, 0, 0);
                sl.Children.Add(descripcionEdit, 0, 1);
                sl.Children.Add(nombreVotacion1, 0, 3);
                sl.Children.Add(nombreVotacion2, 0, 3);
                sl.Children.Add(fechaActualFinal, 0, 6);
                sl.Children.Add(fechaNuevaFinal, 0, 10);

                DatePicker seleccion = new DatePicker();
                seleccion.MinimumDate = Convert.ToDateTime(resultado[5]);
                seleccion.Format = "dd-MM-yyyy";
                seleccion.VerticalOptions = LayoutOptions.CenterAndExpand;
                seleccion.Margin = 10;
                sl.Children.Add(seleccion, 0, 12);

                Button aceptar = new Button { Text = "Aceptar" };
                aceptar.CornerRadius = 25;
                aceptar.TranslationX = 38;
                aceptar.TranslationY = -15;
                aceptar.TextColor = Color.White;
                aceptar.BackgroundColor = Color.Orange;
                aceptar.Clicked += (sendr, EventArgs) => { Accept_Clicked(sendr, EventArgs, resultado, seleccion);};

                sl.Children.Add(aceptar, 0, 20);



            }catch (Exception ex) { DisplayAlert("", ex.Message, "ok"); }
        }

        private async void Accept_Clicked(object sender, EventArgs e, string[] resultado, DatePicker seleccion)
        {
            string fechanueva = seleccion.Date.ToShortDateString();

            var answer = await DisplayAlert("Alerta", "¿Está seguro de que desea editar la fecha final de la votación a " + fechanueva + " ?", "Sí", "No");
            if(answer == true)
            {
                string[] envio = new string[100];
                string acceso = null;
                envio[0] = resultado[0];//IdVotacion            
                envio[1] = fechanueva;//fecha fin

                acceso = Conectar.Union(10, envio);// el 10 es para el edit de info
                await DisplayAlert("", "Su votación se ha editado correctamente", "Aceptar");
                await Navigation.PushAsync(new MenuPage(1));
            }
        }
	}
}