using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Cryptography;
using System.Text;

namespace ProyectoLogin.Recursos
{
    public class Utilidades
    {
        public static long ExpMod(long baseNumber, long exp, long m)
        {
            if (exp == 0)
                return 1;
            long temp = ExpMod(baseNumber, exp / 2, m);
            temp = (temp * temp) % m;
            if (exp % 2 == 1)
                temp = (temp * baseNumber) % m;
            return temp;
        }

        public static string CifrarRSA(string clave)
        {
            long e = 7;
            long p = 97;
            long q = 79;
            long n = p * q;

            int[] mensajeCifrado = new int[clave.Length];
            int i = 0;
            foreach (char c in clave)
            {
                mensajeCifrado[i] = (int)c; 
                i++;
            }

            // Cifrar mensaje
            for (int j = 0; j < i; j++)
            {
                mensajeCifrado[j] = (int)ExpMod(mensajeCifrado[j], e, n);
            }

            Console.WriteLine("AQUI VA LO CIFRADO");
            string resultadoCifrado = string.Join(" ", mensajeCifrado.Select(x => x.ToString().TrimStart('0')));
            Console.WriteLine(resultadoCifrado);
            return resultadoCifrado;
        }

        public static string DescifrarRSA(string claveCifrada)
        {
            long e = 7;
            long p = 97;
            long q = 79;
            long n = p * q;
            long r = (p - 1) * (q - 1);

            long d = ModInverse(e, r); 

            string[] partes = claveCifrada.Split(' ');

            List<int> mensajeCifrado = new List<int>();

            foreach (string parte in partes)
            {
                if (!string.IsNullOrEmpty(parte) && parte != "0")
                {
                    if (int.TryParse(parte, out int numero))
                    {
                        mensajeCifrado.Add(numero);
                    }
                    else
                    {
                    }
                }
            }

            // Descifrar mensaje
            List<char> mensajeDescifrado = new List<char>();
            foreach (int numero in mensajeCifrado)
            {
                mensajeDescifrado.Add((char)ExpMod(numero, d, n));
            }

            Console.WriteLine("Mensaje descifrado:");
            Console.WriteLine(string.Concat(mensajeDescifrado));
            return string.Concat(mensajeDescifrado);
        }
        public static long ModInverse(long a, long m)
        {
            for (long x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                {
                    return x;
                }
            }
            throw new ArithmeticException("No existe inverso multiplicativo");
        }
    }
}
