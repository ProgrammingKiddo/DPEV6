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
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(mensaje);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
            /*  
               int num = int.Parse(mensaje.Substring(1));
               num = num/7;
               mensaje = mensaje.Substring(0, 1) + num.ToString();

               return mensaje;*/
        }
    }
}
