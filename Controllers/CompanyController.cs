using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppTutorial.Interfaces;
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
    }
}
