using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Channels;

namespace MiniRäknare
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            string meddelande = "HÄR ÄR MINIRÄKNAREN, VÄLKOMMEN!";

            for (int i = 0; i < meddelande.Length; i++)
            {
                Console.Write(meddelande[i]);
                if (meddelande[i] == '.')
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    Thread.Sleep(100);
                }

            }

            Console.ResetColor();

            double nummer1, nummer2, resultat;
            char operation;

            List<double> historik = new List<double>();

            do
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.White;

                nummer1 = AnvändareTal("Mata in tal");
                Console.WriteLine();

                operation = AnvändareOperation();
                Console.WriteLine();

                nummer2 = AnvändareTal("Mata in tal");
                Console.WriteLine();


                Console.ResetColor();

                resultat = UtförOperation(nummer1, nummer2, operation);

                if (!double.IsNaN(resultat))
                {

                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine("Resultat är: " + resultat);
                    historik.Add(resultat);

                    Console.ResetColor();

                }
                else
                {

                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("FEL!. Du kan inte dela med '0'.");
                    Console.Beep();
                    Console.WriteLine();

                    Console.ResetColor();
                }

                if (VisaHistorik())
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("Tidigare resultat är: ");
                    Console.WriteLine(
                        );
                    foreach (var result in historik)
                    {
                        Console.WriteLine(result);
                    }
                }

                Console.ResetColor();

            }
            while (FortsättBeräkning());

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("MINIRÄKNAREN STÄNGS");

            Console.ResetColor();

        }

        static double AnvändareTal(string prompt)
        {
            double number;
            while (!double.TryParse(AnvändareInput(prompt), out number))
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("FEL TAL! försök igen");
                Console.Beep();
                Console.WriteLine();

                Console.ResetColor();


            }
            return number;
        }

        static char AnvändareOperation()
        {
            char operation;
            while (!char.TryParse(AnvändareInput("Mata in matematisk operation (+, -, *, /) "), out operation) || !OperationKontroll(operation))
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("FEL MATEMATISK OPERATION! försök igen");
                Console.Beep();
                Console.WriteLine();

                Console.ResetColor();
            }
            return operation;
        }

        static string AnvändareInput(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(prompt);
            return Console.ReadLine();


        }

        static bool OperationKontroll(char operation)
        {
            return operation == '+' || operation == '-' || operation == '*' || operation == '/';

        }

        static double UtförOperation(double nummer1, double nummer2, char operation)
        {
            switch (operation)
            {
                case '+':
                    return nummer1 + nummer2;
                case '-':
                    return nummer1 - nummer2;
                case '*':
                    return nummer1 * nummer2;
                case '/':
                    if (nummer2 != 0)
                    {
                        return nummer1 / nummer2;
                    }
                    else
                    {
                        return double.NaN;
                    }
                default:
                    return double.NaN;


            }
        }

        static void VisaResultat(List<double> historik)
        {
            Console.WriteLine("Tidigare resultat är: ");
            Console.WriteLine();
            foreach (var resultat in historik)
            {
                Console.WriteLine(resultat);
            }
        }

        static bool VisaHistorik()
        {
            Console.WriteLine();
            while (true)
            {

                string svar = AnvändareInput("Skriv 'ja' om du vill visa tidigare resultat. Annars skriv 'nej'").ToLower();
                if (svar == "ja")
                {
                    return true;
                }

                else if (svar == "nej")
                {
                    return false;
                }

            }

        }
            static bool FortsättBeräkning()

            {
                Console.WriteLine();
                while (true)
                {
                   string svar = AnvändareInput("Skriv 'ja' om du vill fortsätta med nya beräkningar. Annars skriv 'nej'").ToLower();
                    if(svar == "ja")
                    {
                        return true;

                    }
                    else if(svar == "nej")
                    {
                        return false;
                    }

                }

            }
        }
    }


