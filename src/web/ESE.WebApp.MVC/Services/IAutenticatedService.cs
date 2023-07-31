using ESE.WebApp.MVC.Models;

namespace ESE.WebApp.MVC.Services
{
    public interface IAutenticatedService
    {
        Task<UserResponseLogin> Login(UserLogin userLogin);
        Task<UserResponseLogin> Register(UserRegister userRegister);
    }
}
