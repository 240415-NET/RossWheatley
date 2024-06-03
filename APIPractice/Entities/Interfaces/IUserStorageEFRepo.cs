

using Practice.Entities;

public interface IUserStorageEFRepo
{
    Task<User> CreateUserInDBAsync(User newUser);
}