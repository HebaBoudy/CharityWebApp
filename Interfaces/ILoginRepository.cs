using WebAppTutorial.Models;

namespace WebAppTutorial.Interfaces
{
    public interface ILoginRepository
    {
        ICollection<Login> GetLogins();
        Login GetLogin(string  UserName,string Password);
        Login GetLogin(string UserName);
        bool UserExists(string Name,string password);
        bool UserExists(string userName);
        bool CreateLogin( Login newlogin);
        bool DeleteUser(Login login);
      bool UpdateUser(Login login); 
       

    }
}
