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
            IPAddress ipAddress = IPAddress.Parse("10.9.17.190");
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
                    string[] carrera = new string[100];
                    byte[] recibir_info = new byte[10000];
                    byte[] msg = new byte[10000];
                    int array_size = 0;
                    int poscoma = 0;

                    array_size = handler.Receive(recibir_info, 0, recibir_info.Length, 0);//recibir
                    Array.Resize(ref recibir_info, array_size);
                    data = Encoding.Default.GetString(recibir_info);

                    string opcion = data.Substring(0, 1);//aqui cojo la opcion ya sea LDAP,CREAR VOTACION,Ver Votacion,Votar
                    SqlConnection cn = new SqlConnection();
                    cn = new SqlConnection("Data Source=localhost;Initial Catalog=VotUcaWebApi;Integrated Security=True");
                    cn.Open();

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
                                while (i < 8)
                                {
                                    poscoma = data.IndexOf(",");
                                    envio[i] = data.Substring(0, poscoma );
                                    Console.WriteLine(envio[i]);//imprimo los datos de la votacion
                                    data = data.Substring(poscoma+1);
                                    i++;
                                }
                               acceso= Insertar(1,envio[1], envio[2], envio[3], envio[4], envio[5],envio[0],envio[6],envio[7]);
                            }
                            break;
                        case "3"://VER VOTACION
                            {
                                DateTime localDate = DateTime.Today;

                                byte[] resultado = new Byte[1000];
                                data = data.Substring(1);
                                SqlCommand consulta = new SqlCommand("Select Carrera From Usuarios where IdUca='" + data + "'", cn);
                                SqlDataReader dr = consulta.ExecuteReader();
                                int z = 0;
                                while (dr.Read())
                                {
                                    carrera[z] = Convert.ToString(dr[z]);

                                }
                                cn.Close();
                                cn.Open();

                                string suma =null;

                                acceso = Insertar(2, null, null, null, null, null, null, null, null);
                                int i = 0;
                                int a = int.Parse(acceso[9]);
                                a = a / 10;
                                        
                                int cont = 0;                                       
                                while (a >= 0)
                                    {
                                    if ((acceso[i + 7] == carrera[0]) ||(acceso[i + 7] == "TODOS LOS GRADOS"))
                                    {
                                        if (DateTime.Compare(Convert.ToDateTime(acceso[i + 4]), localDate) <= 0 && DateTime.Compare(Convert.ToDateTime(acceso[i + 5]), localDate) >= 0)
                                        {
                                            cont++;
                                            suma += acceso[i] + "," + acceso[i + 1] + "," +
                                             acceso[i + 2] + "," + acceso[i + 3] + "," + acceso[i + 4] + "," + acceso[i + 5] + "," +
                                             acceso[i + 6] + "," + acceso[i + 7] + "," + acceso[i + 8] + ",";

                                        }
                                    }
                                        i += 10;
                                            
                                        a--;
                                    }
                                msg = Encoding.ASCII.GetBytes(cont.ToString() + "," + suma);
                                Console.WriteLine(Encoding.Default.GetString(msg));
                                        
                        
                            }; break;
                        case "4":   //VOTAR
                            { 
                                    data = data.Substring(1);
                                    int i = 0;
                                    while (i < 4)
                                    {
                                        poscoma = data.IndexOf(",");
                                        envio[i] = data.Substring(0, poscoma);
                                        data = data.Substring(poscoma + 1);
                                        i++;
                                    }
                                    Console.WriteLine("VOTAR - Los datos han llegado al backend: "+ envio[0] + "," + envio[1] + "," + envio[2] + "," + envio[3]);
                                
                                acceso = Insertar(3, envio[0], envio[1], envio[2], envio[3], null, null, null, null);
                                //envio[0]=id_votaciones,envio[1]=entero del participante,envio[2] es el IdUsuario, envio[3] es el Rol del usuario

                            }
                            ; break;
                        case "5"://votaciones futuras
                            {
                                DateTime localDate = DateTime.Today;

                                byte[] resultado = new Byte[1000];
                                data = data.Substring(1);
                               
                                SqlCommand consulta = new SqlCommand("Select Carrera From Usuarios where IdUca='" + data + "'", cn);
                                SqlDataReader dr = consulta.ExecuteReader();
                                int z = 0;
                                while (dr.Read())
                                {
                                    carrera[z] = Convert.ToString(dr[z]);

                                }
                                cn.Close();
                                cn.Open();

                                string suma =null;
                                
                                acceso = Insertar(2, null, null, null, null, null, null, null, null);
                                int cont = 0;
                                int i = 0;
                                int a = int.Parse(acceso[9]);
                                a = a / 10;
                                
                                while (a >= 0)
                                {
                                    
                                    if (acceso[i + 7] == carrera[0] || (acceso[i + 7] == "TODOS LOS GRADOS"))
                                    {
                                        if (DateTime.Compare(Convert.ToDateTime(acceso[i + 4]), localDate) > 0)
                                        {
                                            cont++;
                                            suma += acceso[i] + "," + acceso[i + 1] + "," +
                                             acceso[i + 2] + "," + acceso[i + 3] + "," + acceso[i + 4] + "," + acceso[i + 5] + "," +
                                             acceso[i + 6] + "," + acceso[i + 7] + "," + acceso[i + 8] + ",";                                           
                                        }
                                    }
                                    i += 10;
                                    
                                    a--;
                                }
                                msg = Encoding.ASCII.GetBytes(cont.ToString() + "," + suma);
                                Console.WriteLine(Encoding.Default.GetString(msg));
                            }; break;
                        case "6"://votaciones acabadas
                            {
                                DateTime localDate = DateTime.Today;

                                byte[] resultado = new Byte[1000];
                                data = data.Substring(1);
                                SqlCommand consulta = new SqlCommand("Select Carrera From Usuarios where IdUca='" + data + "'", cn);
                                SqlDataReader dr = consulta.ExecuteReader();
                                int z = 0;
                                while (dr.Read())
                                {
                                    carrera[z] = Convert.ToString(dr[z]);

                                }
                                cn.Close();
                                cn.Open();

                                string suma = null;
                                acceso = Insertar(2, null, null, null, null, null, null, null, null);
                                int i = 0;
                                int a = int.Parse(acceso[9]);
                                a = a / 10;                                
                                int cont = 0;
                                while (a >= 0)
                                {
                                    if (acceso[i + 7] == carrera[0] || (acceso[i + 7] == "TODOS LOS GRADOS"))
                                    {
                                        if (DateTime.Compare(Convert.ToDateTime(acceso[i + 5]), localDate) < 0)
                                        {
                                            cont++;
                                            suma += acceso[i] + "," + acceso[i + 1] + "," +
                                             acceso[i + 2] + "," + acceso[i + 3] + "," + acceso[i + 4] + "," + acceso[i + 5] + "," +
                                             acceso[i + 6] + "," + acceso[i + 7] + "," + acceso[i + 8] + ",";

                                        }
                                    }
                                    i += 10;                                  
                                    a--;
                                }
                                msg = Encoding.ASCII.GetBytes(cont.ToString()+","+suma);
                                Console.WriteLine(Encoding.Default.GetString(msg));
                            }; break;

                         case "7": //Comprobar si el usuario ya ha votado.
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
                                Console.WriteLine("Comprobacion: " + envio[0] + "," + envio[1] );
                                Console.WriteLine("Comprobando si el usuario ha votado...");
                                 acceso = Insertar(4, envio[0],envio[1], null, null, null, null, null, null);//0 es idvotacion,y 1 es iduca
                                 msg = Encoding.ASCII.GetBytes(acceso[0]+",");
                                //Console.WriteLine(Encoding.Default.GetString(msg));
                            }
                             break;

                        case "8"://ver resultados
                            {
                                byte[] resultado = new Byte[1000];
                                string suma;
                                data = data.Substring(1);
                                envio[0] = data.Substring(0, data.IndexOf(","));
                                acceso = Insertar(8, envio[0], null, null, null, null, null, null, null);
                                
                                suma =  acceso[1] + "." + acceso[2]+ "." + acceso[3] + ".";
                            
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
                                
                                acceso = Insertar(9, envio[0], envio[1], envio[2], null, null, null, null, null);
                                Console.WriteLine("Registrando email, curso...");
                            }
                            break;

                        case "0": //Editar una votación
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

                                acceso = Insertar(11, envio[0], envio[1], null, null, null, null, null, null);
                            }
                            break;
                        case "a": //Eliminar una votación
                            {
                                data = data.Substring(1);
                                int i = 0;
                                while (i < 1)
                                {
                                    poscoma = data.IndexOf(",");
                                    envio[i] = data.Substring(0, poscoma);
                                    data = data.Substring(poscoma + 1);
                                    i++;
                                }

                                acceso = Insertar(12, envio[0], null, null, null, null, null, null, null);
                            }
                            break;
                        case "b": //Obtener rol usuario actual
                            {
                                string suma;
                                data = data.Substring(1);
                                int i = 0;
                                while (i < 1)
                                {
                                    poscoma = data.IndexOf(",");
                                    envio[i] = data.Substring(0, poscoma);
                                    data = data.Substring(poscoma + 1);
                                    i++;
                                }
                                //Console.WriteLine("Hasta aquí llego: " + envio[0]);
                                acceso = Insertar(13, envio[0], null, null, null, null, null, null, null);
                                suma = acceso[0] + ",";
                                //Console.WriteLine(suma);
                                msg = Encoding.ASCII.GetBytes(suma);
                                Console.WriteLine(Encoding.Default.GetString(msg));
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
                Console.WriteLine("Alerta (error en el backend): " + e.Message);
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
                    string searchBase = "CN=" + usuario + ",dc=uca,dc=es";
                    string searchFilter = "tipodocumento=NIF";

                    LdapSearchResults lsc = conn.Search(searchBase, LdapConnection.SCOPE_BASE, searchFilter, null, false);
                    LdapEntry nextEntry = lsc.next();
                    string email = nextEntry.getAttribute("mail").StringValue;
                    string rol = nextEntry.getAttribute("ou").StringValue;
                    /*
                    ------------------------------------------------------------------------------------------------------
                   DirectoryEntry de = new DirectoryEntry();
                   de.Path = "ldap://ldap.uca.es:389/cn=" + usuario + ",dc=uca,dc=es:389";
                   de.Username = usuario;
                   de.Password = contraseña;
                   
                   DirectorySearcher ds = new DirectorySearcher(de);
                   ds.Filter = "(&((&(objectCategory=Person)(objectClass=User)))(samaccountname=" + usuario + "))";
                   ds.SearchScope = SearchScope.Subtree;

                   SearchResult rs = ds.FindOne();

                   Console.WriteLine(rs.GetDirectoryEntry().Properties["mail"].Value.ToString());*/

                    
                    prueba = Insertar(7, usuario, null, null, null, null, null, null, null);
                    acceso = prueba[0];
                    if (acceso == "0") Insertar(10, usuario, email, rol, null, null, null, null, null);
                    if (acceso == "1") { acceso = "2"; }
                    if (acceso == "0") { acceso = "1"; }
                    

                }

            }
            catch (LdapException e)
            {
                Console.WriteLine(e);

            }

            return acceso;
        }

        private static string[] Insertar(int opcion,string part1, string part2, string part3, string part4, string part5,string part6,string part7,string part8)
        {
            string[] acceso = new string[1000];

            SqlConnection cn = new SqlConnection();
            cn = new SqlConnection("Data Source=localhost;Initial Catalog=VotUcaWebApi;Integrated Security=True");
            cn.Open();
            SqlCommand cmd = null;
            try
            {
                switch (opcion)
                {
                    case 1:
                        cmd = new SqlCommand("insert into Votacion (Part1,Part2,Part3,fechaini,fechafin,nombre,Carrera,IdUca) VALUES('" + part1 + "','" + part2 + "','" + part3 + "','" + part4 + "','" + part5 + "','" + part6+ "','" + part7 + "','" + part8 + "')", cn);

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
                                while (i < 9)
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
                    case 3: //VOTAR
                        {
                            cn.Close();
                            cn.Open();
                            SqlCommand consulta = cn.CreateCommand();
                            SqlCommand consulta2 = cn.CreateCommand();
                            SqlCommand consulta3 = cn.CreateCommand();
                            consulta.CommandText = "Select * From Resultados Where IdVotaciones = '" + part1 + "'";
                            consulta2.CommandText = "Select * From Votacion Where IdVotacion='" + part1 + "'";
                            consulta3.CommandText = "Select IdUsuarios From Usuarios Where IdUca='" + part3 + "' ";
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

                            string tipoVotacion ="";

                            dr = consulta2.ExecuteReader();

                            if (dr.Read())
                            {
                                tipoVotacion = Convert.ToString(dr[7]);
                            }

                            Console.WriteLine("La votacion es: " + tipoVotacion);

                            cn.Close();
                            cn.Open();

                            if(tipoVotacion.Equals("TODOS LOS GRADOS")) //VOTACIÓN GLOBAL
                            {
                                float ponderacion = 0.0f;

                                if (part4.Equals("Alumnos"))
                                {
                                    ponderacion = 0.28f;
                                }
                                else if (part4.Equals("PDI"))
                                {
                                    ponderacion = 0.64f;
                                }
                                else
                                {
                                    ponderacion = 0.08f;
                                }

                                if (int.Parse(part2) == 1)
                                {
                                    float res;
                                    res = float.Parse(acceso[1]) + ponderacion;

                                    cmd = new SqlCommand("update Resultados set Result1='" + res + "' where IdVotaciones='" + part1 + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                                if (int.Parse(part2) == 2)
                                {
                                    float res;
                                    res = float.Parse(acceso[2]) + ponderacion;

                                    cmd = new SqlCommand("update Resultados set Result2='" + res + "' where IdVotaciones='" + part1 + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                                if (int.Parse(part2) == 3)
                                {
                                    float res;
                                    res = float.Parse(acceso[3]) + ponderacion;

                                    cmd = new SqlCommand("update Resultados set Result3='" + res + "' where IdVotaciones='" + part1 + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else //VOTACIÓN DE UN GRADO EN CONCRETO
                            {
                                if (int.Parse(part2) == 1)
                                {
                                    float res;
                                    res = float.Parse(acceso[1]) + 1;

                                    cmd = new SqlCommand("update Resultados set Result1='" + res + "' where IdVotaciones='" + part1 + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                                if (int.Parse(part2) == 2)
                                {
                                    float res;
                                    res = float.Parse(acceso[2]) + 1;

                                    cmd = new SqlCommand("update Resultados set Result2='" + res + "' where IdVotaciones='" + part1 + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                                if (int.Parse(part2) == 3)
                                {
                                    float res;
                                    res = float.Parse(acceso[3]) + 1;

                                    cmd = new SqlCommand("update Resultados set Result3='" + res + "' where IdVotaciones='" + part1 + "'", cn);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            cn.Close();
                            cn.Open();

                            dr = consulta3.ExecuteReader();
                            
                            while (dr.Read())
                            {
                                int i = 0;
                                while (i < 1)
                                {
                                    acceso[i] = Convert.ToString(dr[i]);
                                    //Console.WriteLine(acceso[i]);
                                    i++;
                                }
                               
                            }

                            //Console.WriteLine("Hago consulta3 tambien");

                            cn.Close();
                            cn.Open();

                            cmd = new SqlCommand("insert into Registro (IdUsuarios,IdVotacion) VALUES('" + acceso[0] + "','" + part1 +  "')", cn);

                            cmd.ExecuteNonQuery();

                            Console.WriteLine("Registro realizado"); break;
                        }
                    case 4://comprobar si el usuario ha votado
                        {
                            string[] sacar = new string[20];
                            //Console.WriteLine("IdVotacion: " + part1 + " IdUca: " + part2);
                            SqlCommand consulta = new SqlCommand("Select IdUsuarios From Usuarios where IdUca='" + part2 + "'", cn);
                            SqlDataReader dr = consulta.ExecuteReader();
                            while (dr.Read())
                            {                               
                                    sacar[0] = Convert.ToString(dr[0]);
                                   // Console.WriteLine(sacar[0]);
                            }

                            Console.WriteLine("IdUsuario obtenido: " + sacar[0]);

                            cn.Close();
                            cn.Open();
                             consulta = new SqlCommand("Select * From Registro where IdUsuarios='"+sacar[0]+"'and IdVotacion ='" + part1 + "'", cn);
                             dr = consulta.ExecuteReader();
                            //Console.WriteLine(dr.HasRows);
                            if (dr.HasRows)
                            {
                                acceso[0] = "1";
                            }
                            else { acceso[0]="2";
                                Console.WriteLine("El usuario no ha votado");
                            }
                            //Console.WriteLine(acceso[0]);
                        };break;

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
                            SqlCommand consulta = new SqlCommand("Select * From Resultados Where IdVotaciones='" + part1 + "'", cn);
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

                            Console.WriteLine(acceso[1]);


                        }; break;

                        case 9: //Guardar usuario con los datos del Formulario.
                        {

                            cmd = new SqlCommand("update Usuarios set Carrera='" + part2 + "', Curso='" + part3 + "' where IdUca='" + part1 + "'", cn);
                            //cmd = new SqlCommand("insert into Usuarios (IdUca, Carrera, Curso, Email, Rol) VALUES('" + part1 + "','" + part2 + "','" + part3 + "','" + null + "','" + null + "')", cn);

                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Usuario actualizado con curso y carrera.");

                        }
                        break;

                        case 10: //Guardar usuario con los datos del LDAP
                        {
                            cmd = new SqlCommand("insert into Usuarios (IdUca, Carrera, Curso, Email, Rol) VALUES('" + part1 + "','" + null + "','" + null + "','" + part2 + "','" + part3 + "')", cn);

                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Usuario registrado.");
                        }
                        break;

                        case 11: //Actualizar fecha de una votación
                        {
                            cmd = new SqlCommand("update Votacion set fechafin='" + part2 + "' where IdVotacion='" + part1 + "'", cn);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Fecha modificada.");
                            break;
                        }

                        case 12:
                        {    //Eliminar votación de la base de datos
                            cmd = new SqlCommand("delete from Votacion where IdVotacion='" + part1 + "'", cn);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Votación eliminada.");
                            break;
                        }

                    case 13:
                        {   //Obtener rol del usuario usando la app

                            /*Console.WriteLine("Entro en el caso 13, seguro.(" + part1 + ")");*/
                            SqlCommand consulta = new SqlCommand("Select * From Usuarios where IdUca='" + part1 + "'", cn);
                            SqlDataReader dr = consulta.ExecuteReader();

                            if (dr.Read())
                            {
                                acceso[0] = Convert.ToString(dr[5]);
                            }

                            //Console.WriteLine("Aquí doy el dato: " + acceso[0]);
                            break;
                        }
                }
            
                
                
            }
            catch (Exception ex)

            { Console.WriteLine(ex.Message); }

            finally

            { if (cn.State == ConnectionState.Open) cn.Close(); }

            return acceso;

        }

    }

}










