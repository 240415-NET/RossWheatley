using TBG.Presentation;

public class Program
{
    static void Main(string[] args)
    {
        // Forces the user to close the application using menu options
        // However, disabling because it causes user to press enter twice to input through the console
        // Console.TreatControlCAsInput = true;
        Menu menu = new();
        menu.MenuHandler();
    }

    public static bool IsPrime(int x)
    {
        if (x <= 1)
            return false;

        if (x == 2)
            return true;

        if (x % 2 == 0)
            return false;

        var boundary = (int)Math.Floor(Math.Sqrt(x));

        for (int i = 3; i <= boundary; i += 2)
        {
            if (x % i == 0)
                return false;
        }

        return true;
    }
}
