using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace App
{
    public class Conectar
    {
        public static string Union(int opcion,string [] envio) { 
                string acceso = null;
                // Data buffer for incoming data.  
                byte[] bytes = new byte[1024];
                byte[] msg = new byte[1024];
            // Connect to a remote device.  
            try
                {
                    // Establish the remote endpoint for the socket.  
                    // This example uses port 11000 on the local computer.  
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress ipAddress = IPAddress.Parse("192.168.1.81");
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
                                envio[6] );
                            //nombrevotacion,opcion1,opcion2,opcion3,fechaini,fechafin,"yo le pasa una facultad por pasar algo"
                            
                             bytesSent = sender.Send(msg);//enviar
                          
                         
                            /*array_size = sender.Receive(bytes, 0, bytes.Length, 0);
                            Array.Resize(ref bytes, array_size);
                             data = Encoding.Default.GetString(bytes)*/ ; break;
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

