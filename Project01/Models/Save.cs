using TBG.Logic;

namespace TBG;

public class Save
{
    public Guid SaveId { get; set; }
    public Guid UserId { get; set; }
    public GameObject PlayerObject { get; set; }
    public DateTime SaveDate { get; set; }
    public int Turns { get; set; }
    public int Units { get; set; }
    public Coord GridConstraints { get; set; }
    public List<Task> Tasks { get; set; }
    public List<Encounter> Encounters { get; set; }
    public List<Item> Items { get; set; }

    public Save() { }
    public Save(User user, GameObject player)
    {
        SaveId = Guid.NewGuid();
        UserId = user.UserId;
        PlayerObject = player;
        SaveDate = DateTime.Now;
        Turns = 25;
        Units = 2;
        SetGridConstraints(Turns);
        Items = PopulateGameObjects.GenerateItemsList(Turns, GridConstraints.X, GridConstraints.Y);
        Tasks = PopulateGameObjects.GenerateTasksList(Turns, GridConstraints.X, GridConstraints.Y);
        Encounters = PopulateGameObjects.GenerateEncounters(Turns, GridConstraints.X, GridConstraints.Y);

        void SetGridConstraints(int turns)
        {
            int i = turns / 6;
            GridConstraints = new Coord { X = i, Y = i };
        }
    }
}
