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
	public partial class Votar : ContentPage
	{
		public Votar ()
		{
			InitializeComponent ();
            hola();
        }
        public void hola()
        {
            try
            {
                string[] resultado = new string[10];
                int condicion = 2;
                string a = Conectar.Union(3, null);
                int poscoma = a.IndexOf(",");
                condicion = int.Parse(a.Substring(0, poscoma));
                a = a.Substring(poscoma + 1);
                condicion = 1;
                while (condicion >= 0)
                {
                    Button btnCliente = new Button();
                    
                    int i = 0;
                    while (i < 8)
                    {
                        poscoma = a.IndexOf(",");
                        resultado[i] = a.Substring(0, poscoma);
                        a = a.Substring(poscoma + 1);
                        i++;
                    }

                    btnCliente.Text = resultado[6] + " " + resultado[4] + " " + resultado[5];
                    //btnCliente1.Text = resultado[6] + " " + resultado[4] + " " + resultado[5];
                    
                    
                    //btnCliente.TranslationY.LoadFromXaml(condicion.ToString());
                    sl.Children.Add(btnCliente);
                    
                    condicion--;

                }
            }catch(Exception ex) { }
        }
    }
}