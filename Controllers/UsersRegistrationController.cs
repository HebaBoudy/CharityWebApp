using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using System.ComponentModel.DataAnnotations;
using WebAppTutorial.DTO;
using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;
using WebAppTutorial.Repos;

namespace WebAppTutorial.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersRegistrationController : Controller
    {
      
        private readonly IUsersRegistration _UsersRepo;
        private readonly IMapper _Mapper;
        private readonly ILoginRepository _LoginRepos;

        public UsersRegistrationController(IUsersRegistration UserRepo,IMapper mapper, ILoginRepository LoginRepos)
        {
            _UsersRepo = UserRepo;
            _Mapper = mapper;
            _LoginRepos = LoginRepos;   
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UsersRegistration>))]
        public IActionResult GetUsers()
        {
            var Users =_UsersRepo.GetAll();
            if (!ModelState.IsValid)
                return BadRequest(Users);
            return Ok(Users);
        }
       
        [Route("{UserName}")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(UsersRegistration))]
        public IActionResult GetUserByUserName(string UserName)
        {
            if (!_UsersRepo.UserNameExists(UserName))
                return NotFound();

            var user = _UsersRepo.GetUserByUserName(UserName);
            if (!ModelState.IsValid)
                return BadRequest(user);

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(200,Type=typeof(SignUpResponse))]
        
        public IActionResult CreateNewUser( [FromBody] UsersRegistration user)
        {
            SignUpResponse response = new SignUpResponse();
          
           
            if (_UsersRepo.UserExistsEmail(user.Email))
            {
                response.Message = "This email is already signed in";
                response.StatusCode = 422;
                response.IsSignedIn = false;
                return Ok(response);
            }

            if(_LoginRepos.UserExists(user.UserName))
            {
                response.Message = " username exists , try another one ";
                response.StatusCode = 422;
                response.IsSignedIn = false;
                return Ok(response);
            }

            if (!ModelState.IsValid)
            {
                response.Message = " Something went wrong ";
                response.StatusCode = 404;
                response.IsSignedIn = false;
                return Ok(response);
            }
            Login newLogin = new Login()
            {
                UserName = user.UserName,
                Password = user.Password,
                Type = true,
                Company = null,
                UserRegistration = user
            };
            if (!_UsersRepo.AddUser(user)|| !_LoginRepos.CreateLogin(newLogin))
                ModelState.AddModelError("", " Something went wrong on adding ");

            response.Message = " Successful ";
            response.StatusCode = 200;
            response.IsSignedIn = true;

            return Ok(response);
        }

        [Route("UpdateProfile")]
        [HttpPost]
        [ProducesResponseType(200,Type=typeof(SignUpResponse))]
        public IActionResult UpdateUserInfo( [FromQuery] string oldUserName,[FromBody] UsersRegistration user)
        {       /*hena*/
             Login login=_LoginRepos.GetLogin(oldUserName);
            if(login!=null)
            {
                login.UserName = user.UserName;
                _LoginRepos.UpdateUser(login);
            }
            SignUpResponse signUpResponse = new SignUpResponse();   
            UsersRegistration oldUser = _UsersRepo.GetUserByUserName(oldUserName);
            oldUser.FName = user.FName;
            oldUser.LName = user.LName;
            oldUser.UserName = user.UserName;
            oldUser.Email = user.Email;
            oldUser.PhoneNo = user.PhoneNo; 
            oldUser.Password = user.Password;
            if (!_UsersRepo.UpdateUserInfo(oldUserName, oldUser) || !ModelState.IsValid)
            {
                signUpResponse.Message = "Something went Wrong";
                signUpResponse.StatusCode = 200;
                return Ok(signUpResponse);
            }
            signUpResponse.Message = "Updated Successfully";
            signUpResponse.StatusCode = 200;
            return Ok(signUpResponse);

        }




        [Route("Delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult RemoveUser(int id)
        {
            var user = _UsersRepo.GetUserByID(id);
            if (user == null)
                return NotFound();

            Login delete= _LoginRepos.GetLogin(user.UserName);

            _LoginRepos.DeleteUser(delete);

            var userdeleted=_UsersRepo.DeleteUser(user);
         
            if (!userdeleted)
            {
                ModelState.AddModelError("","something went wrong");
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok("Deleted successfully");




        }
       


    }
}