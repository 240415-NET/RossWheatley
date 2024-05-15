using TBG.Logic;

namespace TBG.Presentation;

public class Menu
{
    // String to hold user input
    string userInput = string.Empty;

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

                PresentationUtility.MenuHeader(menuId);

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
            if (ValidMenuSelection(userSelection, PresentationUtility.MenuArrays(menuId)))
            {
                repeat = false;
            }
            else
            {
                PresentationUtility.DisplayMessage("invalid", true);
                repeat = true;
            }

        } while (repeat);

        MenuSelector(menuId, userSelection);
    }

    void MenuSelector(int menuId, int selection)
    {
        // Menus abstracted out for code organization
        Menu_User userMenu = new();
        Menu_Main mainMenu = new();
        Menu_Game gameMenu = new();

        switch (menuId)
        {
            case 0: // Main Menu
                switch (selection)
                {
                    case 1:
                        mainMenu.CreateNewUser(this);
                        break;
                    case 2:
                        mainMenu.FindExistingUser(this);
                        break;
                    case 3:
                        ExitApplication();
                        break;
                    default:
                        PresentationUtility.DisplayMessage("invalid", true);
                        MenuHandler();
                        break;
                }
                break;
            case 1: // User Menu
                switch (selection)
                {
                    case 1:
                        userMenu.ContinueSave(this);
                        break;
                    case 2:
                        userMenu.CreateNewGame(this);
                        break;
                    case 3: // Return to main
                        MenuHandler(); // Returns to main menu
                        break;
                    default:
                        PresentationUtility.DisplayMessage("invalid", true);
                        break;
                }
                break;
            case 2: // In-Game Menu
                switch (selection)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        gameMenu.CharacterDisplay(this);
                        break;
                    case 6:
                        gameMenu.ChangeClass(this);
                        break;
                    case 7:
                        SaveHandler.AutoPersistActiveSave();
                        MenuHandler(1); // Go back
                        break;
                    case 8:
                        SaveHandler.AutoPersistActiveSave();
                        MenuHandler(); // main menu
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

    #region -- Helpers --

    public void ExitApplication()
    {
        SaveHandler.AutoPersistActiveSave();
        Environment.Exit(0);
    }

    public string IntToLetters(int value)
    {
        string result = string.Empty;
        while (--value >= 0)
        {
            result = (char)('A' + value % 26) + result;
            value /= 26;
        }
        return result;
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