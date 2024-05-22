using TBG.Logic;

namespace TBG.Presentation;

public class Menu
{
    // String to hold user input
    string userInput = string.Empty;

    public void Builder(int menuId = 0)
    {
        bool repeat;
        int userSelection = 0;
        do
        {
            do
            {
                repeat = true;
                if (menuId != 3 && menuId != 4)
                {
                    Console.Clear();
                }

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

        MenuSelector.Go(this, menuId, userSelection);
    }

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