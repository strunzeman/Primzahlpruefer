using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        List<int> Primfaktoren = new List<int>();
        //List<int> KleinstePrimfaktoren = new List<int>();
        //List<string> Commands = new List<string>();
        static void Main(string[] args)
        {
            Console.Title = "Primzahlprüfer bis 199.999";
            Menu.KonsolenMenu();
        Start:
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nWelche Zahl soll geprüft werden?");

            Program Primzahl = new Program();
            //Program EingabeVergleich = new Program();

            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            bool valid_entry = false;
            int EingabeZahl = 0;

            while (!valid_entry)
            {
                try
                {
                    Console.Write("Eingabe: ");
                    EingabeZahl = Convert.ToInt32(Console.ReadLine());
                    valid_entry = true;
                } catch
                {
                    valid_entry = false;
                    FehlerAusgabe();
                }
            }
            
            
            int p_fixed = EingabeZahl; //Fixe Instanz der Eingabe

            if (Primzahl.EingabeValid(p_fixed) == false)
            {
                FehlerAusgabe();
                goto Start;
            }

            while (EingabeZahl > 0) //Dynamische Instanz der Eingabe
            {
                Primzahl.Zerlegung(EingabeZahl);
                EingabeZahl--;
            }

            //Primzahl.KleinstePrimfaktorenSammeln(p_fixed);

            if (Primzahl.PrimPrüfungSchnell(p_fixed) == false)
            {
                Primzahl.IstKeinePrimzahlAusgabe(p_fixed);
                //Primzahl.FaktorenListeAusgabe(p_fixed);
                //Primzahl.KleinstePrimfaktorenAusgabe(p_fixed);
            }
            else
            {
                Primzahl.IstPrimzahlAusgabe(p_fixed);
                //Primzahl.FaktorenListeAusgabe(p_fixed);

            }
            goto Start;  //lazy repeat style
        }

        private static void FehlerAusgabe()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ERROR! Es kann nur eine Zahl größer als 1, kleiner als 199.999, oder ein Befehl eingegeben werden.\nProgram wird neu gestartet.");
            Console.Beep();
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        bool EingabeValid(int Eingabe)
        {
            if (Eingabe <= 1 | Eingabe > 199999)
            {
                return false;
            }
            return true;
        }
        void Zerlegung(int EingabeZahl) //Ermittelt alle möglichen Primfaktoren und füllt die Liste <Primfaktoren> damit.
        {
            int Teiler = EingabeZahl / 2;
            while (Teiler > 0) //for (int i = q; i > 0; i--)
            {
                if (EingabeZahl % Teiler != 0) //wenn q (= p/2) kein ganzer Teiler von p ist, probier die nächst kleinere Zahl.
                {
                    Teiler--;
                }
                else if (EingabeZahl % Teiler == 0 && Teiler > 1) //Wenn q ein Teiler von p ist und größer als 1 (und implizit kleiner als p ist, da "int q = p/2"), dann verlass die Schleife.
                {
                    break;
                }
                else
                {
                    Primfaktoren.Add(EingabeZahl); //wenn p nicht durch q geteilt werden kann, muss sie prim sein.
                    break;
                }
            }
        }

        void FaktorenListeAusgabe(int EingabeZahl)
        {
            int i = 0;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nAlle ermittelten Primzahlen/Faktoren bis {0} sind:", EingabeZahl);
            Console.ForegroundColor = ConsoleColor.Cyan;

            foreach (int Faktor in Primfaktoren)
            {
                i++;
                Console.Write("{0}, ", Faktor);
                if(i == Primfaktoren.Count -1)
                {
                    Console.WriteLine("{0}.", Primfaktoren[i]);
                    break;
                }
            }
        }

        /*void KleinstePrimfaktorenSammeln(int EingabeZahl)
        {
            foreach (int Faktor in Primfaktoren)
            {
                if (EingabeZahl % Faktor == 0 && EingabeZahl != Faktor) //
                {
                    KleinstePrimfaktoren.Add(Faktor);
                    //break; //Kommentar entfernen Wenn die Schleife schon beim ersten Treffer abgebrochen werden soll.
                }
            }
        }*/
        void KleinstePrimfaktorenAusgabe(int EingabeZahl)
        {
            int teiler = 1;

            do      // Führe die Schleife aus, solange der Quotient der geteilten Zahl != 1 und der Rest != 0 ist
            {
                teiler++;   // Der 1. Teiler = 2

                while (EingabeZahl % teiler == 0)  // Der Teiler bleibt gleich, solange kein Rest entsteht
                {
                    EingabeZahl = EingabeZahl / teiler;
                    Console.Write((teiler) + "*");
                }

            } while ((EingabeZahl / teiler != 1) && (EingabeZahl % teiler != 0));   // Ist der Quotient = 1 und der Rest = 0 beende die Schleife

            Console.WriteLine(EingabeZahl);
        }

        void IstPrimzahlAusgabe(int EingabeZahl)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Die Zahl {0} kann nicht in Primfaktoren zerlegt werden, sie ist prim.", EingabeZahl);
        }

        bool PrimPrüfungSchnell(int EingabeZahl)
        {
            foreach (int Faktor in Primfaktoren)
            {
                if (EingabeZahl == Faktor)
                {
                    return true;
                }
            }
            return false;
        }

        void IstKeinePrimzahlAusgabe(int EingabeZahl)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Die Zahl {0} ist nicht prim. Ihre kleinsten Primfaktoren sind ", EingabeZahl);
            int Teiler = 1;

            while(Teiler <= EingabeZahl)
            {
                Teiler++;
                while (EingabeZahl % Teiler == 0)  // Der Teiler bleibt gleich, solange kein Rest entsteht
                {
                    EingabeZahl /= Teiler;
                    Console.Write("{0} * ", Teiler);
                    if(EingabeZahl == Teiler)
                    {
                        break;
                    }
                }
                if (((EingabeZahl / Teiler) == 1) && (EingabeZahl >= 2))
                {
                    Console.Write("{0}.", EingabeZahl);
                    break;
                }
            }
        }
    }
    class Menu
    {
        public static void KonsolenMenu()
        {
            //List<string> MenuAuswahl = new List<string>()
            //{
            //    "exit",
            //    "clear",
            //    "menu",
            //    "help"
            //};

            string HeaderTop = "**************************************";
            string HeaderSides = "*                                    *";
            string HeaderMiddle = "*           Primzahlprüfer           *";
            string HeaderMiddle1 = "*                by                  *";
            string HeaderMiddle2 = "*           Mein Schatzi             *";
            string HeaderMiddle3 = "*               v1.0                 *";

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (HeaderTop.Length / 2)) + "}", HeaderTop));
            for (int i = 0; i < 10; i++)
            {
                if (i == 3)
                {
                    Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (HeaderTop.Length / 2)) + "}", HeaderMiddle));
                }
                else if (i == 4)
                {
                    Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (HeaderTop.Length / 2)) + "}", HeaderMiddle1));
                }
                else if (i == 5)
                {
                    Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (HeaderTop.Length / 2)) + "}", HeaderMiddle2));
                }
                else if (i == 6)
                {
                    Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (HeaderTop.Length / 2)) + "}", HeaderMiddle3));
                }
                else
                {
                    Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (HeaderTop.Length / 2)) + "}", HeaderSides));
                }
            }
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (HeaderTop.Length / 2)) + "}", HeaderTop));
        }
        /*void MenuUI()
        {

        }
        void MenuPunktWählen(string input)
        {
            switch (input)
            {
                case "exit":
                    break;
                case "clear":
                    Console.Clear();
                    break;
                case "menu":
                    Console.WriteLine("Welche Option möchten Sie umstellen?");
                    break;
                case "help":
                    Console.WriteLine("Mögliche Befehle: exit, clear, menu, help.");
                    break;
            }

        }*/
    }
}