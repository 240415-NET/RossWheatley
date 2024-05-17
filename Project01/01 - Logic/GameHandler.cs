namespace TBG.Logic;

public static class GameHandler
{
    public static bool EndTurn()
    {
        if (Session.ActiveSave.Turns > 1)
        {
            Session.ActiveSave.Turns -= 1;
            Session.ActiveSave.Units += 2;
            return true;
        }
        else
        {
            GameOver();
            return false;
        }
    }

    public static bool? Move(int input)
    {
        if (!CharacterHandler.MoveCharacter(input)) // Player cannot move any further that direction
        {
            return null;
        }

        Session.ActiveSave.Units -= 1;

        bool encounter = false;

        foreach (Encounter enc in Session.ActiveSave.Encounters)
        {
            if (Session.ActiveSave.PlayerObject.GameObjectAtCoordinates(enc.Coordinates.X, enc.Coordinates.Y))
            {
                encounter = true;
            }
        }

        if (encounter) // moved into encounter
        {
            return true;
        }
        else // no encounter
        {
            return false;
        }
    }

    public static bool Encounter() // return true if player wins, false is player loses (also, handle gameover)
    {
        Random random = new();
        GameObject player = Session.ActiveSave.PlayerObject;
        GameObject enemy = new(false, Session.ActiveSave.PlayerObject.Level);

        DeleteEncounter();

        int attributeIndex = random.Next(1, 3);
        int junkAttribute = attributeIndex == 1 ? 2 : 1;

        if (player.Item != null)
        {
            // item bonus
            player.Skills[player.Item.SkillIndex] += player.Item.Modifier;
        }

        int playerAtt = player.Skills[0] + player.Attributes[attributeIndex];
        int playerHP = player.Skills[1] * player.Attributes[0];

        int enemyAtt = enemy.Skills[0] + enemy.Attributes[attributeIndex];
        int enemyHP = enemy.Skills[1] * enemy.Attributes[0];

        int reward = (enemyHP + 5) * player.Attributes[junkAttribute];

        do
        {
            if (enemyHP - playerAtt > 0)
            {
                enemyHP -= playerAtt; // decrement enemy life
            }
            else
            {
                CharacterHandler.RewardPlayer(reward);
                return true; // player won
            }

            if (playerHP - enemyAtt > 0)
            {
                playerHP -= enemyAtt; // decrement player life
            }
            else
            {
                GameOver();
                return false; // player has lost
            }
        } while (playerHP > 0 && enemyHP > 0);
        GameOver();
        return false;
    }

    static void GameOver()
    {
        SaveHandler.DeleteActiveSave();
    }

    public static void DeleteEncounter()
    {
        DeleteEncounter((Guid)EncounterIdSearch());
    }

    public static void DeleteEncounter(Guid encounterId)
    {
        Session.ActiveSave.Encounters.RemoveAll(enc => enc.EncounterId == encounterId);
    }

    public static Guid? EncounterIdSearch()
    {
        foreach (Encounter enc in Session.ActiveSave.Encounters)
        {
            if (Session.ActiveSave.PlayerObject.GameObjectAtCoordinates(enc.Coordinates.X, enc.Coordinates.Y))
            {
                return enc.EncounterId;
            }
        }
        return null;
    }
}