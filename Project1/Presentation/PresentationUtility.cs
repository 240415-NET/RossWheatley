namespace TBG.Presentation;

public static class PresentationUtility
{
    readonly static int loadingDuration = 4;
    readonly static int sleepTime = 200;
    static Dictionary<string, string> messages = new Dictionary<string, string>()
        {
            {"added","New user added."},
            {"continue","Press any key to continue."},
            {"default","Something went wrong. Please try again."},
            {"duplicate","That username may already exist. Please try again."},
            {"invalid","Invalid input."},
        };

    public static void DisplayMessage(string input = "default", bool wait = true)
    {
        if (input != "waitonly") // // Display message to console in all cases except input == waitonly
        {
            Console.WriteLine(messages[input.ToLower()]);
        }

        if (wait) // Await user confirmation to continue when wait is true
        {
            Console.WriteLine(messages["continue"]);
            Console.ReadKey();
        }
    }

    public static string[] MenuArrays(string menu = "")
    {
        switch (menu.ToLower())
        {
            case "turn":
                return new string[] { "End turn", "Update attributes", "Attempt task", "Return to main menu" };
            default:
                return new string[] { "Create new user", "Login as existing user", "Exit" };
        }
    }

    public static void ShowLoadingAnimation()
    {
        int iterations = loadingDuration * 2;

        for (int i = 0; i < iterations; i++)
        {
            Console.Write(". ");
            Thread.Sleep(sleepTime);
        }
    }

    public static void MenuArt()
    {
        string[] lines = new string[]
        {
        "**********************************************",
        "                                    ",
        " GGGGG    AAAAA   MMM     MMM  EEEEE",
        "GG   GG  AA   AA  MM M   M MM  EE   ",
        "GG       AA   AA  MM  M M  MM  EE   ",
        "GG  GGGG AAAAAAA  MM   M   MM  EEEE ",
        "GG    GG AA   AA  MM       MM  EE   ",
        " GGGGGG  AA   AA  MM       MM  EEEEE",
        };
        for (int i = 0; i <= lines.Length; i++)
        {
            if (i == 0 || i == lines.Length)
            {
                Border();
            }
            else if (i + 1 == lines.Length)
            {
                Console.WriteLine($"***  {lines[i]}  ***");
                Console.WriteLine($"***  {lines[1]}  ***");
            }
            else
            {
                Console.WriteLine($"***  {lines[i]}  ***");
            }
        }

        void Border() // nested method
        {
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine(lines[0]);
            }
        }
    }
}