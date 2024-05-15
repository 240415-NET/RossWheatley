using TBG.Presentation;
using TBG.Logic;

public class Program
{
    static void Main(string[] args)
    {
        // Forces the user to close the application using menu options
        // However, disabling because it causes user to press enter twice to input through the console
        // Console.TreatControlCAsInput = true;

        // SessionHandler.StartNewSession();

        Menu menu = new();
        menu.MenuHandler();
    }
}
