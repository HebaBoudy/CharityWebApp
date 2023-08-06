using WebAppTutorial.Models;

namespace WebAppTutorial.Interfaces
{
    public interface ILoginRepository
    {
        ICollection<Login> GetLogins();
        bool UserNameExists(string Name);

         bool PasswordExists(string password);
       

    }
}
