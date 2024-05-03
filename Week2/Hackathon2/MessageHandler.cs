namespace Hackathon2;
public static class MessageHandler
{
    static Dictionary<string, string> messages = new Dictionary<string, string>()
        {
            {"added","Selection added."}, // Player Added
            {"default","Something went wrong. Please try again."}, // Default Error Message
            {"delete", "Last selection deleted."}, // Selection deleted
            {"invalid","Invalid input."}, // Invalid Input
            {"noentries", "There are currently no entries."}, // No Entries
            {"pressany","Press any key to go back and try again."} // Press Any Key
        };

    public static void DisplayMessage(string input = "default", bool wait = true)
    {
        if (input != "waitonly") // // Display message to console in all cases except input == waitonly
        {
            Console.WriteLine(messages[input.ToLower()]);
        }

        if (wait) // Await user confirmation to continue when wait is true
        {
            Console.WriteLine(messages["pressany"]);
            Console.ReadKey();
        }
    }

    public static string[] MenuArrays(string menu = "")
    {
        switch (menu.ToLower())
        {
            case "addentry":
                return new string[] { "Add another player.", "Return to main menu." };
            default:
                return new string[] { "View draft", "Add new entry", "Delete last entry", "Save draft", "Exit" };
        }
    }

    public static string? DisplayAddEntryDynamicMenu(int index, string?[] details, Draft draft)
    {
        Console.Clear();
        switch (index)
        {
            case 0:
                return $"Which team is making selection #{draft.NextPick} of round {draft.Round}?";
            case 1:
                return $"Which player does {details[0]} select?";
            case 2:
                return $"What position does {details[1]} play?";
            case 3:
                return $"What college did {details[1]} play for?";
            default:
                MessageHandler.DisplayMessage();
                return null;
        }
    }
}