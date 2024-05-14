namespace TBG;

public class Save
{
    public Guid SaveId { get; set; }
    public Guid UserId { get; set; }
    public GameObject PlayerObject { get; set; }
    public DateTime SaveDate { get; set; }
    public int Turns { get; set; }
    public int Units { get; set; }
    public List<int> Tasks { get; set; } // board positions where tasks are populated
    public List<int> Encounters { get; set; } // board positions where encounters occur

    public Save() { }
    public Save(User user, GameObject player)
    {
        SaveId = Guid.NewGuid();
        UserId = user.UserId;
        PlayerObject = player;
        SaveDate = DateTime.Now;
        Turns = 25;
        Units = 2;
    }

    List<int> BoardRandomizeUtility(int typeId)
    {
        Random random = new();
        int count;
        if (typeId == 0) // Tasks
        {
            count = Convert.ToInt32(Math.Round(Convert.ToDouble(Turns / 2))) + random.Next(Convert.ToInt32(Math.Round(Convert.ToDouble(Turns / 2))));
        }
        else // Encounters
        {
            count = Convert.ToInt32(Math.Round(Convert.ToDouble(Turns / 4))) + random.Next(Convert.ToInt32(Math.Round(Convert.ToDouble(Turns / 2))));
        }

        return new List<int>();
    }
}