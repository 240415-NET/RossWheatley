using TBG.Logic;

namespace TBG.Presentation;

public class Menu_Save
{
    string userInput = String.Empty;

    #region -- Displays --

    public void ChangeClass(Menu menu, Session session)
    {
        Console.Clear();
        Console.WriteLine($"Character class: {session.ActiveSave.PlayerObject.CharacterClass}");
        Console.WriteLine("Your character class is completely arbitrary. But feel free to enter a number between 1-3 to change your class anyway:");
        userInput = Console.ReadLine() ?? "";
        Logic_Character.UpdateClass(session, userInput);
        menu.MenuHandler(2); // Return to save menu
    }

    public void CharacterDisplay(Menu menu, Session session)
    {
        bool? success;
        Console.Clear();
        Console.WriteLine($"Character class: {session.ActiveSave.PlayerObject.CharacterClass}");
        Console.WriteLine($"Level: {session.ActiveSave.PlayerObject.Level}");
        Console.WriteLine($"Experience: {session.ActiveSave.PlayerObject.Experience}");
        PresentationUtility.CharacterHeader("skills");
        Console.WriteLine($"Points available: {session.ActiveSave.PlayerObject.SkillPoints}");

        for (int i = 0; i < session.ActiveSave.PlayerObject.Skills.Count(); i++)
        {
            Console.WriteLine($"Skill {i + 1}: {session.ActiveSave.PlayerObject.Skills[i]}");
        }
        PresentationUtility.CharacterHeader("attributes");
        Console.WriteLine($"Points available: {session.ActiveSave.PlayerObject.AttributePoints}");
        for (int i = 0; i < session.ActiveSave.PlayerObject.Attributes.Count(); i++)
        {
            Console.WriteLine($"Attribute {menu.IntToLetters(i + 1)}: {session.ActiveSave.PlayerObject.Attributes[i]}");
        }
        PresentationUtility.CharacterHeader();
        Console.WriteLine("Enter a corresponding letter/number to add available points, or q to go back: ");
        userInput = Console.ReadLine() ?? "";
        if (userInput.Length == 1 && userInput.ToLower() != "q")
        {
            success = Logic_Character.UpdateCharacter(session, userInput);
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