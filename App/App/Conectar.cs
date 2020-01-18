using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace App
{
    public class Conectar
    {
        public static  string Union(int opcion,string [] envio) {
            string acceso= null;

            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];
                byte[] msg = new byte[1024];
            // Connect to a remote device.  
            try
                {
                    // Establish the remote endpoint for the socket.  
                    // This example uses port 11000 on the local computer.  
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress ipAddress = IPAddress.Parse("10.9.17.190");
                     IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

                     // Create a TCP/IP  socket.  
                    Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                    int bytesSent =0;
                    int array_size = 0;
                     string data = null;
                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                    {
                    
                        sender.Connect(remoteEP);
                    switch (opcion)
                    {
                        case 1:// informacion  LDAP
                            
                            // Encode the data string into a byte array.  
                            msg = Encoding.ASCII.GetBytes("1" + envio[0] + "," + envio[1]);   //  usuario y contraseña                         
                            bytesSent = sender.Send(msg); //enviar                          
                            array_size = sender.Receive(bytes, 0, bytes.Length, 0);//recibir
                            Array.Resize(ref bytes, array_size);
                             data = Encoding.Default.GetString(bytes);
                            int poscoma = data.IndexOf(",");
                            acceso = data.Substring(0,poscoma);//valor para controlar si se conecta con la LDAP
                            break;

                        case 2: //informacion VOTACION   
                            msg = Encoding.ASCII.GetBytes("2" + envio[0] + "," + envio[1] + "," +    
                                envio[2] + "," + envio[3] + "," + envio[4] + "," + envio[5] + "," +
                                envio[6] + "," + envio[7] + ",");
                            //nombrevotacion,opcion1,opcion2,opcion3,fechaini,fechafin,carrera,IdUca
                            
                             bytesSent = sender.Send(msg);//enviar
                             break;
                        case 3://VER VOTACION 
                            msg = Encoding.ASCII.GetBytes("3" + envio[0]);
                            bytesSent = sender.Send(msg);

                            array_size = sender.Receive(bytes, 0, bytes.Length, 0);
                            Array.Resize(ref bytes, array_size);
                            acceso = Encoding.Default.GetString(bytes);
                            break;
                        case 4://votar  
                            msg = Encoding.ASCII.GetBytes("4" + envio[0] + "," + envio[1]+ "," + envio[2] + ",");
                            //id_votacion,"1 si es p1,2 si es p2 y 3 si es p3"

                            bytesSent = sender.Send(msg);break;
                        case 5://Ver votaciones futuras
                            msg = Encoding.ASCII.GetBytes("5" + envio[0]);
                            bytesSent = sender.Send(msg);

                            array_size = sender.Receive(bytes, 0, bytes.Length, 0);
                            Array.Resize(ref bytes, array_size);
                            acceso = Encoding.Default.GetString(bytes);
                            break;
                        case 6:
                            msg = Encoding.ASCII.GetBytes("6" + envio[0]);
                            bytesSent = sender.Send(msg);

                            array_size = sender.Receive(bytes, 0, bytes.Length, 0);
                            Array.Resize(ref bytes, array_size);
                            acceso = Encoding.Default.GetString(bytes);
                            break;

                         case 7: //Comprobar si el usuario ha votado
                             msg = Encoding.ASCII.GetBytes("7" + envio[0]+","+envio[9]+",");//0 es idvotacion,y 9 es idusuario
                                Console.WriteLine("IdVotacion: " + envio[0] + " IdUsuario: " + envio[9]);
                             bytesSent = sender.Send(msg);

                             array_size = sender.Receive(bytes, 0, bytes.Length, 0);
                             Array.Resize(ref bytes, array_size);
                             acceso = Encoding.Default.GetString(bytes);
                            
                             break;
                             
                        case 8://Ver resultados de la grafica 
                            msg = Encoding.ASCII.GetBytes("8" + envio[0] + ",");
                            bytesSent = sender.Send(msg);

                            array_size = sender.Receive(bytes, 0, bytes.Length, 0);
                            Array.Resize(ref bytes, array_size);
                            acceso = Encoding.Default.GetString(bytes);
                            break;

                        case 9: //Envio de datos del usuario si es la primera vez que entra
                            msg = Encoding.ASCII.GetBytes("9" + (string)App.Current.Properties["name"] + "," + envio[0] + "," + envio[1] + ",");
                            //curso y grado

                            bytesSent = sender.Send(msg);
                            acceso = (string)App.Current.Properties["name"];
                            break;
                        case 10: //Envío de datos para editar votación
                            msg = Encoding.ASCII.GetBytes("0" + envio[0] + "," + envio[1] + ",");

                            bytesSent = sender.Send(msg);//enviar
                            break;
                        case 11: //Envío de IdVotacion para eliminación
                            msg = Encoding.ASCII.GetBytes("a" + envio[0] + ",");

                            bytesSent = sender.Send(msg);
                            break;
                    }
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

