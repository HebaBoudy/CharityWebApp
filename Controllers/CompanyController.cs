using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;

namespace WebAppTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _CompanyRepository;
        public CompanyController ( ICompanyRepository companyRepository)
        {
            _CompanyRepository = companyRepository; 
        }
        [HttpGet]
        public IActionResult GetCompanies()
        {
            var Company= _CompanyRepository.GetCompanies();
            return Ok(Company);
        }
        [HttpGet("{CompanyId}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        public IActionResult GetCompany(int id)
        {
            if (!_CompanyRepository.CompanyExists(id))
                return NotFound();
            var company = _CompanyRepository.GetCompanyById(id);
            if(!ModelState.IsValid)return BadRequest(ModelState);
            return Ok(company);
        }
    }
}
