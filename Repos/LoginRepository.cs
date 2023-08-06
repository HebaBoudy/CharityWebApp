using WebAppTutorial.Data;
using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;

namespace WebAppTutorial.Repos
{
    public class LoginRepository:ILoginRepository
    {
        private readonly DataContext _context;

        public LoginRepository(DataContext Context)
        {
            _context = Context;
        }

       
        ICollection<Login> ILoginRepository.GetLogins()
        {
            return _context.Login.OrderBy(e => e.LoginId).ToList();
        }

        bool ILoginRepository.PasswordExists(string password)
        {
            return _context.Login.Any(e => e.Password == password);
        }

        bool ILoginRepository.UserNameExists(string Name)
        {
            return _context.Login.Any(e => e.UserName == Name);
        }
     
    }
}
