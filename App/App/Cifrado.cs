using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class Cifrado
    {
        public static string Cifrar(string mensaje)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(mensaje);
            result = Convert.ToBase64String(encryted);
            return result;
            /* int num = int.Parse(mensaje.Substring(1));
             num=num*7;
             mensaje = mensaje.Substring(0, 1) + num.ToString();

             return mensaje;
         }*/
        }
    }
}
