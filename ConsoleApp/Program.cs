using System;

namespace ConsoleApp
{
    class Program
    {
        //public int prim = Convert.ToInt32(Console.ReadLine());
        static void Main(string[] args)
        {
            Program Teiler = new Program();
            Console.WriteLine("Bis zu welcher Zahl sollen die Primzahlen ausgegeben werden?");
            int p = Convert.ToInt32(Console.ReadLine());
            while (p > 0)
            {
                Teiler.Zerlegung(p);
                p--;
            }
        }
        void Zerlegung(int p)
        {
            int q = p / 2;

            for (int i = q; i > 0; i--)
            {
                if (p % q != 0)
                {
                    q--;
                }
                else if (p % q == 0 && q > 1)
                {
                    //Console.WriteLine("{0} ist keine Primzahl.", p);
                    break;
                }
                else
                {
                    Console.WriteLine("{0} ist eine Primzahl.", p);
                    break;
                }
            }
        }
    }
}