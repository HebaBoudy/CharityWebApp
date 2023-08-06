using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebAppTutorial.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class LoginController 
    {
        private readonly ILoginRepository _LoginRepos;
        public LoginController(ILoginRepository LoginRepos )
        {
            _LoginRepos = LoginRepos;   

        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Login>))]
        public IActionResult GetLogins()
        {
            var Login = _LoginRepos.GetLogins();
         
            return Ok(Login);
        }

        private IActionResult Ok(ICollection<Login> login)
        {
            throw new NotImplementedException();
        }
    }
}
