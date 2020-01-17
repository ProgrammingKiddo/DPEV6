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
                new Menu{ MenuTitle = "Manual de usuario", MenuDetail = ""},
                new Menu{ MenuTitle = "Vídeo explicativo", MenuDetail = ""},
                new Menu{ MenuTitle = "Email de soporte", MenuDetail = ""}
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
                    string nombre = (string)App.Current.Properties["name"];
                    if (int.Parse(nombre.Substring(8)) == 0) 
                        Detail = new NavigationPage(new MenuPage(0));
                    else
                        Detail = new NavigationPage(new MenuPage(1));
                }
                else if (menu.MenuTitle.Equals("Manual de usuario"))
                {
                    #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                    Device.OpenUri(new Uri("https://docs.google.com/document/d/1kyRvKGb01fU7mXhrFEPSryjrrR9n8Ixy/edit#heading=h.gjdgxs"));
                    #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                }
                else if (menu.MenuTitle.Equals("Vídeo explicativo"))
                {
                    #pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                    Device.OpenUri(new Uri("https://www.youtube.com/watch?v=BO764hYiEek"));
                    #pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                }else if (menu.MenuTitle.Equals("Email de soporte"))
                {
                    await DisplayAlert("Correo electrónico de soporte:", "votuca.spprt@gmail.com", "Aceptar");
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
