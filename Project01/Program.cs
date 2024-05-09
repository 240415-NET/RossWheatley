using TBG.Presentation;

class Program
{
    static void Main(string[] args)
    {
        Console.TreatControlCAsInput = true;
        Menu menu = new();
        menu.MenuHandler();
    }
}
