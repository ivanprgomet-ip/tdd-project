using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStore.Bll;

namespace VideoStore.Gui
{
    public class Program
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
                        gui.RentMovie();
                        break;
                    case "2":
                        gui.ReturnMovie();
                        break;
                    case "3":
                        gui.GetAllCustomers();
                        break;
                    case "4":
                        gui.RegisterNewCustomer();
                        break;
                    case "5":
                        gui.AddMovie();
                        break;
                    case "e":
                        Environment.Exit(0);
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
        IDateTime datetime;
        Bll.IVideoStore video;
        Bll.IMovieRentals rentals;

        public Gui()
        {
            video = new Bll.VideoStore(new MovieRentals(new OurDateTime()));
            rentals = new Bll.MovieRentals(datetime);
        }

        public string Menu()
        {
            Console.WriteLine("2017 \u00a9 Videostore\nIvan Prgomet & Therese Sjögren");
            Console.WriteLine("____________________________________________");
            Console.WriteLine();
            Console.WriteLine("[1] Rent a Movie");
            Console.WriteLine("[2] Return a Movie");
            Console.WriteLine("[3] Get all Customers");
            Console.WriteLine("[4] Register new Customer");
            Console.WriteLine("[5] Add new Movie");
            Console.WriteLine("[e] Exit");

            Console.Write("Enter a command >> ");

            string command = Console.ReadLine();

            return command;
        }

        public void ReturnMovie()
        {
            Console.WriteLine("Return a movie");
            Console.WriteLine("------------");
            Console.Write("Customer SSN: ");

            string customer = Console.ReadLine();

            Console.Write("Movie title: ");

            string movie = Console.ReadLine();

            try
            {
                video.ReturnMovie(movie, customer);
                Console.WriteLine($"{customer} returned {movie}.");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
        public void RentMovie()
        {
            Console.WriteLine("Rent a movie");
            Console.WriteLine("------------");
            Console.Write("Customer SSN: ");

            string customer = Console.ReadLine();

            Console.Write("Movie title: ");

            string movie = Console.ReadLine();

            try
            {
                video.RentMovie(movie, customer);
                Console.WriteLine($"{movie} added to {customer} rentals.");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
        public void RegisterNewCustomer()
        {
            Console.WriteLine("Register Customer");
            Console.WriteLine("-----------------");
            Console.Write("Enter the name: ");

            string name = Console.ReadLine();

            Console.Write("Enter the Social Security Number: ");

            string ssn = Console.ReadLine();

            try
            {
                video.RegisterCustomer(name, ssn);
                Console.WriteLine($"{name} with SSN: {ssn} registered.");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
        public void AddMovie()
        {
            Console.WriteLine("Add movie");
            Console.WriteLine("----------");
            Console.Write("Title: ");

            string title = Console.ReadLine();

            try
            {
                video.AddMovie(new Movie(title));
                Console.WriteLine($"Movie with {title} added.");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
        public void GetAllCustomers()
        {
            var allcustomers = video.GetCustomers();

            if (allcustomers != null)
            {
                if (allcustomers.Count == 0)
                    Console.WriteLine("No customers registered yet");
                else
                {
                    foreach (var c in allcustomers)
                    {
                        Console.WriteLine($"Name: {c.Name} SSN: {c.SocialSecurityNumber}");
                    }
                }
            }
            Console.ReadLine();
        }
    }

}
