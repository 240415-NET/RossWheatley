using TBG.Logic;

namespace TBG.Presentation;

public static class Menu_User
{
    static string userInput = string.Empty;

    public static void CreateNewGame(Menu menu)
    {
        if (SaveHandler.CreateNewGame())
        {
            Console.Clear();
            PresentationUtility.ShowLoadingAnimation();
            menu.Builder(2);
        }
        else
        {
            PresentationUtility.DisplayMessage();
            menu.Builder(1);
        }
    }

    public static void ContinueSave(Menu menu)
    {
        Console.Clear();
        PresentationUtility.ShowLoadingAnimation(); // Pomp and circumstance

        int userSelection = 0;
        bool repeat;

        List<string> saves = SaveHandler.GetUserSavesList();
        if (saves.Count() > 0) // If list is not empty
        {
            do
            {
                repeat = true;
                Console.Clear();
                Console.WriteLine("Please select which save to continue:");

                // Display list of saves
                foreach (string saveString in saves)
                {
                    Console.WriteLine(saveString);
                }

                userInput = Console.ReadLine() ?? "";

                // Input validation
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
                    PresentationUtility.DisplayMessage("invalid");
                }
            } while (repeat);

            // Set the active save and move forward
            Console.Clear();

            if (SaveHandler.LoadSave(userSelection))
            {
                PresentationUtility.ShowLoadingAnimation();
                menu.Builder(2);
            }
            else
            {
                PresentationUtility.DisplayMessage();
                menu.Builder(1);
            }
        }
        else
        {
            Console.Clear();
            PresentationUtility.DisplayMessage("nosave");
            menu.Builder(1);
        }
    }
}