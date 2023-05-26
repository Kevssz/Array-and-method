using System;
using System.Text;

namespace MilkTeaShop
{
    class Program
    {
        static decimal[] sizePrices = { 80m, 100m };
        static decimal[] addonPrices = { 0m, 20m, 10m, 15m, 30m, 25m };
        static string[] flavors = { "Classic", "Taro", "Matcha", "Strawberry", "Red Velvet" };

        static void Main(string[] args)
        {
            Console.Write("Enter the number of orders: ");
            int numberOfOrders;
            if (!int.TryParse(Console.ReadLine(), out numberOfOrders) || numberOfOrders < 1)
            {
                Console.WriteLine("Invalid number of orders. Exiting the program.");
                return;
            }

            decimal totalPrice = 0;

            for (int i = 0; i < numberOfOrders; i++)
            {
                Console.WriteLine($"\nOrder #{i + 1}");

                bool placeAnotherOrder = true;

                while (placeAnotherOrder)
                {
                    Console.WriteLine("\nWelcome to the Milk Tea Shop!");
                    DisplayFlavors();
                    DisplaySizes();
                    DisplayAddons();

                    int flavorIndex = GetSelection("flavor", flavors.Length);
                    string selectedFlavor = flavors[flavorIndex - 1];

                    int sizeIndex = GetSelection("size", sizePrices.Length);
                    bool isLargeSize = (sizeIndex == 2);

                    int addonIndex = GetSelection("add-on (0 for none)", addonPrices.Length);
                    string selectedAddon = (addonIndex == 0) ? "None" : flavors[addonIndex - 1];

                    decimal price = sizePrices[sizeIndex - 1] + addonPrices[addonIndex];

                    totalPrice += price;

                    DisplayOrderSummary(selectedFlavor, isLargeSize, selectedAddon, price);

                    Console.Write("\nPlace another order? (yes/no) ");
                    string userInput = Console.ReadLine();

                    if (userInput.ToLower() != "yes")
                    {
                        placeAnotherOrder = false;
                    }
                }
            }

            Console.WriteLine($"\nTotal price for all orders: ₱{totalPrice:F2}");

            Console.WriteLine("\nPayment Options:");
            Console.WriteLine("Cash");

            decimal cashAmount;
            Console.Write("Enter the amount in cash: ");
            if (!decimal.TryParse(Console.ReadLine(), out cashAmount) || cashAmount < totalPrice)
            {
                Console.WriteLine("Invalid cash amount or insufficient funds. Exiting the program.");
                return;
            }

            decimal change = cashAmount - totalPrice;

            Console.WriteLine($"Payment successful! Change: ₱{change:F2}. Your order has been placed.");

            Console.WriteLine("\nThank you for shopping with us!");
        }

        static void DisplayFlavors()
        {
            Console.WriteLine("Available Flavors:");
            for (int i = 0; i < flavors.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {flavors[i]}");
            }
        }

        static void DisplaySizes()
        {
            Console.WriteLine("Available Sizes:");
            Console.WriteLine($"1. Regular: ₱{sizePrices[0]}");
            Console.WriteLine($"2. Large: ₱{sizePrices[1]}");
        }

        static void DisplayAddons()
        {
            Console.WriteLine("Available Add-ons:");
            Console.WriteLine($"0. None");
            for (int i = 0; i < flavors.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {flavors[i]}: ₱{addonPrices[i + 1]}");
            }
        }

        static int GetSelection(string selectionType, int arrayLength)
        {
            int selectedIndex;
            while (true)
            {
                Console.Write($"Enter the number corresponding to the {selectionType}: ");
                if (int.TryParse(Console.ReadLine(), out selectedIndex) && selectedIndex >= 0 && selectedIndex <= arrayLength)
                {
                    break;
                }
                Console.WriteLine("Invalid selection. Please try again.");
            }
            return selectedIndex;
        }

        static void DisplayOrderSummary(string flavor, bool isLargeSize, string addon, decimal price)
        {
            string size = isLargeSize ? "Large" : "Regular";

            StringBuilder summaryBuilder = new StringBuilder();
            summaryBuilder.AppendLine("\nOrder Summary:");
            summaryBuilder.AppendLine("---------------");
            summaryBuilder.AppendLine($"Flavor: {flavor}");
            summaryBuilder.AppendLine($"Size: {size}");
            summaryBuilder.AppendLine($"Add-on: {addon}");
            summaryBuilder.AppendLine($"Price: ₱{price:F2}");

            Console.WriteLine(summaryBuilder.ToString());
        }
    }
}
