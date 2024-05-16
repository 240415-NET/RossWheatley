using TBG.Logic;

namespace TBG.Presentation;

/*
1 - Move
2 - End Turn
3 - Search
4 - Attempt task
5 - Update Character -- Done
6 - Change class -- Done
7 - Go back -- Done
8 - Main menu -- Done
*/

public static class Menu_Game
{
    static string userInput = String.Empty;
    static GameObject character = new();

    #region -- 1 --
    #endregion

    #region -- 2 --
    #endregion

    #region -- 3 --
    public static void Search(Menu menu)
    {
        Console.Clear();
        PresentationUtility.ShowLoadingAnimation();
        if (ItemHandler.Search())
        {
            Console.Clear();
            // get and display item details
            // prompt user to equip or go back
        }
        else
        {
            Console.Clear();
            PresentationUtility.DisplayMessage("noitem");
            menu.MenuHandler(2);
        }
    }
    #endregion

    #region -- 4 --
    #endregion

    #region -- 5 --
    public static void UpdateCharacter(Menu menu)
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

    #region -- 6 -- 
    public static void ChangeClass(Menu menu)
    {
        Console.Clear();
        character = CharacterHandler.GetActiveCharacter();
        Console.WriteLine($"Character class: {character.CharacterClass}");
        Console.WriteLine("Your character class is completely arbitrary. But feel free to enter a number between 1-3 to change your class anyway:");
        CharacterHandler.UpdateClass(Console.ReadLine() ?? "");
        menu.MenuHandler(2); // Return to save menu
    }
    #endregion
}