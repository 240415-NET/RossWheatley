

using Practice.Entities;

public interface IItemStorageEFRepo
{
    Task<Item> CreateItemInDBAsync(Item item);
}