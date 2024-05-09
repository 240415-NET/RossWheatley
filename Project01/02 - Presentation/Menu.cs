using TBG.Data;

namespace TBG.Presentation;

public class Menu
{
    Session session = new();
    IDataAccess dataAccess = new JSONFileData();
    string userInput = "";

    #region -- Handler & Selector -- 

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
                PresentationUtility.ShowLoadingAnimation();
                Console.Clear();

                PresentationUtility.MenuHeader(session, menuId);

                PrintMenuArray(PresentationUtility.MenuArrays(menuId));

                userInput = Console.ReadLine() ?? "";

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

        } while (!ValidMenuSelection(userSelection, PresentationUtility.MenuArrays(menuId)));

        MenuSelector(menuId, userSelection);
    }

    void MenuSelector(int menuId, int selection)
    {
        switch (menuId)
        {
            case 0: // Main Menu
                switch (selection)
                {
                    case 1:
                        CreateNewUser();
                        break;
                    case 2:
                        FindExistingUser();
                        break;
                    case 3:
                        ExitApplication();
                        break;
                    default:
                        PresentationUtility.DisplayMessage("invalid", true);
                        break;
                }
                break;
            case 1: // User Menu
                switch (selection)
                {
                    case 1:

                        break;
                    case 2:
                        CreateNewGame();
                        break;
                    case 3: // Return to main
                        MenuHandler(); // Returns to main menu
                        break;
                    default:
                        PresentationUtility.DisplayMessage("invalid", true);
                        break;
                }
                break;
            case 2: // Save Menu
                switch (selection)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        MenuHandler(1);
                        break;
                    case 5:
                        MenuHandler();
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

    #endregion 

    #region -- Main Menu --

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
        Login(newUser);
    }

    void ExitApplication()
    {
        // Persist all data before close
        Environment.Exit(0);
    }

    void Login(User user)
    {
        session.ActiveUser = user;
        Console.Clear();
        PresentationUtility.ShowLoadingAnimation();
        MenuHandler(1);
    }

    void FindExistingUser()
    {
        Console.WriteLine("Enter your username:");
        userInput = Console.ReadLine() ?? "";

        if (dataAccess.UserExists(userInput))
        {
            Login(dataAccess.GetUser(userInput));
        }
        else
        {
            PresentationUtility.DisplayMessage("found", true);
            PresentationUtility.ShowLoadingAnimation();
            MenuHandler();
        }
    }

    #endregion

    #region -- User Menu -- 

    void CreateNewGame()
    {
        session.ActiveSave = new Save();
        dataAccess.StoreSave(session.ActiveSave);
        Console.Clear();
        PresentationUtility.ShowLoadingAnimation();
        MenuHandler(2);
    }

    #endregion

    #region -- Save Menu --

    #endregion 

    #region -- Helpers --

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