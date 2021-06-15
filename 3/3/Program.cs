using Org.BouncyCastle.Security;
using System;
using System.Security.Cryptography;
using System.Text;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string key = "";
                if (args.Length - 1 < 3)
                {
                    Console.WriteLine("args must be >= 3");
                    break;
                }
                else
                {

                    SecureRandom number = new SecureRandom();
                    int pcMove = 0;
                    pcMove = number.Next(1, args.Length);

                    string choisePK = args[pcMove];
                    byte[] hmac = hmacSHA256(choisePK, ref key);
                    Console.WriteLine("HMAC:" + BitConverter.ToString(hmac).Replace("-", "").ToLower());

                    Console.WriteLine("Available move:");
                    Console.WriteLine("0 - exit");
                    for (int i = 1; i < args.Length; i++)
                    {
                        string argument = args[i];
                        Console.Write(i); // Write index
                        Console.Write(" - ");
                        Console.WriteLine(argument); // Write string

                    }
                    Console.Write("Enter your move: ");
                    int n = Convert.ToInt32(Console.ReadLine());
                    if (n == 0)
                    {
                        break;
                    }
                    string ur = args[n];
                    Console.WriteLine("Your move: " + ur);
                    Console.WriteLine("Computer move: " + choisePK);

                    if (n == pcMove)
                    {
                        Console.WriteLine("Draw");
                    }
                    int length = args.Length - 1 / 2;
                    if (pcMove < n)
                    {
                        Console.WriteLine("You lose");
                    }
                    else if (pcMove > n)
                    {
                        Console.WriteLine("You win");
                    }
                    Console.WriteLine("HMAC key:" + key);
                }

            }
            static byte[] hmacSHA256(String data, ref string key)
            {
                key += RandomString(32);
                using (HMACSHA256 hmac = new HMACSHA256(Encoding.ASCII.GetBytes(key)))
                {
                    return hmac.ComputeHash(Encoding.ASCII.GetBytes(data));
                }
            }
            static string RandomString(int length)
            {  
                char[] allowableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZqwertyuiopasdfghjklzxcvbnm1234567890".ToCharArray();
                SecureRandom random = new SecureRandom();
                string str = "";
                for (var i = 0; i < length; i++)
                {
                    str += allowableChars[random.Next(0, 60)].ToString();
                }
                return new string(str);
            }
        }
    }
}
