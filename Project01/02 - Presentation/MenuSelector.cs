using TBG.Logic;

namespace TBG.Presentation;

public static class MenuSelector
{
    public static void Go(Menu menu, int menuId, int selection)
    {
        // Menus abstracted out for code organization
        Menu_Main mainMenu = new();

        switch (menuId)
        {
            case 0: // Main Menu
                switch (selection)
                {
                    case 1:
                        mainMenu.CreateNewUser(menu);
                        break;
                    case 2:
                        mainMenu.FindExistingUser(menu);
                        break;
                    case 3:
                        menu.ExitApplication();
                        break;
                    default:
                        PresentationUtility.DisplayMessage("invalid", true);
                        menu.Builder();
                        break;
                }
                break;
            case 1: // User Menu
                switch (selection)
                {
                    case 1:
                        Menu_User.ContinueSave(menu);
                        break;
                    case 2:
                        Menu_User.CreateNewGame(menu);
                        break;
                    case 3: // Return to main
                        menu.Builder(); // Returns to main menu
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
                        Menu_Game.Move(menu);
                        break;
                    case 2:
                        Menu_Game.EndTurn(menu);
                        break;
                    case 3:
                        Menu_Game.Search(menu);
                        break;
                    case 4:
                        Menu_Game.SearchForTask(menu);
                        break;
                    case 5:
                        Menu_Game.UpdateCharacter(menu);
                        break;
                    case 6:
                        Menu_Game.ChangeClass(menu);
                        break;
                    case 7:
                        SaveHandler.AutoPersistActiveSave();
                        menu.Builder(1); // Go back
                        break;
                    case 8:
                        SaveHandler.AutoPersistActiveSave();
                        menu.Builder(); // main menu
                        break;
                    default:
                        PresentationUtility.DisplayMessage("invalid", true);
                        break;
                }
                break;
            case 3:
                switch (selection)
                {
                    case 1:
                        CharacterHandler.EquipItem();
                        menu.Builder(2);
                        break;
                    default:
                        ItemHandler.DeleteItem();
                        menu.Builder(2);
                        break;
                }
                break;
            case 4:
                switch (selection)
                {
                    case 1:
                        Menu_Game.AttemptTask(menu);
                        break;
                    default:
                        menu.Builder(2);
                        break;
                }
                break;
            case 5:
                Menu_Game.Move(menu, false, selection);
                break;
            default:
                PresentationUtility.DisplayMessage();
                menu.Builder();
                break;
        }
    }
}