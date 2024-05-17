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
            return false;
        }
    }

    public static bool? Move(int input)
    {
        switch (input)
        {
            case 1: // Move Up
                if (Session.ActiveSave.PlayerObject.Coordinates.Y < Session.ActiveSave.GridConstraints.Y)
                {
                    Session.ActiveSave.PlayerObject.Coordinates.Y += 1;
                }
                else
                {
                    return null;
                }
                break;
            case 2: // Move Down
                if (Session.ActiveSave.PlayerObject.Coordinates.Y > -Session.ActiveSave.GridConstraints.Y)
                {
                    Session.ActiveSave.PlayerObject.Coordinates.Y -= 1;
                }
                else
                {
                    return null;
                }
                break;
            case 3: // Move Left
                if (Session.ActiveSave.PlayerObject.Coordinates.X > -Session.ActiveSave.GridConstraints.X)
                {
                    Session.ActiveSave.PlayerObject.Coordinates.X -= 1;
                }
                else
                {
                    return null;
                }
                break;
            case 4: // Move Right
                if (Session.ActiveSave.PlayerObject.Coordinates.X < -Session.ActiveSave.GridConstraints.X)
                {
                    Session.ActiveSave.PlayerObject.Coordinates.X += 1;
                }
                else
                {
                    return null;
                }
                break;
            default:
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

    public static bool Encounter()
    {
        // return true if player wins encounter
        // return false if player dies (game over)
        return false;
    }
}