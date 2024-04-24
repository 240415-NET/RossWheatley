namespace Hackathon2;
public static class MessageHandler
{
    private static string DefaultErrorMessage = "Something went wrong. Please try again.";
    private static string InvalidInputMessage = "Invalid input.";

    public static void DisplayMessage(string message = "")
    {
        switch (message.ToLower())
        {
            case "invalid":
                Console.WriteLine(InvalidInputMessage);
                break;
            default:
                Console.WriteLine(DefaultErrorMessage);
                break;
        }
        Console.Write(" Press any key to go back and try again.");
        Console.ReadKey();
    }
}