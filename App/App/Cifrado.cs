using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class Cifrado
    {
        public static string Cifrar(string mensaje)
        {
            
            int num = int.Parse(mensaje.Substring(1));
            num=num*7;
            mensaje = mensaje.Substring(0, 1) + num.ToString();

            return mensaje;
        }
    }
}
