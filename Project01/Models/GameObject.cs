namespace TBG;

public class GameObject
{
    public Guid SaveId { get; set; }
    public bool IsPlayer { get; set; }
    private int characterClass;
    public int Level { get; set; }
    public int Experience { get; set; }
    public Item? Item { get; set; }
    public int SkillPoints { get; set; }
    public int[] Skills { get; set; }
    public int AttributePoints { get; set; }
    public int[] Attributes { get; set; }
    public Coord Coordinates { get; set; }

    public int CharacterClass
    {
        get
        {
            return characterClass;
        }
        set
        {
            characterClass = (value <= 3 && value > 0) ? value : 1;
        }
    }

    public GameObject() { }

    public GameObject(Guid saveId, bool isPlayer, int charClass, int level, int experience, int skillPoints, int attributePoints, Coord coordinates,
    string skillValues, string attributeValues, Item item)
    {
        SaveId = saveId;
        IsPlayer = isPlayer;
        CharacterClass = charClass;
        Level = level;
        Experience = experience;
        SkillPoints = skillPoints;
        AttributePoints = attributePoints;
        Coordinates = coordinates;

        Skills = new int[2];
        Attributes = new int[3];

        string[] skillVal = skillValues.Split(',');
        string[] attVal = attributeValues.Split(',');

        for (int i = 0; i < Skills.Count(); i++)
        {
            Skills[i] = int.Parse(skillVal[i]);
        }

        for (int i = 0; i < Attributes.Count(); i++)
        {
            Attributes[i] = int.Parse(attVal[i]);
        }

        this.Item = item;
    }

    public GameObject(bool isPlayer, int level = 1)
    {
        Skills = new int[2];
        Attributes = new int[3];

        IsPlayer = isPlayer;
        Experience = 0;
        Level = level;

        if (IsPlayer)
        {
            SkillPoints = 2;
            Skills[0] = 1;
            Skills[1] = 1;
            AttributePoints = 2;
            Attributes[0] = 1;
            Attributes[1] = 1;
            Attributes[2] = 1;
            CharacterClass = 1;
            Coordinates = new Coord { X = 0, Y = 0 };

        }
        else
        {
            RandomizePoints(level);
        }

    }

    private void RandomizePoints(int level)
    {
        Random random = new Random();
        level += random.Next(1, 4); // adds as many as +3 levels to enemy

        // Convert level to integer and multiple by 2
        int skillPoints = level * 2;
        int attributePoints = level * 2;

        // Random bonus to offset player item
        if (random.Next(2) > 0)
        {
            skillPoints += level;
        }
        else
        {
            attributePoints += level;
        }

        // Randomly assigns points
        do
        {
            Skills[random.Next(2)] += 1;
            skillPoints -= 1;
        } while (skillPoints > 0);

        do
        {
            Attributes[random.Next(3)] += 1;
            attributePoints -= 1;
        } while (attributePoints > 0);
    }

    public bool GameObjectAtCoordinates(int x, int y)
    {
        if (this.Coordinates.X == x && this.Coordinates.Y == y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}