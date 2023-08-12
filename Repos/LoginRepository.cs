using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            return _context.Login.ToList();

        }
        Login ILoginRepository.GetLogin(string username, string pass)
        {
            return _context.Login.Where(e => e.UserName == username && e.Password == pass).FirstOrDefault();
        }
        bool ILoginRepository.UserExists(string Name, string password)
        {
            return _context.Login.Any(e => e.Password == password && e.UserName == Name);
        }

         bool ILoginRepository.CreateLogin(Login login)
        {
            bool user = _context.Login.Any(e => e.UserName == login.UserName);
            if(user)
                return false;
         

            _context.Add(login);
            var saved = _context.SaveChanges();
            return (saved > 0) ? true:false;
        
        }
         bool ILoginRepository.UserExists(string userName)
        {
            return _context.Login.Any(e => e.UserName == userName);
        }

        public Login GetLogin(string UserName)
        {
            return _context.Login.Where(e => e.UserName == UserName).FirstOrDefault();
        }

        public bool DeleteUser(Login login)
        {
             _context.Login.Remove(login);
            var saved = _context.SaveChanges();
            return (saved > 0) ? true : false;

        }
    }
};
