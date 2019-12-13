using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Novell.Directory.Ldap;
using System.Net;
using System.Net.Sockets;



namespace App
{


    public partial class MainPage : ContentPage
    {
        public ICommand TapCommand => new Command<string>(OpenBrowser);

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }



        void OpenBrowser(string url)
        {
            Device.OpenUri(new Uri(url));
        }

        private async void Btnpage1_Clicked(object sender, EventArgs e)
        {
            try
            {
                string usuario = PLCusuario.Text;
                string contraseña = btncontrasena.Text;
                string acceso = Conectar.Union(usuario, contraseña);
               
                
                int c = int.Parse(acceso.Substring(0));
                if (c == 1)
                {
                    
                        await Navigation.PushAsync(new MenuPage());
                }
                else
                {
                    await DisplayAlert("Alerta", "el usuario o la contraseña no son correctos", "ok");
                }
            }catch(Exception e2)
            {

                await DisplayAlert("Alerta", "el usuario o la contraseña no son correctos", "ok");
            }
        }
        /* private void Btnpage1_Clicked(object sender, EventArgs e)
         {
             lblRes.Text = "Hola" + PLCusuario.Text;

         }*/
    }
    public class Conectar
    {
        public static  string  Union(string usuario,string contraseña)
        {
            string acceso= null;
                // Data buffer for incoming data.  
                byte[] bytes = new byte[1024];
                
                // Connect to a remote device.  
                try
                {
                    // Establish the remote endpoint for the socket.  
                    // This example uses port 11000 on the local computer.  
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress ipAddress = IPAddress.Parse("10.183.114.57");
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

                    // Create a TCP/IP  socket.  
                    Socket sender = new Socket(ipAddress.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);

                    // Connect the socket to the remote endpoint. Catch any errors.  
                    try
                    {
                        sender.Connect(remoteEP);

                        Console.WriteLine("Socket connected to {0}",
                            sender.RemoteEndPoint.ToString());

                        // Encode the data string into a byte array.  
                        byte[] msg = Encoding.ASCII.GetBytes(usuario+","+contraseña);

                        // Send the data through the socket.  
                        int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.  
                    int array_size = 0;

                    array_size = sender.Receive(bytes, 0, bytes.Length, 0);
                    Array.Resize(ref bytes, array_size);
                    string data = Encoding.Default.GetString(bytes);

                    int poscoma = data.IndexOf(",");
                     acceso = data.Substring(0, poscoma);

                    // Release the socket.  
                    sender.Shutdown(SocketShutdown.Both);
                        sender.Close();

                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("SocketException : {0}", se.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            return acceso;
            }

            
        }

    }




