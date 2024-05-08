using TBG.Data;

namespace TBG.Presentation;

public class Menu
{
<<<<<<< HEAD
    string userInput = ""; // variable to handle Console.ReadLine()

    #region -- Handlers --
=======
    Session session = new();
    IDataAccess dataAccess = new JSONFileData();
    string userInput = "";

    public void StartMenu()
    {
        bool validInput = false;
        int userSelection = 0;
        do
        {
            do
            {
                Console.Clear();
                PresentationUtility.MenuArt();
                PrintMenuArray(PresentationUtility.MenuArrays());
                try
                {
                    userSelection = Convert.ToInt32(Console.ReadLine());
                    validInput = true;
                }
                catch
                {
                    validInput = false;
                    PresentationUtility.DisplayMessage("invalid", true);
                }
            } while (!validInput); // iterates only if the user's input is *not* valid
        } while (!MainMenuSelector(userSelection));
    }

    void LoginExistingUser()
    {

    }

    void PlayGame()
    {

    }

GCNotificationStatus     void PrintMenuArray(string[] options, bool clear = false)
    {
        if (clear)
        {
            Console.Clear();
        }
        Console.WriteLine("Please make a selection: \n");
        for (int i = 0; i < options.Count(); i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
    }
>>>>>>> 62e02c4a50f4067afbd47c8cf0d2075a273eb441

    void CreateNewUser()
    {
        bool repeat = false;
        do
        {
            repeat = true;
            Console.Clear();
            Console.WriteLine("Please enter a user name: ");
            userInput = Console.ReadLine() ?? "";
            if (!dataAccess.UserExists(userInput))
            {
                repeat = false;
            }
            else
            {
                PresentationUtility.DisplayMessage("duplicate", true);
            }
        } while (repeat); // Will prompt for user input again if user name already exists
        User newUser = new(userInput);
        dataAccess.StoreUser(newUser);
        PresentationUtility.DisplayMessage("added", true);
<<<<<<< HEAD
        MenuHandler(1);
=======
        session.ActiveUser = newUser;

        // "Log user in" and move to game menu
        Console.WriteLine($"{session.ActiveUser.UserName} now logged in.");
    }

    void ExitApplication()
    {
        Environment.Exit(0);
>>>>>>> 62e02c4a50f4067afbd47c8cf0d2075a273eb441
    }

    public void MenuHandler(int menuId = 0)
    {
        bool validInput = false;
        int userSelection = 0;
        do
        {
            do
            {
                Console.Clear();
                if (menuId == 0)
                {
                    PresentationUtility.MenuArt();
                    PrintMenuArray(PresentationUtility.MenuArrays());
                }
                else if (menuId == 1)
                {
                    PrintMenuArray(PresentationUtility.MenuArrays("user"));
                }
                try
                {
                    userSelection = Convert.ToInt32(Console.ReadLine());
                    validInput = true;
                }
                catch
                {
                    validInput = false;
                    PresentationUtility.DisplayMessage("invalid", true);
                }
            } while (!validInput); // iterates only if the user's input is *not* valid
        } while (ValidMenuSelection(menuId, userSelection));
    }

    #endregion

    #region -- Helpers --

    bool MainMenuSelector(int selection)
    {
        switch (selection)
        {
            case 1:
                CreateNewUser();
                return true;
            case 2:
                return true;
            case 3:
                return true;
            default:
                Console.WriteLine("2");
                PresentationUtility.DisplayMessage("invalid", true);
                return false;
        }
    }

    bool UserMenuSelection(int selection)
    {
        switch (selection)
        {
            case 1:
            Console.WriteLine("1 selected.");
            Console.ReadLine();
                return true;
            case 2:
            Console.WriteLine("2 selected.");
            Console.ReadLine();
                return true;
            case 3:
            MenuHandler(); // Returns to main menu
                return true;
            default:
                PresentationUtility.DisplayMessage("invalid", true);
                return false;
        }
    }

    void PrintMenuArray(string[] options, bool clear = false)
    {
        if (clear)
        {
            Console.Clear();
        }
        Console.WriteLine("Please make a selection: \n");
        for (int i = 0; i < options.Count(); i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
    }

    bool ValidMenuSelection(int i, int userSelection)
    {
        if (i == 0)
        {
            return !MainMenuSelector(userSelection);
        }
        else if (i == 1)
        {
            return !UserMenuSelection(userSelection);
        }
        return false;
    }

    #endregion
}