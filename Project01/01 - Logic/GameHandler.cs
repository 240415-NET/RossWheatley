namespace TBG.Logic;

public static class GameHandler
{
    public static string DirectDanger { get; set; }
    public static string IndirectDanger { get; set; }

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
                Session.LastEncounter = enc;
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

        Session.LastEncounter.EnemyLevel = enemy.Level; // Captures enemy level for presentation purposes

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
                Session.LastEncounter.PlayerDamage += enemyAtt;
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

    public static void GameOver()
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

    static void SetDirectDanger()
    {
        int sum = 0;
        int max = 4;

        double ratio;
        GameObject player = Session.ActiveSave.PlayerObject;
        int coordX = player.Coordinates.X;
        int coordY = player.Coordinates.Y;
        int contX = Session.ActiveSave.GridConstraints.X;
        int contY = Session.ActiveSave.GridConstraints.Y;

        // Check Up
        if (coordY < contY && EcounterMatchAtCoordinates(coordX, coordY + 1))
        {
            sum += 1;
        }
        else if (coordY == contY)
        {
            max -= 1;
        }
        // Check down
        if (coordY > -contY && EcounterMatchAtCoordinates(coordX, coordY - 1))
        {
            sum += 1;
        }
        else if (coordY == -contY)
        {
            max -= 1;
        }
        // Check left
        if (coordX > -contX && EcounterMatchAtCoordinates(coordX - 1, coordY))
        {
            sum += 1;
        }
        else if (coordX == -contX)
        {
            max -= 1;
        }
        // Check right
        if (coordX < contX && EcounterMatchAtCoordinates(coordX + 1, coordY))
        {
            sum += 1;
        }
        else if (coordX == contX)
        {
            max -= 1;
        }

        ratio = sum / max;

        DirectDanger = sum == 0 ? "None" : ratio <= 0.25 ? "Low" : ratio <= 0.50 ? "Medium" : ratio <= 0.75 ? "Moderate" : "High";
    }

    static void SetIndirectDanger()
    {
        // Reset variables at each call
        int sum = 0;
        int max = 4;

        double ratio;
        GameObject player = Session.ActiveSave.PlayerObject;
        int coordX = player.Coordinates.X;
        int coordY = player.Coordinates.Y;
        int contX = Session.ActiveSave.GridConstraints.X;
        int contY = Session.ActiveSave.GridConstraints.Y;

        // Check Up-Left
        if (coordY < contY && coordX > -contX && EcounterMatchAtCoordinates(coordX - 1, coordY + 1))
        {
            sum += 1;
        }
        else if (coordY == contY && coordX == -contX)
        {
            max -= 1;
        }
        // Check Up-Right
        if (coordY < contY && coordX < contX && EcounterMatchAtCoordinates(coordX + 1, coordY + 1))
        {
            sum += 1;
        }
        else if (coordY == contY && coordX == contX)
        {
            max -= 1;
        }
        // Check Down-Left
        if (coordY > -contY && coordX > -contX && EcounterMatchAtCoordinates(coordX - 1, coordY - 1))
        {
            sum += 1;
        }
        else if (coordY == -contY && coordX == -contX)
        {
            max -= 1;
        }
        // Check Down-Right
        if (coordY > -contY && coordX < contX && EcounterMatchAtCoordinates(coordX - 1, coordY + 1))
        {
            sum += 1;
        }
        else if (coordY == -contY && coordX == contX)
        {
            max -= 1;
        }

        ratio = sum / max;

        IndirectDanger = sum == 0 ? "None" : ratio <= 0.25 ? "Low" : ratio <= 0.50 ? "Medium" : ratio <= 0.75 ? "Moderate" : "High";
    }

    public static void SetDangerIndicators()
    {
        SetDirectDanger();
        SetIndirectDanger();
    }

    static bool EcounterMatchAtCoordinates(int x, int y)
    {
        foreach (Encounter encounter in Session.ActiveSave.Encounters)
        {
            if (encounter.Coordinates.X == x && encounter.Coordinates.Y == y)
            {
                return true;
            }
        }
        return false;
    }

    public static bool TaskOrItemAtCoordinates()
    {
        foreach (Task task in Session.ActiveSave.Tasks)
        {
            if (Session.ActiveSave.PlayerObject.GameObjectAtCoordinates(task.Coordinates.X, task.Coordinates.Y))
            {
                return true;
            }
        }

        foreach (Item item in Session.ActiveSave.Items)
        {
            if (Session.ActiveSave.PlayerObject.GameObjectAtCoordinates(item.Coordinates.X, item.Coordinates.Y))
            {
                return true;
            }
        }

        return false;
    }
}