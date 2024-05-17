namespace TBG.Logic;

public static class CharacterHandler
{
    public static bool? UpdateCharacter(string s) // True = Success; False = Invalid, Null = No points to allocate
    {
        int input;
        if (s.Length == 1 && s != "") // input is appropriate length (single digit or character) and not the escape character
        {
            if (int.TryParse(s, out input)) // input is single integer
            {
                if (input > 0 && input <= Session.ActiveSave.PlayerObject.Skills.Count()) // input corresponds to a valid skill
                {
                    if (Session.ActiveSave.PlayerObject.SkillPoints > 0) // Skill points are available
                    {
                        Session.ActiveSave.PlayerObject.Skills[input - 1] += 1;
                        Session.ActiveSave.PlayerObject.SkillPoints -= 1;
                        return true;
                    }
                    else
                    {
                        return null;
                    }
                }
                else // invalid number
                {
                    return false;
                }
            }
            else
            {
                // input is not a digit
                input = Char.ToLower(s[0]) - 'a' + 1;
                if (input > 0 && input <= Session.ActiveSave.PlayerObject.Attributes.Count()) // input corresponds to a valid attribute
                {
                    if (Session.ActiveSave.PlayerObject.AttributePoints > 0) // Attribute points are available
                    {
                        Session.ActiveSave.PlayerObject.Attributes[input - 1] += 1;
                        Session.ActiveSave.PlayerObject.AttributePoints -= 1;
                        return true;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    public static void RewardPlayer(int reward)
    {
        int level = (Session.ActiveSave.PlayerObject.Experience + reward) / 10;
        if (level > 0)
        {
            // add level(s)
            Session.ActiveSave.PlayerObject.Level += level;
            Session.ActiveSave.PlayerObject.SkillPoints += level * 2;
            Session.ActiveSave.PlayerObject.AttributePoints += level * 2;
        }
        Session.ActiveSave.PlayerObject.Experience += reward - (level * 10);
    }

    public static void UpdateClass(string s)
    {
        try
        {
            Session.ActiveSave.PlayerObject.CharacterClass = Convert.ToInt32(s);
        }
        catch
        {
            // Doesn't matter
        }
    }

    public static GameObject GetActiveCharacter()
    {
        return Session.ActiveSave.PlayerObject;
    }

    public static void EquipItem()
    {
        Session.ActiveSave.PlayerObject.Item = ItemHandler.GetSearchItem(false);
        ItemHandler.DeleteItem();
    }

    public static bool MoveCharacter(int input)
    {
        switch (input)
        {
            case 1: // Move Up
                if (Session.ActiveSave.PlayerObject.Coordinates.Y < Session.ActiveSave.GridConstraints.Y)
                {
                    Session.ActiveSave.PlayerObject.Coordinates.Y += 1;
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            case 2: // Move Down
                if (Session.ActiveSave.PlayerObject.Coordinates.Y > -Session.ActiveSave.GridConstraints.Y)
                {
                    Session.ActiveSave.PlayerObject.Coordinates.Y -= 1;
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            case 3: // Move Left
                if (Session.ActiveSave.PlayerObject.Coordinates.X > -Session.ActiveSave.GridConstraints.X)
                {
                    Session.ActiveSave.PlayerObject.Coordinates.X -= 1;
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            case 4: // Move Right
                if (Session.ActiveSave.PlayerObject.Coordinates.X < Session.ActiveSave.GridConstraints.X)
                {
                    Session.ActiveSave.PlayerObject.Coordinates.X += 1;
                    return true;
                }
                else
                {
                    return false;
                }
                break;
            default:
                return false;
        }
    }
}