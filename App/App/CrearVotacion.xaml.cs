using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CrearVotacion : ContentPage
	{
		public CrearVotacion ()
		{
			InitializeComponent ();
            targetPicker.Items.Add("votacion Simple");
            targetPicker.Items.Add("votacion Compleja");

        }

        
    }
}