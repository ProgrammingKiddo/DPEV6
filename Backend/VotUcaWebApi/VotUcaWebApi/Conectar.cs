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
            IPAddress ipAddress = IPAddress.Parse("192.168.1.81");
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
                    string[] envio = new string[10000];
                    string[] acceso = new string[10000];
                    byte[] recibir_info = new byte[10000];
                    byte[] msg = new byte[10000];
                    int array_size = 0;
                    int poscoma = 0;

                    array_size = handler.Receive(recibir_info, 0, recibir_info.Length, 0);//recibir
                    Array.Resize(ref recibir_info, array_size);
                    data = Encoding.Default.GetString(recibir_info);

                    string opcion = data.Substring(0, 1);//aqui cojo la opcion ya sea LDAP,CREAR VOTACION,Ver Votacion,Votar

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
                                DateTime localDate = DateTime.Now;

                                        byte[] resultado = new Byte[1000];
                                        string suma=null;

                                        acceso = Insertar(2, null, null, null, null, null, null);
                                        int i = 0;
                                        int a = int.Parse(acceso[9]);
                                        a = a / 10;
                                        
                                        int cont = 0;                                       
                                        while (a >= 0)
                                        {
                                            Console.WriteLine(Convert.ToDateTime(acceso[i + 4])+"<="+ localDate + "," + Convert.ToDateTime(acceso[i + 5]) + ">=" + localDate);
                                            if (DateTime.Compare(Convert.ToDateTime(acceso[i+4]), localDate) <= 0 && DateTime.Compare(Convert.ToDateTime(acceso[i+5]), localDate) >=0)
                                            {
                                             cont++;
                                            suma +=acceso[i] + "," + acceso[i + 1] + "," +
                                             acceso[i + 2] + "," + acceso[i + 3] + "," + acceso[i + 4] + "," + acceso[i + 5] + "," +
                                             acceso[i + 6] + ",";
                                            
                                            }
                                           i += 10;
                                            
                                            a--;
                                        }
                                msg = Encoding.ASCII.GetBytes(cont.ToString() + "," + suma);
                                Console.WriteLine(Encoding.Default.GetString(msg));
                                        
                        
                            }; break;
                        case "4":
                            { 
                                    data = data.Substring(1);
                                    int i = 0;
                                    while (i < 2)
                                    {
                                        poscoma = data.IndexOf(",");
                                        envio[i] = data.Substring(0, poscoma);
                                        data = data.Substring(poscoma + 1);
                                        i++;
                                    }
                                    Console.WriteLine(envio[0] + "," + envio[1]);
                                    acceso = Insertar(3, envio[0], envio[1], null, null, null, null);
                                    //envio[0]=id_votaciones,envio[1]=entero del participante
                                
                            }
                            ; break;
                        case "5"://votaciones futuras
                            {
                                DateTime localDate = DateTime.Now;

                                byte[] resultado = new Byte[1000];
                                string suma=null;
                                
                                acceso = Insertar(2, null, null, null, null, null, null);
                                int cont = 0;
                                int i = 0;
                                int a = int.Parse(acceso[9]);
                                a = a / 10;
                                
                                while (a >= 0)
                                {
                                    Console.WriteLine(Convert.ToDateTime(acceso[i + 4]) + ">" + localDate);
                                    if (DateTime.Compare(Convert.ToDateTime(acceso[i + 4]), localDate) >0)
                                    {
                                        cont++;
                                        suma +=acceso[i] + "," + acceso[i + 1] + "," +
                                         acceso[i + 2] + "," + acceso[i + 3] + "," + acceso[i + 4] + "," + acceso[i + 5] + "," +
                                         acceso[i + 6] + ",";
                                       
                                        Console.WriteLine(cont);
                                    }
                                    i += 10;
                                    
                                    a--;
                                }
                                msg = Encoding.ASCII.GetBytes(cont.ToString() + "," + suma);
                                Console.WriteLine(Encoding.Default.GetString(msg));
                            }; break;
                        case "6"://votaciones acabadas
                            {
                                DateTime localDate = DateTime.Now;

                                byte[] resultado = new Byte[1000];
                                string suma = null;
                                acceso = Insertar(2, null, null, null, null, null, null);
                                int i = 0;
                                int a = int.Parse(acceso[9]);
                                a = a / 10;                                
                                int cont = 0;
                                while (a >= 0)
                                {
                                    Console.WriteLine(Convert.ToDateTime(acceso[i + 5]) + "<" + localDate);
                                    if (DateTime.Compare(Convert.ToDateTime(acceso[i + 5]), localDate) < 0)
                                    {
                                        cont++;
                                        suma +=acceso[i] + "," + acceso[i + 1] + "," +
                                         acceso[i + 2] + "," + acceso[i + 3] + "," + acceso[i + 4] + "," + acceso[i + 5] + "," +
                                         acceso[i + 6] + ",";
                                        
                                    }
                                    i += 10;                                  
                                    a--;
                                }
                                msg = Encoding.ASCII.GetBytes(cont.ToString()+","+suma);
                                Console.WriteLine(Encoding.Default.GetString(msg));
                            }; break;

                       /* case "7": //Comprobar usuario registrado.
                            {
                                envio[0] = data.Substring(1);
                                Console.WriteLine("Comprobando usuario...");
                                acceso = Insertar(7, envio[0], null, null, null, null, null);
                                msg = Encoding.UTF8.GetBytes(acceso[0]);
                            }
                            break*/

                        case "8"://ver resultados
                            {
                                byte[] resultado = new Byte[1000];
                                string suma;
                                data = data.Substring(1);
                                envio[0] = data.Substring(0, data.IndexOf(","));
                                acceso = Insertar(8, envio[0], null, null, null, null, null);
                                
                                suma =  acceso[1] + "," + acceso[2]+ "," + acceso[3] + ",";
                            
                                msg = Encoding.ASCII.GetBytes(suma);
                                Console.WriteLine(Encoding.Default.GetString(msg));

                            };break;

                        case "9": //Guardar Usuario
                            {
                                data = data.Substring(1);
                                int i = 0;
                                while (i < 3)
                                {
                                    poscoma = data.IndexOf(",");
                                    envio[i] = data.Substring(0, poscoma);
                                    data = data.Substring(poscoma + 1);
                                    i++;
                                }
                                
                                acceso = Insertar(9, envio[0], envio[1], envio[2], null, null, null);
                                Console.WriteLine("Registrando usuario...");
                            }
                            break;

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
            string[] prueba = new string[20];
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
                    
                       prueba = Insertar(7, usuario, null, null, null, null, null);
                    acceso = prueba[0];
                    if (acceso == "1") {acceso = "2"; }
                    if (acceso == "0") { acceso = "1"; }

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
            cn = new SqlConnection("Data Source=DESKTOP-QDS38O2;Initial Catalog=pinf;Integrated Security=True");
            cn.Open();
            SqlCommand cmd = null;
            try
            {
                switch (opcion)
                {
                    case 1:
                        cmd = new SqlCommand("insert into Votacion (Part1,Part2,Part3,fechaini,fechafin,nombre) VALUES('" + part1 + "','" + part2 + "','" + part3 + "','" + part4 + "','" + part5 + "','" + part6 + "')", cn);

                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("insert into Resultados (Result1,Result2,Result3) VALUES('" + 0 + "','" + 0 + "','" + 0 + "')", cn);

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
                                    //Console.WriteLine(acceso[i + j]);
                                    i++;
                                }
                                acceso[9] = j.ToString();
                                j += 10;
                            }

                        }
                        break;
                    case 3:
                        {
                            SqlCommand consulta = new SqlCommand("Select * From Resultados Where IdVotaciones='" + part1 + "' ", cn);
                            SqlDataReader dr = consulta.ExecuteReader();
                            int j = 0;
                            while (dr.Read())
                            {
                                int i = 0;
                                while (i < 4)
                                {
                                    acceso[i + j] = Convert.ToString(dr[i]);
                                    //Console.WriteLine(acceso[i + j] + "El valor de i+j es " + (i + j));
                                    i++;
                                }
                                j += 10;
                            }
                            cn.Close();
                            cn.Open();

                            if (int.Parse(part2) == 1)
                            {
                                int res;
                                res = int.Parse(acceso[1]) + 1;

                                cmd = new SqlCommand("update Resultados set Result1='" + res + "' where IdVotaciones='" + part1 + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                            if (int.Parse(part2) == 2)
                            {
                                int res;
                                res = int.Parse(acceso[2]) + 1;

                                cmd = new SqlCommand("update Resultados set Result2='" + res + "' where IdVotaciones='" + part1 + "'", cn);
                                cmd.ExecuteNonQuery();
                            }
                            if (int.Parse(part2) == 3)
                            {
                                int res;
                                res = int.Parse(acceso[3]) + 1;

                                cmd = new SqlCommand("update Resultados set Result3='" + res + "' where IdVotaciones='" + part1 + "'", cn);
                                cmd.ExecuteNonQuery();
                            }



                            Console.WriteLine("Registro realizado"); break;
                        }

                        case 7: //Comprobar Usuario
                        {
                            SqlCommand consulta = new SqlCommand("Select * From Usuarios where IdUca='" + part1 + "'", cn);
                            SqlDataReader dr = consulta.ExecuteReader();

                            if (dr.HasRows)
                                acceso[0] = "1";
                            else
                                acceso[0] = "0";

                            Console.WriteLine("Usuario comprobado.");
                        }
                        break;

                        case 8:
                        {
                            SqlCommand consulta = new SqlCommand("Select * From Resultados Where IdVotaciones='" + part1 + "' ", cn);
                            SqlDataReader dr = consulta.ExecuteReader();
                            int j = 0;
                            while (dr.Read())
                            {
                                int i = 0;
                                while (i < 5)
                                {
                                    acceso[i + j] = Convert.ToString(dr[i]);
                                    //Console.WriteLine(acceso[i + j] + "El valor de i+j es " + (i + j));
                                    i++;
                                }
                                j += 10;
                            }


                        }; break;

                        case 9:
                        {
                            cmd = new SqlCommand("insert into Usuarios (IdUca, Carrera, Curso) VALUES('" + part1 + "','" + part2 + "','" + part3 + "')", cn);

                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Usuario registrado.");

                        }
                        break;
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










