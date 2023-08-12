using WebAppTutorial.Models;

namespace WebAppTutorial.Interfaces
{
    public interface IUsersRegistration
    {
          UsersRegistration GetUserByEmail(string email);
          UsersRegistration  GetUserByID(int id);
          UsersRegistration GetUserByUserName(string userName);
          bool UserExistsEmail(string email);
          bool UserExistsID(int  ID);
          bool UserNameExists(string username);   
          ICollection<UsersRegistration> GetAll();
          bool AddUser(UsersRegistration user);
          bool UpdateUserInfo(UsersRegistration user);
          bool DeleteUser(UsersRegistration user);

    }
}
