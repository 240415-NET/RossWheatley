namespace TBG;

public class Task
{
    // Attempting tasks costs units
    // Successfully completing tasks return units
    // Tasks will have a random chance to return units
    // AttributeA & AttributeB will factor into probability of "winning" a task
    private int unitCost; // random # between 1 - 5
    private int reward; // unitCost + random # between 1 - 5
    private float probability; // random # between 1 - 100
}