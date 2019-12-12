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
                try
                {
                    
                        Socket listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        Socket conexion;
                        IPEndPoint connect = new IPEndPoint(IPAddress.Parse("192.168.1.81"), 5000);
                        int conexiones = 10;
                       // while (conexiones > 0)
                        //{
                        Console.WriteLine("esperando conexion");
                        listen.Bind(connect);

                        listen.Listen(10);

                        conexion = listen.Accept();

                        Console.WriteLine(conexiones);
                        //recibir
                        byte[] recibir_info = new byte[100];
                        string data = "";
                        int array_size = 0;

                        array_size = conexion.Receive(recibir_info, 0, recibir_info.Length, 0);
                        Array.Resize(ref recibir_info, array_size);
                        data = Encoding.Default.GetString(recibir_info);

                        int poscoma = data.IndexOf(",");
                        string usuario = data.Substring(0, poscoma);
                        string contraseña = data.Substring(poscoma + 1);
                        Console.WriteLine(usuario);
                        Console.WriteLine(contraseña);
                        string acceso = Uca.Credenciales(usuario, contraseña);
                        //enviar
                        /* Console.WriteLine(acceso);
                         byte[] enviar_info = new byte[100];
                         string data1 = acceso + ",";

                         enviar_info = Encoding.Default.GetBytes(data1);
                         Console.WriteLine(acceso);
                         listen.Send(enviar_info);
                         Console.WriteLine("enviar");
                         */
                        //listen.Shutdown(SocketShutdown.Both);

                        //conexiones = conexiones - 1;
                   // }
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine("hay algun fallo");
                }

            }

        }
        public class Uca
        {
            public static string Credenciales(string usuario, string contraseña)
            {   string acceso = "";
                if (usuario != "" && contraseña != "")
                {
                    
                    
                    string ldapHost = "ldap.uca.es";
                    int ldapPort = 389;
                    string loginDN = "CN=" + usuario + ",dc=uca,dc=es";
                    LdapConnection conn = new LdapConnection();

                    conn.Connect(ldapHost, ldapPort);
                    conn.Bind(loginDN, contraseña);
                    if (conn.Bound)
                        acceso = "1";

                    else
                    {
                        acceso = "2";
                    }
                }
                return acceso;
            }


        }
    }
}
