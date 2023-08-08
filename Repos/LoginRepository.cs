using WebAppTutorial.Data;
using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;

namespace WebAppTutorial.Repos
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DataContext _context;

        public LoginRepository(DataContext Context)
        {
            _context = Context;
        }

        ICollection<Login> ILoginRepository.GetLogins()
        {
            return _context.Login.OrderBy(e => e.Id).ToList();

        }
        Login ILoginRepository.GetLogin(string username, string pass)
        {
            return _context.Login.Where(e => e.UserName == username && e.Password == pass).FirstOrDefault();
        }
        bool ILoginRepository.UserExists(string Name, string password)
        {
            return _context.Login.Any(e => e.Password == password && e.UserName == Name);
        }

    }
};
