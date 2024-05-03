namespace Hackathon2;

public class Menu
{
    Draft draft = new();
    string? userInput;

    public void MainMenu()
    {
        do
        {
            PrintMenuOptions(MessageHandler.MenuArrays(), true);
            userInput = Console.ReadLine();
        }
        while (!ValidMenuInput(userInput, MessageHandler.MenuArrays())); // Loop if input not valid

        NavigateMainMenu(Convert.ToInt16(userInput)); // Go to menu select

    }
    void PrintMenuOptions(string[] options, bool clear = false)
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

    public void NavigateMainMenu(int input)
    {
        switch (input)
        {
            case 1:
                DisplayEntries();
                break;
            case 2:
                AddEntry();
                break;
            case 3:
                DeleteLast();
                break;
            case 4:
                // Save
                MessageHandler.DisplayMessage();
                break;
            default:
                Environment.Exit(0);
                break;
        }
    }

    bool ValidMenuInput(string? input, string[] options)
    {
        try
        {
            if (Convert.ToInt16(input) > 0 && Convert.ToInt16(input) <= options.Count())
            {
                return true;
            }
            else
            {
                MessageHandler.DisplayMessage("invalid");
                return false;
            }
        }
        catch (Exception e)
        {
            MessageHandler.DisplayMessage("invalid");
            return false;
        }
    }

    void AddEntry()
    {
        Console.Clear();
        draft.Counters();

        string?[] details = new string[4];

        for (int i = 0; i < details.Count(); i++)
        {
            Console.WriteLine(MessageHandler.DisplayAddEntryDynamicMenu(i, details, draft));
            details[i] = Console.ReadLine();
        }

        draft.Selections.Add(new DraftChoice(draft.Round, draft.NextPick, details[0], details[1], details[2], details[3]));
        Console.Clear();
        MessageHandler.DisplayMessage("added", false);

        do
        {
            PrintMenuOptions(MessageHandler.MenuArrays("addentry"));
            userInput = Console.ReadLine();
            Console.Clear();
        }
        while (!ValidMenuInput(userInput, MessageHandler.MenuArrays("addentry")));

        if (Convert.ToInt16(userInput) == 1)
        {
            AddEntry(); // Repeat Add Entry
        }
        else
        {
            MainMenu(); // Return to Main Menu
        }
    }

    // Iterates and writes all entries to the console
    void DisplayEntries()
    {
        Console.Clear();
        if (draft.Selections.Count() > 0)
        {
            foreach (DraftChoice choice in draft.Selections)
            {
                Console.WriteLine(choice);
            }
            MessageHandler.DisplayMessage("waitonly"); // Prompt user before clearing console and returning to main
        }
        else
        {
            MessageHandler.DisplayMessage("noentries");
        }
        MainMenu();
    }

    void DeleteLast()
    {
        if (draft.Selections.Count > 0)
        {
            Console.Clear();
            draft.Selections.RemoveAt(draft.Selections.Count - 1);
            draft.Counters(false); // Decrement next pick/round
            MessageHandler.DisplayMessage("delete");
        }
        else
        {
            MessageHandler.DisplayMessage("noentries");
        }
        MainMenu();
    }
}