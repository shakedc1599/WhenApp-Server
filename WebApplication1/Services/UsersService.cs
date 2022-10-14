using whenAppModel.Models;
using WhenUp;

namespace whenAppModel.Services
{

    public class UsersService : IUsersService
    {
        private readonly WhenAppContext _context;

        public UsersService(WhenAppContext Context)
        {
            _context = Context;
        }

        //Get user by his username - action number 3.
        public async Task<User?> Get(string username)
        {
            var user = await _context.Users.FindAsync(username);
            return user;
        }


        public async Task<User?> Add(string username, string password)
        {

            var user = new User(username, password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        //delete user - action number 5.
        public async Task<User?> Delete(string username)
        {
            var user = await Get(username);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }

        public async Task<bool> Validation(string username, string password)
        {
            var user = await Get(username);

            return user != null && user.Password == password;
        }

    }
}
