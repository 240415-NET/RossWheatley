using Practice.Entities;

public interface IUserService
{
    Task<User> CreateNewUserAsync(User newUser);
}