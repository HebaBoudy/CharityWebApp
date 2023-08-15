using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;
using WebAppTutorial.DTO;
using WebAppTutorial.Repos;

namespace WebAppTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController :Controller
    {
        public readonly ICampaignRepository _CampaignRepo;
        public readonly ICompanyRepository _CompanyRepo;
        public readonly IMapper _Mapper;
        public readonly IDonor_CampaignRepository _Donor_CampaignRepos;

        public CampaignController(ICampaignRepository CampaignRepo,ICompanyRepository CompanyRepo, IMapper Mapper, IDonor_CampaignRepository Donor_CampaignRepos)
        {
            _CampaignRepo=CampaignRepo;
            _CompanyRepo=CompanyRepo;
            _Mapper=Mapper; 
            _Donor_CampaignRepos=Donor_CampaignRepos;   
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(CampaignDto))]
        public IActionResult GetAllCampaigns([FromQuery]int Companyid)
        {
           var campaigns= _Mapper.Map<List<CampaignDto>>(_CampaignRepo.GetAllCampaigns(Companyid));
           CompanyDto company= _Mapper.Map<CompanyDto>(_CompanyRepo.GetCompanyById(Companyid));    

            if (company==null)
                return NotFound();

            return Ok(campaigns);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateNewCampaign([FromBody] CampaignDto newCampaign)
        {
            Campaign campaign= _Mapper.Map<Campaign>(newCampaign);
            campaign.Company = _CompanyRepo.GetCompanyById(newCampaign.CompanyID);

           if( _CampaignRepo.CampaignExists(campaign))
            {
                ModelState.AddModelError("", " This title already exists for the same company , try another name  ");
                return BadRequest();

            }
            if(!_CampaignRepo.CreateCampain(campaign))
            {
                ModelState.AddModelError("", " Something went wrong on adding ");
                return BadRequest();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok("Added successfully ");

        }
        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCampaign([FromQuery] string Name,CampaignDto campaign)
        {
            if (campaign == null)
                return BadRequest(ModelState);
            if (Name != campaign.Title)
                return BadRequest(ModelState);
          Campaign c=  _Mapper.Map<Campaign>(campaign);
            if (!_CampaignRepo.UpdateCampaign(c))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            if (c.CollectedAmount == c.RequiredAmount)
                c.Active = false;
            return Ok("Updated successfully ");
            
        }
        [Route("Delete/{title}")]
        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteCampaign( string title)
        {
           
            Campaign campaign =_CampaignRepo.GetCampaignByName(title);
         
            if (campaign == null)
                return BadRequest();
            Donor_Campaign row = _Donor_CampaignRepos.Get(campaign.ID);
            if (row != null)
                _Donor_CampaignRepos.Delete(row);
            if (! _CampaignRepo.DeleteCampaign(campaign))
            return BadRequest();

          if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok("Deleted Successfully");



        }

    }
}
