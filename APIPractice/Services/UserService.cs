using Practice.Entities;

namespace Practice.Services;

public class UserService : IUserService
{
    private readonly IUserStorageEFRepo userStorage;

    public UserService(IUserStorageEFRepo efRepo)
    {
        userStorage = efRepo;
    }

    public async Task<User> CreateNewUserAsync(User newUser)
    {
        if (UserExists(newUser.Name) == true)
        {
            throw new Exception("User already exists!");
        }

        if (string.IsNullOrEmpty(newUser.Name) == true)
        {
            throw new Exception("Username cannot be blank!");
        }

        await userStorage.CreateUserInDBAsync(newUser);

        return newUser;
    }

    public bool UserExists(string userName)
    {
        return false;
    }
}