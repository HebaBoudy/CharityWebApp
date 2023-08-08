using WebAppTutorial.Models;

namespace WebAppTutorial.Interfaces
{
    public interface ILoginRepository
    {
        ICollection<Login> GetLogins();
        Login GetLogin(string  UserName,string Password);
        bool UserExists(string Name,string password);
      
       

    }
}
