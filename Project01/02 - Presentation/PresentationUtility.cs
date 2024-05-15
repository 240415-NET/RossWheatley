using TBG.Logic;

namespace TBG.Presentation;

public static class PresentationUtility
{
    readonly static int loadingDuration = 2;
    readonly static int sleepTime = 200;
    private static bool displayArt = true;
    static Dictionary<string, string> messages = new Dictionary<string, string>()
        {
            {"added","New user added."},
            {"continue","Press any key to continue."},
            {"default","Something went wrong. Please try again."},
            {"duplicate","That username may already exist. Please try again."},
            {"enough", "You do not have enough points available for that."},
            {"notfound","That user could not be found."},
            {"invalid","Invalid input."},
            {"nosave","No save data found for active user."},
            {"allocated","Points successfully allocated."}
        };

    public static void MenuHeader(int menuId)
    {
        if (menuId == 0)
        {
            MenuArt();
        }
        else if (menuId == 1)
        {
            Console.WriteLine($"Hello, {Session.ActiveUser.UserName}!");
        }
    }

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

    public static string[] MenuArrays(int menu = 0)
    {
        switch (menu)
        {
            case 1:
                return new string[] { "Continue previous game", "Create new game", "Main menu" };
            case 2:
                return new string[] { "Move", "End turn", "Search", "Attempt task", "Update character", "Change class", "Go back", "Main menu" };
            case 3:
                return new string[] { "Move Up", "Move Down", "Move Left", "Move Right" };
            default:
                return new string[] { "Create new user", "Login as existing user", "Exit" };
        }
    }

    public static void ShowLoadingAnimation(int load = 0, int sleep = 0)
    {
        load = load == 0 ? loadingDuration : load;
        sleep = sleep == 0 ? sleepTime : sleep;

        int iterations = loadingDuration * 2;

        for (int i = 0; i < iterations; i++)
        {
            Console.Write(". ");
            Thread.Sleep(sleepTime);
        }
    }

    public static void CharacterHeader(string s = "")
    {
        if (s == "")
        {
            Console.WriteLine("--------------------------------");
        }
        else if (s.ToLower() == "skills")
        {
            Console.WriteLine("-----------S K I L L S----------");
        }
        else if (s.ToLower() == "attributes")
        {
            Console.WriteLine("-------A T T R I B U T E S------");
        }
    }

    public static void MenuArt()
    {
        if (displayArt)
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
            displayArt = false;
        }
        displayArt = false;
    }
}