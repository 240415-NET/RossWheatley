using TBG.Data;

namespace TBG.Presentation;

public class Menu
{
    Session session = new();
    IDataAccess dataAccess = new JSONFileData();
    Menu_User userMenu = new();
    Menu_Main mainMenu = new();
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
                        mainMenu.CreateNewUser(this, session, dataAccess);
                        break;
                    case 2:
                        mainMenu.FindExistingUser(this, session, dataAccess);
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
                        userMenu.ContinueSave(this, session, dataAccess);
                        break;
                    case 2:
                        userMenu.CreateNewGame(this, session, dataAccess);
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
                        AutoPersistActiveSave();
                        MenuHandler(1); // Go back
                        break;
                    case 5:
                        AutoPersistActiveSave();
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

    public void AutoPersistActiveSave()
    {
        if (session.ActiveSave != null)
        {
            session.ActiveSave.SaveDate = DateTime.Now;
            dataAccess.PersistSave(session.ActiveSave);
        }
    }

    public void ExitApplication()
    {
        AutoPersistActiveSave();
        Environment.Exit(0);
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