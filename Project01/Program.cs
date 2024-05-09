using TBG.Presentation;

class Program
{
    static void Main(string[] args)
    {
        // Forces the user to close the application using menu options
        // However, disabling because it causes user to press enter twice to input through the console
        // Console.TreatControlCAsInput = true;
        Menu menu = new();
        menu.MenuHandler();
    }
}
