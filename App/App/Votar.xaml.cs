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
        { string[] resultado = new string[10];
            int condicion=0;
            string a = Conectar.Union(3, null);
             int poscoma = a.IndexOf(",");
            condicion = int.Parse(a.Substring(0, poscoma));
            a = a.Substring(poscoma + 1);

            while (condicion > 0)
            {  Button btnCliente = new Button();
                int i = 0;
                while (i < 8)
                {
                    poscoma = a.IndexOf(",");
                    resultado[i] = a.Substring(0, poscoma);
                    a = a.Substring(poscoma + 1);
                }
                    
                    btnCliente.Text = resultado[7]+" "+resultado[4]+" "+resultado[5];
                btnCliente.TranslationX.LoadFromXaml(condicion.ToString());
                btnCliente.TranslationY.LoadFromXaml(condicion.ToString());
                sl.Children.Add(btnCliente);
                    condicion--;
                
            }
        }
    }
}