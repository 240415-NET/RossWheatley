using TBG.Logic;

namespace TBG.Presentation;

public static class Menu_Game
{
    static string userInput = String.Empty;
    static GameObject character = new();

    #region -- Displays --

    public static void ChangeClass(Menu menu)
    {
        Console.Clear();
        character = CharacterHandler.GetActiveCharacter();
        Console.WriteLine($"Character class: {character.CharacterClass}");
        Console.WriteLine("Your character class is completely arbitrary. But feel free to enter a number between 1-3 to change your class anyway:");
        CharacterHandler.UpdateClass(Console.ReadLine() ?? "");
        menu.MenuHandler(2); // Return to save menu
    }

    public static void CharacterDisplay(Menu menu)
    {
        character = CharacterHandler.GetActiveCharacter();
        bool? success;
        Console.Clear();
        Console.WriteLine($"Character class: {character.CharacterClass}");
        Console.WriteLine($"Level: {character.Level}");
        Console.WriteLine($"Experience: {character.Experience}");
        PresentationUtility.CharacterHeader("skills");
        Console.WriteLine($"Points available: {character.SkillPoints}");

        for (int i = 0; i < character.Skills.Count(); i++)
        {
            Console.WriteLine($"Skill {i + 1}: {character.Skills[i]}");
        }
        PresentationUtility.CharacterHeader("attributes");
        Console.WriteLine($"Points available: {character.AttributePoints}");
        for (int i = 0; i < character.Attributes.Count(); i++)
        {
            Console.WriteLine($"Attribute {menu.IntToLetters(i + 1)}: {character.Attributes[i]}");
        }
        PresentationUtility.CharacterHeader();
        Console.WriteLine("Enter a corresponding letter/number to add available points, or q to go back: ");
        userInput = Console.ReadLine() ?? "";
        if (userInput.Length == 1 && userInput.ToLower() != "q")
        {
            success = CharacterHandler.UpdateCharacter(userInput);
            if (success != null && (bool)success)
            {
                PresentationUtility.DisplayMessage("allocated");
            }
            else if (success == null)
            {
                PresentationUtility.DisplayMessage("enough");
            }
            else
            {
                PresentationUtility.DisplayMessage("invalid");
            }
        }
        else if (userInput.ToLower() != "q")
        {
            PresentationUtility.DisplayMessage("invalid");
        }
        menu.MenuHandler(2);
    }
    #endregion
}