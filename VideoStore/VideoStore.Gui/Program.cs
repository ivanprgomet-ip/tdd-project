using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.Gui
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice = string.Empty;
            Gui gui = new Gui();
            while (choice != "e")
            {
                choice = gui.Menu();

                switch (choice)
                {
                    case "1":
                        // rent a movie
                        break;
                    case "2":
                        // return a movie
                        break;
                    case "3":
                        // get all movies
                        break;
                    case "4":
                        // get all customers
                        break;
                    case "5":
                        // register new customer
                        break;
                    case "6":
                        // add new movie
                        break;
                    case "e":
                        // exit application
                        break;
                    default:
                        break;

                }
                Console.Clear();
            }

        }
    }

    public class Gui
    {
        public string Menu()
        {
            Console.WriteLine("2017 \u00a9 Videostore\nIvan Prgomet & Therese Sjögren");
            Console.WriteLine("____________________________________________");
            Console.WriteLine();
            Console.WriteLine("[1] Rent a Movie");
            Console.WriteLine("[2] Return a Movie");
            Console.WriteLine("[3] Get all Movies");
            Console.WriteLine("[4] Get all Customers");
            Console.WriteLine("[5] Register new Customer");
            Console.WriteLine("[6] Add new Movie");
            Console.WriteLine("[e] Exit");

            Console.Write("Enter a command >> ");

            string command = Console.ReadLine();

            return command;
        }
    }
}
