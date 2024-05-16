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
        Items = PopulateGameObjects.GenerateItemsList();
        GenerateTasksList();

        void SetGridConstraints(int turns)
        {
            int i = (int)Math.Ceiling((double)turns / 2);
            GridConstraints = new Coord { X = i, Y = i };
        }

        void GenerateTasksList()
        {
            int count = Turns / 2;
            List<Task> newTasksList = new();
            bool repeat;
            Task newTask;

            for (int i = 0; i < count; i++) // loops until count integer is satisfied
            {
                if (newTasksList.Count() > 0)
                {
                    do // iterates until an item with unique X, Y coordinates can be added
                    {
                        repeat = true;
                        newTask = RandomTask();
                        foreach (Task task in newTasksList) // checks that an existing item in the list doesn't have the same coordinates
                        {
                            bool notDuplicate = newTask.Coordinates.X != task.Coordinates.X && newTask.Coordinates.Y != task.Coordinates.Y;
                            if (notDuplicate)
                            {
                                repeat = false;
                            }
                        }
                    } while (repeat);
                    newTasksList.Add(newTask); // Adds the item if it passes the unique check
                }
                else
                {
                    newTask = RandomTask();
                    newTasksList.Add(newTask);
                }
            }
            Tasks = newTasksList;

            Task RandomTask() // nested method to generate item with random X, Y coordinates
            {
                Random random = new();
                Task randomTask = new();
                randomTask.Coordinates = new Coord { X = random.Next(-GridConstraints.X, GridConstraints.X), Y = random.Next(-GridConstraints.Y, GridConstraints.Y) };
                return randomTask;
            }
        }
    }
}