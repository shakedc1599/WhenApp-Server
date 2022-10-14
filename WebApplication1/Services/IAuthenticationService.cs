using whenAppModel.Models;

namespace WebApplication1.Services
{
    public interface IAuthenticationService
    {
        public string CreateToken(string username);

        public string ValidateToken();

        public string GetUsernameFromToken();
    }
}
