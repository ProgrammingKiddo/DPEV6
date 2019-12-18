using Novell.Directory.Ldap;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace VotUcaWebApi
{
    public class Conectar
    {

        public static void Union()
        {


            // Incoming data from the client.  
            string data = null;
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse("10.182.114.75");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.  
                    string[] envio = new string[1000];
                    string[] acceso = new string[1000];
                    byte[] recibir_info = new byte[1000];
                    byte[] msg = new byte[1000];
                    int array_size = 0;
                    int poscoma = 0;

                    array_size = handler.Receive(recibir_info, 0, recibir_info.Length, 0);//recibir
                    Array.Resize(ref recibir_info, array_size);
                    data = Encoding.Default.GetString(recibir_info);

                    string opcion = data.Substring(0, 1);//aqui cojo la opcion ya sea LDAP o VOTACION

                    switch (opcion)
                    {
                        case "1"://LDAP
                            poscoma = data.IndexOf(",");
                            string usuario = Descrifado.Login(data.Substring(1, poscoma - 1));
                            string contraseña = Descrifado.Login(data.Substring(poscoma + 1));
                            acceso[0] = Credenciales(usuario, contraseña);//credenciales tiene la conexion con la LDAP
                            msg = Encoding.ASCII.GetBytes(acceso[0] + ",");
                            break;
                        case "2"://CREAR VOTACION
                            {
                                data = data.Substring(1);
                                int i = 0;
                                while (i < 7)
                                {
                                    poscoma = data.IndexOf(",");
                                    envio[i] = data.Substring(0, poscoma );
                                    Console.WriteLine(envio[i]);//imprimo los datos de la votacion
                                    data = data.Substring(poscoma+1);
                                    i++;
                                }



                               acceso= Insertar(1,envio[1], envio[2], envio[3], envio[4], envio[5],envio[0]);

                            }
                            break;
                        case "3"://VER VOTACION
                            {
                                byte[] resultado = new Byte[2048];

                                acceso = Insertar(2, null, null, null, null, null,null);
                                int i = 0;
                                int a = int.Parse(acceso[9]);
                                a = a / 10;
                                msg = Encoding.ASCII.GetBytes(acceso[9]+","+acceso[i] + "," + acceso[i + 1] + "," +
                                   acceso[i + 2] + "," + acceso[i + 3] + "," + acceso[i + 4] + "," + acceso[i + 5] + "," +
                                   acceso[i + 6] + "," +
                                   acceso[i + 7] + ",");
                                msg.CopyTo(resultado, 0);
                                i += 10;
                                while (a > 1)
                                {

                                    msg = Encoding.ASCII.GetBytes(acceso[i] + "," + acceso[i + 1] + "," +
                                   acceso[i + 2] + "," + acceso[i + 3] + "," + acceso[i + 4] + "," + acceso[i + 5] + "," +
                                   acceso[i + 6] + "," +
                                   acceso[i + 7] + ",");
                                    msg.CopyTo(resultado, msg.Length);
                                    i += 10;
                                    a--;
                                }
                                msg = resultado;
                                Console.WriteLine(Encoding.Default.GetString(msg));
                            }; break;
                    }
                    handler.Send(msg);//enviar
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("1" + e);
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static string Credenciales(string usuario, string contraseña)
        {
            string acceso = "";
            try
            {
                string ldapHost = "ldap.uca.es";
                int ldapPort = 389;
                string loginDN = "CN=" + usuario + ",dc=uca,dc=es";
                LdapConnection conn = new LdapConnection();
                conn.Connect(ldapHost, ldapPort);
                conn.Bind(loginDN, contraseña);
                if (conn.Bound)
                {
                    acceso = "1";
                }

            }
            catch (LdapException e)
            {
                Console.WriteLine(e);

            }

            return acceso;
        }

        private static string[] Insertar(int opcion,string part1, string part2, string part3, string part4, string part5,string part6)
        {
            string[] acceso = new string[1000];

            SqlConnection cn = new SqlConnection();
            cn = new SqlConnection("Data Source=DESKTOP-1CTQ3SE\\SQLEXPRESS;Initial Catalog=Pinf;Integrated Security=True");
            cn.Open();
            SqlCommand cmd;
            try
            {
                switch (opcion)
                {
                    case 1:
                        cmd = new SqlCommand("insert into Votacion (Part1,Part2,Part3,fechaini,fechafin,nombre) VALUES('" + part1 + "','" + part2 + "','" + part3 + "','" + part4 + "','" + part5 + "','" + part6+ "')", cn);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Registro realizado");
                        break;
                    case 2:
                        {
                            SqlCommand consulta = new SqlCommand("Select * From Votacion", cn);
                            SqlDataReader dr = consulta.ExecuteReader();
                            int j = 0;
                            while (dr.Read())
                            {
                                int i = 0;
                                while (i < 7)
                                {
                                    acceso[i + j] = Convert.ToString(dr[i]);
                                    //Console.WriteLine(acceso[i + j] + "El valor de i+j es " + (i + j));
                                    i++;
                                }
                                
                                acceso[9] = j.ToString();

                                j += 10;

                            }
                            break;
                        }
                }
            }

            catch (Exception ex)

            { Console.WriteLine(ex.Message + "2"); }

            finally

            { if (cn.State == ConnectionState.Open) cn.Close(); }

            return acceso;

        }
    }

}










