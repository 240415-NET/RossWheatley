namespace TBG.Logic;


public static class PopulateGameObjects
{
    public static List<Item> GenerateItemsList(int turns, int conX, int conY)
    {
        int count = turns / 3;
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
                        if (notDuplicate)
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
        return newItemsList;

        Item RandomItem() // nested method to generate item with random X, Y coordinates
        {
            Random random = new();
            Item randomItem = new();
            randomItem.Coordinates = new Coord { X = random.Next(-conX, conX), Y = random.Next(-conY, conY) };
            return randomItem;
        }
    }

    public static List<Task> GenerateTasksList(int turns, int conX, int conY)
    {
        int count = turns / 2;
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
        return newTasksList;

        Task RandomTask() // nested method to generate item with random X, Y coordinates
        {
            Random random = new();
            Task randomTask = new();
            randomTask.Coordinates = new Coord { X = random.Next(-conX, conY), Y = random.Next(-conX, conY) };
            return randomTask;
        }
    }

    public static List<Encounter> GenerateEncounters(int turns, int conX, int conY)
    {
        int count = turns / 3;
        List<Encounter> encountersList = new();
        bool repeat;
        Encounter newEncounter;

        for (int i = 0; i < count; i++) // loops until count integer is satisfied
        {
            do // iterates until an item with unique X, Y coordinates can be added
            {
                repeat = true;
                newEncounter = RandomEncounter();
                bool posZero = newEncounter.Coordinates.X == 0 && newEncounter.Coordinates.Y == 0;

                foreach (Encounter encounter in encountersList) // checks that an existing item in the list doesn't have the same coordinates
                {
                    bool notDuplicate = newEncounter.Coordinates.X != encounter.Coordinates.X && newEncounter.Coordinates.Y != encounter.Coordinates.Y;

                    if (notDuplicate)
                    {
                        repeat = false;
                    }
                }

                if (!posZero)
                {
                    repeat = false;
                }

            } while (repeat);
            encountersList.Add(newEncounter); // Adds the item if it passes the unique check
        }

        return encountersList;

        Encounter RandomEncounter() // nested method to generate item with random X, Y coordinates
        {
            Random random = new();
            Encounter randomEncounter = new();
            randomEncounter.Coordinates = new Coord { X = random.Next(-conX, conX), Y = random.Next(-conY, conY) };
            return randomEncounter;
        }
    }

}
