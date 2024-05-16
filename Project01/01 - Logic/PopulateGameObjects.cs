namespace TBG.Logic;

public static class PopulateGameObjects
{
    public static List<Item> GenerateItemsList(int turns, int conX, int conY)
    {
        int count = turns / 6; // Generates items at an ~ratio of 4:25
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
                newItem = RandomItem(conX, conY);
                newItemsList.Add(newItem);
            }
        }
        return newItemsList;

        Item RandomItem(int conX, int conY) // nested method to generate item with random X, Y coordinates
        {
            Random random = new();
            Item randomItem = new();
            randomItem.Coordinates = new Coord { X = random.Next(-conX, conX), Y = random.Next(-conY, conY) };
            return randomItem;
        }
    }
}