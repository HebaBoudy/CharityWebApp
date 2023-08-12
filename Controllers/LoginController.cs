using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using WebAppTutorial.DTO;
namespace WebAppTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginRepository _LoginRepos;
        private readonly ICompanyRepository _CompanyRepos;
        private readonly IMapper _Mapper;

        public LoginController(ILoginRepository LoginRepos,IMapper mapper, ICompanyRepository CompanyRepos)
        {
            _LoginRepos = LoginRepos;
            _Mapper = mapper;
            _CompanyRepos = CompanyRepos;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LoginDto>))]
        
        public IActionResult GetLogins()
        {
            var Login =_Mapper.Map<List<LoginDto>>(_LoginRepos.GetLogins());

            if (!ModelState.IsValid)
                return BadRequest(Login);
            return Ok(Login);
        }
        [HttpGet("{UserName,password}")]
        [ProducesResponseType(200,Type=typeof ( LoginDto))]
        public IActionResult GetLogin(string username,string pass )
        {
            if (!_LoginRepos.UserExists(username, pass))
                return NotFound();

            var login =_Mapper.Map<LoginDto>( _LoginRepos.GetLogin(username,pass));
            if(!ModelState.IsValid)
                return BadRequest(login);   
            return Ok(login);
        }
       
    }
}
