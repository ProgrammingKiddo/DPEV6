using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Sockets;
using System.Text;

using Novell.Directory.Ldap;

namespace VotUcaWebApi
{
    public class Startup
    {
      
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //int i = 0;
           // while (i < 10)
            //{
                Conectar.Union();
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                app.UseMvc();
                app.Run(async (context) =>
                {
                    await context.Response.WriteAsync("No hay datos para mostrar.");
                });
            //}
        }

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
                    IPAddress ipAddress = IPAddress.Parse("10.183.114.57");
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
 
                            byte[] recibir_info = new byte[100];
                          
                            int array_size = 0;

                            array_size = handler.Receive(recibir_info, 0, recibir_info.Length, 0);
                            Array.Resize(ref recibir_info, array_size);
                            data = Encoding.Default.GetString(recibir_info);

                            int poscoma = data.IndexOf(",");
                            string usuario = data.Substring(0, poscoma);
                            string contraseña = data.Substring(poscoma + 1);


                        string acceso = Uca.Credenciales(usuario, contraseña);
                       
                            // Show the data on the console.  
                            Console.WriteLine("Text received : {0}", data);

                            // Echo the data back to the client.  
                            byte[] msg = Encoding.ASCII.GetBytes(acceso+",");
                            
                            handler.Send(msg);
                            handler.Shutdown(SocketShutdown.Both);
                            handler.Close();
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }

                    Console.WriteLine("\nPress ENTER to continue...");
                    Console.Read();

                }

                
            }


        }

        }
        public class Uca
        {
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
                
            }catch(LdapException e) {

            
            }
        
                return acceso;
            }


        }
   