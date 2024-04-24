namespace Hackathon2;

public class Menu
{
    // Declares list to hold all draft picks
    Draft draft = new();
    // Declares a starting value for draft pick and round
    int round = 1;
    int pick = 0;
    string? userInput;

    public void MainMenu()
    {
        // Array of main menu options
        string[] mainMenuOptions = { "View existing entries.", "Add new entry.", "Delete an entry.", "Exit" };
        do
        {
            Console.Clear();
            Console.WriteLine("Please make a selection: \n");
            PrintMenuOptions(mainMenuOptions);
            userInput = Console.ReadLine();
        }
        while (!MenuInputValid(userInput, mainMenuOptions));

        NavigateMainMenu(Convert.ToInt16(userInput));

    }

    void PrintMenuOptions(string[] options)
    {
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
                // Navigate to view list
                DisplayEntries();
                break;
            case 2:
                // Navigate to Add
                AddEntry();
                break;
            case 3:
                // Navigate to Delete
                break;
            default:
                // Exit
                Environment.Exit(0);
                break;
        }
    }

    bool MenuInputValid(string input, string[] options)
    {
        try
        {
            if (Convert.ToInt16(input) > 0 && Convert.ToInt16(input) <= options.Count())
            {
                return true;
            }
            else
            {
                DefaultMessages.DisplayInvalidInputMessage();
                return false;
            }
        }
        catch (Exception e)
        {
            DefaultMessages.DisplayInvalidInputMessage();
            return false;
        }
    }

    string AddEntryDynamicMenu(int index, string[] details)
    {
        switch (index)
        {
            case 0:
                return $"Which team is making selection #{pick} of round {round}?";
            case 1:
                return $"Which player does {details[index]} select?";
            case 2:
                return $"What position does {details[index]} play?";
            case 3:
                return $"What college did {details[index]} play for?";
            default:
                MessageHandler.DisplayMessage();
                return null;
        }
    }

    void AddEntry()
    {
        IterateCounters();

        string[] details = new string[4];
        string[] addOptions = { "1. Add another player.", "2. Return to main menu." };

        for (int i = 0; i < details.Count(); i++)
        {
            Console.Clear();
            AddEntryDynamicMenu(i, details);
            details[i] = Console.ReadLine();
        }

        DraftChoice choice = new(round, pick, details[0], details[1], details[2], details[3]);
        draft.Add(choice);
        do
        {
            Console.Clear();
            Console.WriteLine("Player added.");
            PrintMenuOptions(addOptions);
            userInput = Console.ReadLine();
        }
        while (!MenuInputValid(userInput, addOptions));

        if (Convert.ToInt16(userInput) == 1)
        {
            AddEntry();
        }
        else
        {
            MainMenu();
        }
    }

    void DisplayEntries()
    {
        Console.Clear();
        if (draft.Count() > 0)
        {
            foreach (DraftChoice choice in draft)
            {
                Console.WriteLine(choice);
            }
        }
        else
        {
            Console.WriteLine("There are currently no entries.");
        }
        Console.WriteLine("(Press any key to return to main menu.)");
        Console.ReadKey();
        MainMenu();
    }

    void IterateCounters()
    {
        if (pick > 32)
        {
            round++;
            pick = 1;
        }
        else
        {
            pick++;
        }
    }
}