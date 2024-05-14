namespace TBG.Presentation;

public class Menu_User
{
    string userInput = "";

    public void CreateNewGame(Menu menu, Session session, IDataAccess dataAccess)
    {
        session.ActiveSave = new Save(session.ActiveUser, new GameObject(true));
        dataAccess.PersistSave(session.ActiveSave);
        Console.Clear();
        PresentationUtility.ShowLoadingAnimation();
        menu.MenuHandler(2);
    }

    public void ContinueSave(Menu menu, Session session, IDataAccess dataAccess)
    {
        Console.Clear();
        PresentationUtility.ShowLoadingAnimation(); // Pomp and circumstance

        int userSelection = 0;
        bool repeat;

        List<Save> saves = dataAccess.GetUserSavesList(session.ActiveUser);
        if (saves.Count() > 0)
        {
            do
            {
                repeat = true;
                Console.Clear();
                Console.WriteLine("Please select which save to continue:");
                for (int i = 0; i < saves.Count(); i++)
                {
                    Console.WriteLine($"{i + 1}. Last saved: {saves[i].SaveDate/*.ToShortDateString()*/}");
                }
                userInput = Console.ReadLine() ?? "";
                try
                {
                    userSelection = Convert.ToInt32(userInput);
                    if (userSelection > saves.Count())
                    {
                        repeat = true;
                    }
                    else
                    {
                        repeat = false;
                    }
                }
                catch
                {
                    repeat = true; // If user input cannot be converted to int
                }
                if (repeat)
                {
                    PresentationUtility.DisplayMessage("invalid", true);
                }
            } while (repeat);

            // Set the active save and move forward
            Console.Clear();
            session.ActiveSave = saves[userSelection - 1];
            PresentationUtility.ShowLoadingAnimation();
            menu.MenuHandler(2);
        }
        else
        {
            Console.Clear();
            PresentationUtility.DisplayMessage("nosave", true);
            menu.MenuHandler(1);
        }
    }
}