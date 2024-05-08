using TBG.Data;

namespace TBG.Presentation;

public class Menu
{
    Session session = new();
    IDataAccess dataAccess = new JSONFileData();
    string userInput = "";

    #region -- Handlers --
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
        session.ActiveUser = newUser;
        Console.Clear();
        PresentationUtility.ShowLoadingAnimation(2, 100);
        MenuHandler(1);
    }

    void ExitApplication()
    {
        Environment.Exit(0);
    }

    public void MenuHandler(int menuId = 0)
    {
        bool repeat;
        int userSelection = 0;
        do
        {
            do
            {
                repeat = true;
                Console.Clear();

                if (menuId == 0)
                {
                    PresentationUtility.MenuArt();
                }

                PrintMenuArray(PresentationUtility.MenuArrays(menuId));

                userInput = Console.ReadLine();

                try
                {
                    userSelection = Convert.ToInt32(userInput);
                    repeat = false;
                }
                catch
                {
                    repeat = true;
                    PresentationUtility.DisplayMessage("invalid", true);
                }

            } while (repeat); // iterates only if the user's input is *not* valid

        } while (ValidMenuSelection(userSelection, PresentationUtility.MenuArrays(menuId)));
        
        MenuSelector(menuId, userSelection);
    }

    #endregion

    #region -- Helpers --

    void MenuSelector(int menuId, int selection)
    {
        switch (menuId)
        {
            case 0:
                switch (selection)
                {
                    case 1:
                        CreateNewUser();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    default:
                        PresentationUtility.DisplayMessage("invalid", true);
                        break;
                }
                break;
            case 1:
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("1 selected.");
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("2 selected.");
                        Console.ReadLine();
                        break;
                    case 3:
                        MenuHandler(); // Returns to main menu
                        break;
                    default:
                        PresentationUtility.DisplayMessage("invalid", true);
                        break;
                }
                break;
            default:
                PresentationUtility.DisplayMessage();
                MenuHandler();
                break;

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

    bool ValidMenuSelection(int userSelection, string[] options)
    {
        return userSelection <= options.Length;
    }

    #endregion
}