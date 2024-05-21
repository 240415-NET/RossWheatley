namespace TBG;

public class Task
{
    // Attempting tasks costs units
    // Attempting tasks gain XP
    // Successfully completing tasks return bonus XP
    // AttributeA & AttributeB will factor into probability of "winning" a task
    public Guid SaveId { get; set; }
    public Guid TaskId { get; set; }
    public int UnitCost { get; set; } // random # between 1 - 5
    public int Reward { get; set; } // unitCost * random # between 1 - 5
    public float Probability { get; set; } // random # between 1 - 100
    public Coord Coordinates { get; set; }

    public Task()
    {
        TaskId = Guid.NewGuid();
    }

    public Task(Guid saveId, Guid taskId, int cost, int reward, int probability, Coord coordinates)
    {
        SaveId = saveId;
        TaskId = taskId;
        UnitCost = cost;
        Reward = reward;
        Probability = (float)probability;
        Coordinates = coordinates;
    }
}