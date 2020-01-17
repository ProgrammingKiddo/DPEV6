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
    public partial class MenuPage : MasterDetailPage
    {
        public MenuPage(int i)
        {
            InitializeComponent();
           Init(i);

            
           
        }

        void Init(int i)
        {
            List<Menu> menu = new List<Menu>
            {
                new Menu{ MenuTitle = "Inicio", MenuDetail = ""},               
                new Menu{ MenuTitle = "Historial", MenuDetail = ""},
                new Menu{ MenuTitle = "Configuración", MenuDetail = ""},
                new Menu{ MenuTitle = "Cerrar Sesión", MenuDetail = ""}
            };

            ListMenu.ItemsSource = menu;
            if (i == 1)
            {
                Detail = new NavigationPage(new Page1())
                {
                    BarBackgroundColor = Color.FromHex("FFA73F")
                };
            }
            else
            {
                Detail = new NavigationPage(new interfaz_usuario())
                {
                    BarBackgroundColor = Color.FromHex("FFA73F")
                };
            }
           
        }
        private async void btncerrarsesion_Clicked(object sender, EventArgs e)//boton acceder
        {

            var answer = await DisplayAlert("Alerta", "¿Estás seguro de que quieres cerrar sesión?", "Si", "No");
            if (answer == true)
            {
                Detail = new NavigationPage(new MainPage());
            }
        }

        async void ListMenu_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var menu = e.SelectedItem as Menu;
            if (menu != null)
            {
                if (menu.MenuTitle.Equals("Inicio"))
                {
                    Detail = new NavigationPage(new MenuPage(1));
                }
                else if (menu.MenuTitle.Equals("Crear Votaciones"))
                {
                    Detail = new NavigationPage(new CrearVotacion())
                    {
                        BarBackgroundColor = Color.DarkBlue
                    }; 
                }
                else if (menu.MenuTitle.Equals("Cerrar Sesión"))
                {
                    var answer = await DisplayAlert("Alerta", "¿Estás seguro de que quieres cerrar sesión?", "Si", "No");
                    if (answer == true)
                    {
                        Detail = new NavigationPage(new MainPage());
                    }  
                }

            }
        }
    }
    public class Menu
    {
        public string MenuTitle
        {
            get;
            set;
        }
        public object MenuDetail
        {
            get;
            set;
        }
    }
}