using TBG.Data;

namespace TBG.Presentation;

public class Menu
{
    string userInput = "";

    public void StartMenu()
    {
        bool validInput = false;
        do
        {
            Console.Clear();
            PresentationUtility.MenuArt();
            PrintMenuArray(PresentationUtility.MenuArrays());
            try
            {
                validInput = MainMenuSelector(Convert.ToInt32(Console.ReadLine()));
            }
            catch
            {
                validInput = false;
                PresentationUtility.DisplayMessage("invalid", true);
            }
        } while (!validInput);
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

    void CreateNewUser()
    {
        JSONFileData jsonFileData = new();
        User newUser = new();
        do
        {

            Console.WriteLine("Please enter a user name: ");
            newUser = new(Console.ReadLine() ?? "");
        } while (!jsonFileData.StoreUser(newUser));
    }

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
                PresentationUtility.DisplayMessage("invalid", true);
                return false;
        }
    }
}