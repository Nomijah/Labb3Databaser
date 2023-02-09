using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3Databaser.Models;

namespace Labb3Databaser.Interface
{
    internal class InterfaceMethods
    {
        public static int CheckInput(int menuNum)
        {
            int response = -1;
            do
            {
                try
                {
                    response = Convert.ToInt32(Console.ReadLine());
                    if (response < 1 || response > menuNum)
                    {
                        Console.WriteLine("Du har angivit fel siffra, välj en" +
                            " siffra från 1 - " + menuNum);
                        response = -1;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Du måste ange en siffra, försök igen.");
                }
            } while (response < 1);
            return response;
        }

        public static bool FirstName()
        {
            Console.WriteLine("Vill du sortera på [1] förnamn eller " +
                "[2] efternamn?");
            int choice = CheckInput(2);
            if (choice == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Ascending()
        {
            Console.WriteLine("Vill du använda [1] stigande eller " +
                "[2] fallande bokstavsordning?");
            int choice = CheckInput(2);
            if (choice == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void PrintStudents(IOrderedQueryable list)
        {
            int counter = 1;
            Console.WriteLine();
            foreach (Student item in list)
            {
                Console.WriteLine($"{counter}. {item.FName} {item.LName}");
                counter++;
            }
            PressToCont();
        }

        public static void PrintClasses(IOrderedQueryable list)
        {
            Console.WriteLine();
            foreach (Class item in list)
            {
                Console.WriteLine($"{item.ClassId}. {item.ClassName}");
            }
        }

        public static string UserStringInput()
        {
            int userChoice = 2;
            string input = "error";
            do
            {
                input = Console.ReadLine();
                Console.WriteLine($"Du har valt {input}, vill du godkänna det?" +
                    $"\n[1] Ja\n[2] Nej");
                userChoice = CheckInput(2);
                if (userChoice == 2)
                {
                    Console.WriteLine("Ange text på nytt:");
                }
            } while (userChoice == 2);
            return input;
        }

        public static int UserIntInput()
        {
            int userChoice = 2;
            int input = 0;
            do
            {
                while (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Ange endast siffror, försök igen:");
                }
                Console.WriteLine($"Du har valt {input}, vill du godkänna det?" +
                    $"\n[1] Ja\n[2] Nej");
                userChoice = CheckInput(2);
                if (userChoice == 2)
                {
                    Console.WriteLine("Ange siffror på nytt:");
                }
            } while (userChoice == 2);
            return input;
        }

        public static void PressToCont()
        {
            Console.WriteLine("\nTryck på valfri tangent för att återgå till" +
                " menyn.");
            Console.ReadKey();
        }
    }
}
