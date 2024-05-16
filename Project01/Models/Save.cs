using System.Text.RegularExpressions;

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
    public List<GameObject> Encounters { get; set; }
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
        GenerateItemsList();

        void SetGridConstraints(int turns)
        {
            int i = (int)Math.Ceiling((double)turns / 2);
            GridConstraints = new Coord { X = i, Y = i };
        }

        void GenerateItemsList()
        {
            int count = Turns / 6; // Generates items at an ~ratio of 4:25
            List<Item> newItemsList = new();
            bool repeat;
            Item newItem;

            for (int i = 0; i < count; i++) // loops until count integer is satisfied
            {
                if (newItemsList.Count() > 0)
                {
                    do // iterates until an item with unique X, Y coordinates can be added
                    {
                        repeat = true;
                        newItem = RandomItem();
                        foreach (Item item in newItemsList) // checks that an existing item in the list doesn't have the same coordinates
                        {
                            bool notDuplicate = newItem.Coordinates.X != item.Coordinates.X && newItem.Coordinates.Y != item.Coordinates.Y;
                            bool notAtZero = newItem.Coordinates.X != 0 && newItem.Coordinates.Y != 0;
                            if (notDuplicate && notAtZero)
                            {
                                repeat = false;
                            }
                        }
                    } while (repeat);
                    newItemsList.Add(newItem); // Adds the item if it passes the unique check
                }
                else
                {
                    newItem = RandomItem();
                    newItemsList.Add(newItem);
                }
            }
            Items = newItemsList;

            Item RandomItem() // nested method to generate item with random X, Y coordinates
            {
                Random random = new();
                Item randomItem = new();
                randomItem.Coordinates = new Coord { X = random.Next(-GridConstraints.X, GridConstraints.X), Y = random.Next(-GridConstraints.Y, GridConstraints.Y) };
                return randomItem;
            }
        }
    }
}