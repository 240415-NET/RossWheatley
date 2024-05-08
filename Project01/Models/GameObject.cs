namespace TBG;

public class GameObject
{
    private Guid? gameId;
    private bool isEnemy;
    private int characterClass; // # between 1 - 3
    private int level;
    private int experience;
    private int skillPoints;
    private int[] skills = new int[2];
    // private int skill1; // attack
    // private int skill2; // defense
    private int attributePoints;
    private int[] attributes = new int[3];
    // private int attributeA; // strength
    // private int attributeB; // dexterity
    // private int attributeC; // Luck

    public GameObject(bool isEnemy, int level = 1, Guid? gameId = null)
    {
        if (isEnemy)
        {
            this.gameId = gameId;
            this.isEnemy = false;
            this.level = level;
            experience = 0;
            skillPoints = 2;
            skills[0] = 1;
            skills[1] = 1;
            attributePoints = 2;
            attributes[0] = 1;
            attributes[1] = 1;
            attributes[2] = 1;
        }
        else
        {
            // build an enemy
            this.isEnemy = true;
            RandomizePoints(level);
        }
    }

    void RandomizePoints(int level)
    {
        Random random = new Random();

        // Convert level to integer and multiple by 2
        int skillPoints = level * 2;
        int attributePoints = level * 2;

        // Randomly assigns points
        do
        {
            skills[random.Next(2)] += 1;
            skillPoints -= 1;
        } while (skillPoints > 0);

        do
        {
            attributes[random.Next(3)] += 1;
            attributePoints -= 1;
        } while (attributePoints > 0);
    }
}