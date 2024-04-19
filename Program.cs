namespace RossWheatley;
/*
In this Hackathon we want to create a C# console application with the following requirments.

1 - Prompts the user for some input
2 - Does something with that input
3 - Handles any exceptions that may arise during the running of the application (no hard crashing)
4 - Continues to run until the user quits the application, from within the application (no ctrl+c)

We also want to make sure this code is visible to all of us! Have someone share their screen and 
type for the group, making sure that they are inside of their personal repo on this organization. 

We want to then push this code up before we leave today. I will compile links to their repo's and 
make a teams post with links to the different groups' work for future reference. 
*/

// Census Survey
// "how many people are in your house?"

class Program
{
    static void Main(string[] args)
    {
        bool repeat;
        string userInput = "";

        int householdCount = 0;

        do
        {
            do
            {
                Console.WriteLine("How many people live in your house?");
                repeat = false;
                try
                {
                    householdCount = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                    Console.WriteLine("Make sure you enter an integer!");
                    repeat = true;
                }
                // Do something with user's input
                repeat = HandleHouseholdSize(householdCount);

            } while (repeat);

            Console.WriteLine("Would you like to answer this question again? (Y/N)");
            userInput = Console.ReadLine();
        } while (RepeatProgram(userInput));
    }

    private static bool RepeatProgram(string userInput)
    {
        if (userInput == "Y")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Handles user's household size, comments on the household size, and rejects unacceptable input
    private static bool HandleHouseholdSize(int i)
    {
        if (i < 1)
        {
            Console.WriteLine("Hmmmm... That doesn't seem right.");
            return true;
        }
        else if (i >= 1 && i < 5)
        {
            Console.WriteLine("Thank you.");
            return false;
        }
        else if (i >= 5 && i < 10)
        {
            Console.WriteLine("Oh, that's a pretty big family.");
            return false;
        }
        else if (i >= 10 && i <= 100)
        {
            Console.WriteLine("Wow...");
            return false;
        }
        else if (i > 100)
        {
            Console.WriteLine("You cannot enter a value greater than 100.");
            return true;
        }
        return false;
    }
}
