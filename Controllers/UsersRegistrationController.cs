using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
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


        [HttpGet("{UserName}")]
        [ProducesResponseType(200, Type = typeof(UsersRegistration))]
        public IActionResult GetUserByUserName([FromQuery] string UserName)
        {
            if (!_UsersRepo.UserNameExists(UserName))
                return NotFound();

            var user = _UsersRepo.GetUserByUserName(UserName);
            if (!ModelState.IsValid)
                return BadRequest(user);

            return Ok(user);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateNewUser( [FromQuery]  string password,[FromBody] UsersRegistration user)
        {
          
            var emailValidation = new EmailAddressAttribute();
            var isValid = emailValidation.IsValid(user.Email);
            if (!isValid)
            {

                ModelState.AddModelError("", " Incorrect email format ");
                return BadRequest();
            }
            if (_UsersRepo.UserExistsEmail(user.Email))
            {
                ModelState.AddModelError("", "This email is already signed in ");
                return StatusCode(422);

            }

            if(_LoginRepos.UserExists(user.UserName))
            {
                ModelState.AddModelError("", "This userName is taken , try another one  ");
                return StatusCode(422);

            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
          
            if (!_UsersRepo.AddUser(user))
                ModelState.AddModelError("", " Something went wrong on adding ");

            Login newLogin = new Login()
            {
                UserName = user.UserName,
                Password = password,
                Type = true,
                Company = null,
                UserRegistration = user
            };
            if (!_LoginRepos.CreateLogin(newLogin))
                return BadRequest();

            return Ok("Added successfully");
        }



        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserInfo([FromQuery] string oldUserName, [FromBody] UsersRegistration user)
        {
            Login login = _LoginRepos.GetLogin(oldUserName);

            if (user == null)
                return BadRequest(ModelState);
            var emailValidation = new EmailAddressAttribute();
            var isValid = emailValidation.IsValid(user.Email);
            if (!isValid)
            {

                ModelState.AddModelError("", " Incorrect email format ");
                return BadRequest();
            }
            if (!_UsersRepo.UserExistsID(user.ID))
                return NotFound();

            if (!_UsersRepo.UpdateUserInfo(user))
            {
                ModelState.AddModelError("", "Something went wrong ");
                return BadRequest();

            }
            if (!ModelState.IsValid)
                return BadRequest();
         
                login.UserName=user.UserName;
            login.UserRegistration = user;
           
               
            return Ok("Updated successfully ");

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