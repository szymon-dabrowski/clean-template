namespace Clean.Modules.UserAccess.Domain.Users.Services;
public interface IPasswordHashing
{
    string Hash(string password);
}