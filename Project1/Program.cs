using TBG.Presentation;

namespace TBG;
class Program
{
    static void Main(string[] args)
    {
        Console.TreatControlCAsInput = true;
        new Menu().StartMenu();
    }
}
