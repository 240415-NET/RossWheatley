namespace TBG.Logic;

public static class Logic_Character
{
    public static bool? UpdateCharacter(Session session, string s) // True = Success; False = Invalid, Null = No points to allocate
    {
        int input;
        if (s.Length == 1 && s != "") // input is appropriate length (single digit or character) and not the escape character
        {
            if (int.TryParse(s, out input)) // input is single integer
            {
                if (input > 0 && input <= session.ActiveSave.PlayerObject.Skills.Count()) // input corresponds to a valid skill
                {
                    if (session.ActiveSave.PlayerObject.SkillPoints > 0) // Skill points are available
                    {
                        session.ActiveSave.PlayerObject.Skills[input - 1] += 1;
                        session.ActiveSave.PlayerObject.SkillPoints -= 1;
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
                if (input > 0 && input <= session.ActiveSave.PlayerObject.Attributes.Count()) // input corresponds to a valid attribute
                {
                    if (session.ActiveSave.PlayerObject.AttributePoints > 0) // Attribute points are available
                    {
                        session.ActiveSave.PlayerObject.Attributes[input - 1] += 1;
                        session.ActiveSave.PlayerObject.AttributePoints -= 1;
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

    public static void UpdateClass(Session session, string s)
    {
        try
        {
            session.ActiveSave.PlayerObject.CharacterClass = Convert.ToInt32(s);
        }
        catch
        {
            // Doesn't matter
        }
    }
}