namespace TBG;

public class GameObject
{
    public bool IsPlayer { get; set; }
    public int CharacterClass { get; set; } // # between 1 - 3
    public int Level { get; set; }
    public int Experience { get; set; }
    public int SkillPoints { get; set; }
    public int[] Skills { get; set; }
    public int AttributePoints { get; set; }
    public int[] Attributes { get; set; }

    public GameObject() { }

    public GameObject(bool isPlayer, int level = 1/*, Guid? gameId = null*/)
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

        }
        else
        {
            RandomizePoints(Level);
        }

    }

    private void RandomizePoints(int level)
    {
        Random random = new Random();

        // Convert level to integer and multiple by 2
        int skillPoints = level * 2;
        int attributePoints = level * 2;

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
}