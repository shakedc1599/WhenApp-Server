using whenAppModel.Models;

namespace whenAppModel.Services
{
    public interface IUsersService
    {

        public Task<User?> Get(string username);

        public Task<User?> Add(string username, string password);

        public Task<User?> Add(User user);

        public Task<bool> Validation(string username, string password);

        public Task<User?> Delete(string username);
    }
}
