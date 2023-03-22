using Backend.Core.RepositoryInterface;

namespace Backend.Core.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool CheckValidUser(string username, string password)
        {
            string _username = _configuration.GetValue<string>("ClientId");
            string _password = _configuration.GetValue<string>("ClientPassword");
            return (_username.Equals(username, StringComparison.OrdinalIgnoreCase) && _password == password);
        }
    }
}
