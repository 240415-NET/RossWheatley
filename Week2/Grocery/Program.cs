namespace Grocery;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter your grocery list (separated with commas): ");
        string userInput = Console.ReadLine();
        string[] groceryItems = userInput.Split(",");
        int[] itemsInCart = new int[groceryItems.Count()];
        bool repeat;
        int itemNumber = 0;

        do
        {
            repeat = false;
            Console.Clear();
            Console.WriteLine("Your list (items in cart): ");
            for (int i = 0; i < groceryItems.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. {groceryItems[i]}: {itemsInCart[i].ToString()}");
            }

            Console.WriteLine("Input corresponding # to place item in cart. \nInput rm + # to remove. \nInput q to quit:");

            userInput = Console.ReadLine();

            if (userInput.ToLower() == "q")
            {
                repeat = false;
            }
            else if (userInput.Contains("rm"))
            {
                int numbers = Convert.ToInt32(new String(userInput.Where(Char.IsDigit).ToArray()));

                if (numbers <= groceryItems.Count() && itemsInCart[numbers - 1] - 1 >= 0)
                {
                    itemsInCart[numbers - 1] -= 1;
                }
                else
                {
                    Console.WriteLine("Invalid format. Please try again. (Press any key to continue.)");
                    Console.ReadKey();
                }
                repeat = true;
            }
            else
            {
                try
                {
                    itemNumber = Convert.ToInt32(userInput);
                    if (itemNumber <= groceryItems.Count())
                    {
                        itemsInCart[itemNumber - 1] += 1;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid format. Please try again. (Press any key to continue.)");
                    Console.ReadKey();
                }
                repeat = true;
            }
        } while (repeat);
    }
}
