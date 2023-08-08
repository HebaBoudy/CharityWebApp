using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebAppTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginRepository _LoginRepos;

        public LoginController(ILoginRepository LoginRepos)
        {
            _LoginRepos = LoginRepos;

        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Login>))]
        public IActionResult GetLogins()
        {
            var Login = _LoginRepos.GetLogins();

            if (!ModelState.IsValid)
                return BadRequest(Login);
            return Ok(Login);
        }
        [HttpGet("{UserName,password}")]
        [ProducesResponseType(200,Type=typeof ( Login))]
        public IActionResult GetLogin(string username,string pass )
        {
            if (!_LoginRepos.UserExists(username, pass))
                return NotFound();
            var login = _LoginRepos.GetLogin(username,pass);
            if(!ModelState.IsValid)
                return BadRequest(login);   
            return Ok(login);
        }


    }
}
