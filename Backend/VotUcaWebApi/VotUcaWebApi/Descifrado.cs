using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotUcaWebApi
{
    public class Descrifado
    {
        public static string Login(string mensaje)
        {
           
            int num = int.Parse(mensaje.Substring(1));
            num = num/7;
            mensaje = mensaje.Substring(0, 1) + num.ToString();
            
            return mensaje;
        }
    }
}
