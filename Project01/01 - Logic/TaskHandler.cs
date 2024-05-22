namespace TBG.Logic;

public static class TaskHandler
{
    public static bool? AttemptTask()
    {
        Random random = new();
        Task task = GetTask();
        DeleteTask();
        if (task.UnitCost <= Session.ActiveSave.Units)
        {
            Session.ActiveSave.Units -= task.UnitCost;
            int index = random.Next(Session.ActiveSave.PlayerObject.Attributes.Count()); // randomly chooses which attribute
            int count = Session.ActiveSave.PlayerObject.Attributes[index]; // # of attempted rolls player will get

            for (int i = 0; i < count; i++)
            {
                if (random.Next(1, 100) <= task.Probability)
                {
                    CharacterHandler.RewardPlayer(task.Reward);
                    return true;
                }
            }
            CharacterHandler.RewardPlayer(5); // default reward
            return false;
        }
        return null;
    }
    public static Guid? TaskIdSearch()
    {
        foreach (Task task in Session.ActiveSave.Tasks)
        {
            if (Session.ActiveSave.PlayerObject.GameObjectAtCoordinates(task.Coordinates.X, task.Coordinates.Y))
            {
                return task.TaskId;
            }
        }
        return null;
    }

    public static Task GetTask()
    {
        Task task = Session.ActiveSave.Tasks.Find(task => task.TaskId == (Guid)TaskIdSearch()) ?? new Task();

        if (task.UnitCost == 0)
        {
            task = RandomizeTask(task);
        }
        DeleteTask();
        Session.ActiveSave.Tasks.Add(task);
        return task;
    }

    static Task RandomizeTask(Task task)
    {
        Random random = new();
        task.UnitCost = random.Next(1, 5);
        task.Reward = (task.UnitCost * random.Next(1, 5)) + 5;
        task.Probability = random.Next(0, 100);
        return task;
    }

    public static void DeleteTask()
    {
        DeleteTask((Guid)TaskIdSearch());
    }

    public static void DeleteTask(Guid taskId)
    {
        Session.ActiveSave.Tasks.RemoveAll(task => task.TaskId == taskId);
    }
}