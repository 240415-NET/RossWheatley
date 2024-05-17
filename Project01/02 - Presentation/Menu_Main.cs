using TBG.Logic;

namespace TBG.Presentation;

public class Menu_Main
{
    public void CreateNewUser(Menu menu)
    {
        bool repeat = false;
        string userInput;
        do
        {
            repeat = true;
            Console.Clear();
            Console.WriteLine("Please enter a user name: ");
            userInput = Console.ReadLine() ?? "";

            repeat = LoginHandler.CheckUserExists(userInput); // repeat if user already exists

            if (repeat)
            {
                // User already exists
                PresentationUtility.DisplayMessage("duplicate", true);
            }
        } while (repeat); // Will prompt for user input again if user name already exists

        if (LoginHandler.CreateNewUser(userInput))
        {
            PresentationUtility.DisplayMessage("added");
            // Proceed to login if success
            Console.Clear();
            PresentationUtility.ShowLoadingAnimation();
            menu.Builder(1);
        }
        else
        {
            // Go back to main menu if something fails
            PresentationUtility.DisplayMessage();
            menu.Builder();
        }
    }

    public void FindExistingUser(Menu menu)
    {
        Console.Clear();
        Console.WriteLine("Enter your username:");
        string userInput = Console.ReadLine() ?? "";

        if (LoginHandler.FindExistingUser(userInput))
        {
            Console.Clear();
            PresentationUtility.ShowLoadingAnimation();
            menu.Builder(1);
        }
        else
        {
            PresentationUtility.DisplayMessage("notfound", true);
            menu.Builder();
        }
    }
}