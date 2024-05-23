using TBG.Logic;

namespace TBG.Presentation;

public class Menu_Main
{
    public void CreateNewUser(Menu menu)
    {
        bool repeat = false;
        string userInput = String.Empty;
        string password = String.Empty;
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

        do
        {
            repeat = true;
            Console.Clear();
            Console.WriteLine("Enter a password: ");
            password = Console.ReadLine() ?? "";
            repeat = PasswordInputInvalid(password);
            if (repeat)
            {
                PresentationUtility.DisplayMessage("invalid");
            }
        } while (repeat);


        if (LoginHandler.CreateNewUser(userInput, password))
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
        bool repeat = true;
        Console.Clear();
        Console.WriteLine("Enter your username:");
        string userName = Console.ReadLine() ?? "";
        string password;

        if (LoginHandler.CheckUserExists(userName))
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Enter password:");
                password = Console.ReadLine() ?? "";
                repeat = PasswordInputInvalid(password);
                if (!repeat) // if password input is valid
                {
                    if (LoginHandler.Login(userName, password))
                    {
                        repeat = false;
                    }
                    else
                    {
                        repeat = true;
                        // need incorrect password message
                        PresentationUtility.DisplayMessage("password");
                    }
                }
                else // if password input is not valid
                {
                    PresentationUtility.DisplayMessage("invalid");
                }
            } while (repeat);

            Console.Clear();
            PresentationUtility.ShowLoadingAnimation();

            menu.Builder(1);
        }
        else
        {
            PresentationUtility.DisplayMessage("notfound");
            menu.Builder();
        }
    }

    bool PasswordInputInvalid(string input) // Just checks to make sure input is not blank
    {
        if (input == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}