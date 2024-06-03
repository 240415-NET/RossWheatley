using Practice.Entities;

public interface IItemService
{
    Task<Item> CreateNewItemAsync(Item newItem);
}