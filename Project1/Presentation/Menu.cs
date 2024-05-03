namespace TBG.Presentation;

public class Menu
{
    string userInput = "";

    public void StartMenu()
    {
        PresentationUtility.MenuArt();
        PrintMenuArray(PresentationUtility.MenuArrays());
        userInput = Console.ReadLine() ?? "";
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
}