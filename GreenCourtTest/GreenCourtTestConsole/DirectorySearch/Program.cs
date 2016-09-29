using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;
using System.Web;

namespace NumberRental
{
    class Program
    {
        static string config = "";

        static List<RentalNumber> rentalList = new List<RentalNumber>();

        static void Main(string[] args)
        {

        bool notquit = true;

        InitializeNumberList();

        do {
 
            Menu();

            ConsoleKeyInfo keyboardInput;
  
            keyboardInput = Console.ReadKey(true);

            switch (keyboardInput.KeyChar.ToString())
            {
                case "1" :

                    WriteConfig();
                    break;
  
                case "2" :

                    CheckOutRentalNumber(rentalList);
                    break;

                case "3":

                    ReturnRentalNumber(rentalList);
                    break;

               case "4":

                    ListRented(rentalList);
                    break;

                default:
                        notquit = false;
                        return;
               }

            } while (notquit);

        }

    static public void Menu()
        {

            Console.WriteLine(@"Press ""1"" to Configure limit of rental numbers.");
            Console.WriteLine(@"Press ""2"" to Check out a rental number.");
            Console.WriteLine(@"Press ""3"" to Return a rental number.");
            Console.WriteLine(@"Press ""4"" to Get current status of rentals.");
            Console.WriteLine(@"Press ""ESCAPE"" to Exit.");
        }

    static string ReadConfig()
    {
        try
        {
            StreamReader sr = new StreamReader(@"C:\temp\config.txt");
            config = sr.ReadLine();
            sr.Close();
 
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message + "Make sure the config.txt file is in the temp folder.");  // C:\temp\config.txt missing or inaccessible
            Console.ReadKey();
        }
        finally
        {
   
        }

        return config;
    }

    public static void WriteConfig()
    {

        Console.WriteLine("How many numbers should be make available?");

        config = Console.ReadLine();

            try
            {
                StreamWriter sw = new StreamWriter(@"C:\temp\config.txt");

                sw.WriteLine(config);    

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message + "Ensure that you have access to the temp folder");        // C:\temp inaccessible
            }
            finally
            {
               InitializeNumberList();
            }

        Console.WriteLine("There will be " + config.ToString() + " numbers available to rent.");

        }

    public static void InitializeNumberList()
    {
            int rangeTopLimit = 10;

            try
            {
                 rangeTopLimit = Convert.ToInt32(ReadConfig());
            }
            catch {
                Console.WriteLine("Number of rentals defaulted to 10.  Press a key to continue...");        // if config.txt corrupted
                Console.ReadKey();

            }
            finally
            {

            }


        for (int i = 0; i < rangeTopLimit; i++)
        {
            RentalNumber rentable = new RentalNumber();
            rentable.NumberName = i;
            rentable.Rented = false;
            rentalList.Add(rentable);
        }
      }


    protected static void ReturnRentalNumber(List<RentalNumber> rentalList)
    {

        Console.WriteLine("Which number do you want to return?");

        var returnedResponse = Console.ReadLine();

        int returnedNumber = Convert.ToInt32(returnedResponse);

        rentalList[returnedNumber].Rented = false;

      }


      protected static void CheckOutRentalNumber(List<RentalNumber> rentalList)
      {

        Console.WriteLine("Which number do you want to rent?");

        var rentedResponse = Console.ReadLine();

        int rentedNumber = Convert.ToInt32(rentedResponse);
        
        if (rentalList[rentedNumber].Rented == true)
            {
                Console.WriteLine("Already rented - please pick a different number");
            } else
            {
                rentalList[rentedNumber].Rented = true;
            }
       }

        protected static void ListRented(List<RentalNumber> rentalList)
        {

            int count = rentalList.Count();
            for (int i = 1; i < count; ++i)
            {
                Console.WriteLine("Number {0} is {1}", rentalList[i].NumberName, rentalList[i].Rented ? "rented" : "not rented" );
            }
        }

    }
    }


  

