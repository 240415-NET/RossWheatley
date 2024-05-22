using TBG.Logic;

namespace TBG.Presentation;

/*
1 - Move
2 - End Turn -- Done
3 - Search -- Done
4 - Attempt task -- Done
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

    public static void Move(Menu menu, bool check = true, int input = 0)
    {
        Console.Clear();
        if (check)
        {
            if (SaveHandler.PlayerHasUnits())
            {
                menu.Builder(5);
            }
            else
            {
                Console.Clear();
                PresentationUtility.DisplayMessage("notenough");
                menu.Builder(2);
            }
        }
        else
        {
            // handle movement call
            bool? playerMoved = GameHandler.Move(input);
            if (playerMoved != null && (bool)playerMoved) // Handle encounter
            {
                PresentationUtility.ShowLoadingAnimation();
                Console.Clear();
                PresentationUtility.DisplayMessage("enemy");
                Console.Clear();
                PresentationUtility.ShowLoadingAnimation();
                if (GameHandler.Encounter()) // true if player wins
                {
                    Console.Clear();
                    Console.WriteLine($"Player defeated level {Session.LastEncounter.EnemyLevel} enemy while taking {Session.LastEncounter.PlayerDamage} total damage.");
                    PresentationUtility.DisplayMessage("waitonly");
                    menu.Builder(2);
                }
                else // false if player died
                {
                    Console.Clear();
                    Console.WriteLine($"Player was defeated by level {Session.LastEncounter.EnemyLevel} enemy.");
                    PresentationUtility.DisplayMessage("gameover");
                    // Trigger game over logic
                    menu.Builder(1);
                }
            }
            else if (playerMoved != null)// No encounter
            {
                Console.Clear();
                PresentationUtility.ShowLoadingAnimation(1, 100);
                menu.Builder(2);
            }
            else
            {
                PresentationUtility.DisplayMessage("nomove");
                menu.Builder(2);
            }
        }
    }

    #endregion

    #region -- 2 --
    public static void EndTurn(Menu menu)
    {
        Console.Clear();
        PresentationUtility.ShowLoadingAnimation(1, 100);
        bool progressGame = GameHandler.EndTurn();
        if (progressGame)
        {
            menu.Builder(2);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Game over.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
            menu.Builder(1);
        }
    }
    #endregion

    #region -- 3 --
    public static void Search(Menu menu)
    {
        Console.Clear();
        PresentationUtility.ShowLoadingAnimation(1, 100);

        if (ItemHandler.ItemIdSearch() != null)
        {
            Console.Clear();
            // get and display item details
            Item item = ItemHandler.GetSearchItem();
            Console.WriteLine("Item found!");
            Console.WriteLine($"Skill: {item.SkillIndex + 1}");
            Console.WriteLine($"Modifier: {item.Modifier}");

            // prompt user to equip or go back
            menu.Builder(3);

        }
        else
        {
            Console.Clear();
            PresentationUtility.DisplayMessage("noitem");
            menu.Builder(2);
        }
    }
    #endregion

    #region -- 4 --
    public static void SearchForTask(Menu menu)
    {
        Console.Clear();
        PresentationUtility.ShowLoadingAnimation();

        if (TaskHandler.TaskIdSearch() != null)
        {
            Console.Clear();
            // get and display item details
            Task task = TaskHandler.GetTask();

            Console.WriteLine("Task available!");
            Console.WriteLine($"Unit cost: {task.UnitCost}");

            // prompt user to equip or go back
            menu.Builder(4); // 
        }
        else
        {
            Console.Clear();
            PresentationUtility.DisplayMessage("notask");
            menu.Builder(2);
        }
    }

    public static void AttemptTask(Menu menu)
    {
        Console.Clear();
        PresentationUtility.ShowLoadingAnimation();
        Task task = TaskHandler.GetTask();
        bool? attempt = TaskHandler.AttemptTask();
        int reward = attempt != null && (bool)attempt ? task.Reward : 5;

        if (attempt != null && (bool)attempt)
        {
            //Success
            Console.Clear();
            PresentationUtility.DisplayMessage("success", false);
        }
        else if (attempt != null)
        {
            // Failure
            Console.Clear();
            PresentationUtility.DisplayMessage("fail", false);
        }
        else
        {
            Console.Clear();
            PresentationUtility.DisplayMessage("notenough");
            menu.Builder(2);
        }
        Console.WriteLine($"You've recieved {reward} XP as a reward");
        PresentationUtility.DisplayMessage("waitonly");
        menu.Builder(2);
    }
    #endregion

    #region -- 5 --
    public static void UpdateCharacter(Menu menu)
    {
        character = CharacterHandler.GetActiveCharacter();
        Item item;
        if (character.Item != null)
        {

        }
        bool? success;
        Console.Clear();
        Console.WriteLine($"Character class: {character.CharacterClass}");
        Console.WriteLine($"Level: {character.Level}");
        Console.WriteLine($"Experience: {character.Experience}");
        if (character.Item != null)
        {
            PresentationUtility.CharacterHeader("item");
            Console.WriteLine($"Item Modifies: Skill {character.Item.SkillIndex + 1}");
            Console.WriteLine($"Skill Modified: +{character.Item.Modifier}");
        }
        PresentationUtility.CharacterHeader("skills");
        Console.WriteLine($"Points available: {character.SkillPoints}");

        for (int i = 0; i < character.Skills.Count(); i++)
        {
            int skillValue = character.Skills[i];
            if (character.Item != null && character.Item.SkillIndex == i)
            {
                Console.WriteLine($"Skill {i + 1}: {character.Skills[i]} (+{character.Item.Modifier})");
                // skillValue += character.Item.Modifier;
            }
            else
            {
                Console.WriteLine($"Skill {i + 1}: {character.Skills[i]}");
            }
            // Console.WriteLine($"Skill {i + 1}: {skillValue}");
        }
        PresentationUtility.CharacterHeader("attributes");
        Console.WriteLine($"Points available: {character.AttributePoints}");
        for (int i = 0; i < character.Attributes.Count(); i++)
        {
            Console.WriteLine($"Attribute {menu.IntToLetters(i + 1)}: {character.Attributes[i]}");
        }
        PresentationUtility.CharacterHeader();
        Console.WriteLine("Enter a corresponding letter/number to add available points, or x to go back: ");
        userInput = Console.ReadLine() ?? "";
        if (userInput.Length == 1 && userInput.ToLower() != "x")
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
        else if (userInput.ToLower() != "x")
        {
            PresentationUtility.DisplayMessage("invalid");
        }
        else if (userInput.ToLower() == "x")
        {
            menu.Builder(2);
        }
        MenuSelector.Go(menu, 2, 5);
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
        menu.Builder(2); // Return to save menu
    }
    #endregion
}