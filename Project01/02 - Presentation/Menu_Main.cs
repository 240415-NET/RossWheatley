namespace TBG.Presentation;

public class Menu_Main
{
    #region -- Main Menu --

    public void CreateNewUser(Menu menu, Session session, IDataAccess dataAccess)
    {
        bool repeat = false;
        string userInput;
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
        Login(menu, session, newUser);
    }

    void Login(Menu menu, Session session, User user)
    {
        session.ActiveUser = user;
        Console.Clear();
        PresentationUtility.ShowLoadingAnimation();
        menu.MenuHandler(1);
    }

    public void FindExistingUser(Menu menu,Session session, IDataAccess dataAccess)
    {
        Console.Clear();
        Console.WriteLine("Enter your username:");
        string userInput = Console.ReadLine() ?? "";

        if (dataAccess.UserExists(userInput))
        {
            Login(menu, session, dataAccess.GetUser(userInput));
        }
        else
        {
            PresentationUtility.DisplayMessage("found", true);
            menu.MenuHandler();
        }
    }

    #endregion
}