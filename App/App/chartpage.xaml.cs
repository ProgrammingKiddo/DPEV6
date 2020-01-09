using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microcharts;
using Entry = Microcharts.Entry;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class chartpage : ContentPage
	{
        List<Entry> entryList;

        public chartpage(string[] resultado)
        {
            try
            {
                InitializeComponent();
                entryList = new List<Entry>();
                LoadEntries(resultado);

                barchart.Chart = new BarChart()
                {
                    Entries = entryList

                };

                Piechart.Chart = new RadialGaugeChart()
                {
                    Entries = entryList

                };

                donutchart.Chart = new DonutChart()
                {
                    Entries = entryList

                };

                linechart.Chart = new LineChart()
                {
                    Entries = entryList

                };
            }

            catch(Exception e)
            {

                DisplayAlert("a", e.Message, "ok");

            }
            
        }

        private void LoadEntries(string[] resultado)
        {
            try
            {
                int i=0;
                int [] balor = new int[3];
                string valores = Conectar.Union(8,resultado);
                while(i <= 3)
                {
                    balor[i] = int.Parse(valores.Substring(0, valores.IndexOf(",")));
                    valores = valores.Substring(valores.IndexOf(",") + 1);
                    i++;
                }
               
            
                Entry e1 = new Entry(balor[1])
                {
                    Label = resultado[1],
                    ValueLabel = balor[1].ToString(),
                    Color = SKColor.Parse("#51FF00")
                };

                Entry e2 = new Entry(balor[2])
                {
                    Label = resultado[2],
                    ValueLabel = balor[2].ToString(),
                    Color = SKColor.Parse("#FF9700")
                };

                Entry e3 = new Entry(balor[3])
                {
                    Label = resultado[3],
                    ValueLabel = balor[3].ToString(),
                    Color = SKColor.Parse("#00FBFF")
                };
                entryList.Add(e1);
                entryList.Add(e2);
                entryList.Add(e3);
            }
            catch(Exception ex)
            {
                DisplayAlert("a", ex.Message, "ok");
            }
           

        }

	}
}