namespace Backend.Core.RepositoryInterface
{
    public interface IUserRepository
    {
        public bool CheckValidUser(string username, string password);
    }
}
