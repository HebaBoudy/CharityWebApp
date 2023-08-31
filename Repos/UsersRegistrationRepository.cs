using Microsoft.EntityFrameworkCore;
using WebAppTutorial.Data;
using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;

namespace WebAppTutorial.Repos
{
    public class UsersRegistrationRepository : IUsersRegistration
    {
       private readonly DataContext _dataContext;

        public  UsersRegistrationRepository(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        UsersRegistration IUsersRegistration.GetUserByEmail(string  email)
        {
            return _dataContext.UsersRegistration.Where(e => e.Email == email).FirstOrDefault();
        }

         bool IUsersRegistration.UserExistsEmail(string email)
        {
            return _dataContext.UsersRegistration.Any(e => e.Email == email);
        }

        ICollection<UsersRegistration> IUsersRegistration.GetAll()
        {
           return  _dataContext.UsersRegistration.OrderBy(e => e.ID).ToList();
        }

         bool IUsersRegistration.AddUser(UsersRegistration user)
        {
          
           
            _dataContext.Add(user);
            var saved = _dataContext.SaveChanges();
            return (saved > 0) ? true : false;
        }
          UsersRegistration IUsersRegistration.GetUserByID(int id)
        {
         return  _dataContext.UsersRegistration.Where(e => e.ID == id).FirstOrDefault();
          
        }
         bool IUsersRegistration.UserExistsID(int id)
        {
            return _dataContext.UsersRegistration.Any(e => e.ID == id);
        }

        public bool UpdateUserInfo(string oldUser,UsersRegistration user)
        {
          

            _dataContext.UsersRegistration.Update(user);
            var saved = _dataContext.SaveChanges();
            return (saved > 0) ? true : false;
        }

        public bool DeleteUser(UsersRegistration user)
        {
           
            _dataContext.UsersRegistration.Remove(user);
            var saved=_dataContext.SaveChanges();
            return (saved > 0) ? true : false;
        }
        public bool UserNameExists(string userName)
        {
            return _dataContext.Login.Any(e => e.UserName == userName);    

        }

        public UsersRegistration GetUserByUserName(string userName)
        {
            return _dataContext.UsersRegistration.Where(e => e.UserName == userName).FirstOrDefault();
        }

      
    }
}
