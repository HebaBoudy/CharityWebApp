using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAppTutorial.DTO;
using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;
using WebAppTutorial.Repos;

namespace WebAppTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _CompanyRepository;
        private readonly ILoginRepository _LoginRepository;
        private readonly IMapper _Mapper;

        public CompanyController ( ICompanyRepository companyRepository,IMapper mapper, ILoginRepository LoginRepository)
        {
            _CompanyRepository = companyRepository; 
            _Mapper = mapper;   
            _LoginRepository = LoginRepository;
        }


        [HttpGet]
        public IActionResult GetCompanies()
        {
            var Company=_Mapper.Map<List<CompanyDto>>(_CompanyRepository.GetCompanies()) ;
            return Ok(Company);
        }

        [HttpGet("{CompanyId}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        public IActionResult GetCompany(int id)
        {
            if (!_CompanyRepository.CompanyExists(id))
                return NotFound();
            var company = _Mapper.Map <CompanyDto> (_CompanyRepository.GetCompanyById(id));
            if(!ModelState.IsValid)return BadRequest(ModelState);
            return Ok(company);
        }





        [HttpPost]
        [ProducesResponseType (204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCompany([FromQuery ] string userName, [FromQuery] string password,[FromBody]Company company)
        {
            if(_CompanyRepository.GetCompanyByName(company.Name)!=null)
            {
                ModelState.AddModelError("", " Company  already exists ");
                return StatusCode(422);
            }
            if(!ModelState.IsValid)             
              return BadRequest(ModelState);
            if (!_CompanyRepository.AddCompany(company))
                ModelState.AddModelError("", " Something went wrong on adding ");
            Login NewLogin = new Login()
            {
                UserName = userName,
                Password = password,
                Company = company,
                UserRegistration = null,
                Type = false
            };
            _LoginRepository.CreateLogin(NewLogin);
            return Ok(" Company added successfully ");

        }
        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCompany([FromQuery] int id, [FromBody] CompanyDto company)/*id,name,raised amount*/
        {
            if (company == null)
                return BadRequest(ModelState);
            if(id!=company.ID)
                return BadRequest(ModelState);

            if (!_CompanyRepository.UpdateCompany( _Mapper.Map<Company>(company)))
            return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok("Updated successfully ");

        }

       // [Route("Delete/{username}")]
        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteCompany([FromQuery]string userName)
        {
            Company company = _CompanyRepository.GetCompanyByUserName(userName);
            Login  login = _LoginRepository.GetLogin(userName);
            if (company == null)
                return NotFound();

            Login delete = _LoginRepository.GetLogin(company.UserName);

            _LoginRepository.DeleteUser(delete);

            var companydeleted = _CompanyRepository.DeleteCompany(company);

            if (!companydeleted)
            {
                ModelState.AddModelError("", "something went wrong");
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok("Deleted successfully");




        }
    }

   
}
