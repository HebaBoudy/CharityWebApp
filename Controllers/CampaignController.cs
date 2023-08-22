using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;
using WebAppTutorial.DTO;
using WebAppTutorial.Repos;
using Shared.Models;
using System.ComponentModel.Design;

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
        [ProducesResponseType(200, Type = typeof(Campaign))]
        public IActionResult GetAllCampaigns([FromQuery] string userName)
        {
          
            Company company =_CompanyRepo.GetCompanyByUserName(userName);
            var campaigns =_CampaignRepo.GetAllCampaigns(company.ID);

            if (company == null)
                return NotFound();

            return Ok(campaigns);
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]

        public IActionResult CreateNewCampaign([FromBody] FrontEndCampaign FrontCampaign)
        {
            if (FrontCampaign == null)
                return BadRequest();
            //Campaign campaign= _Mapper.Map<Campaign>(newCampaign);
           
            Company company = _CompanyRepo.GetCompanyByUserName(FrontCampaign.UserName);
            if (company == null)
                return BadRequest();
            Campaign newCampaign = new Campaign()
            {
                Company = company,
                CompanyID = company.ID,
                Title = FrontCampaign.Title,
                RequiredAmount = FrontCampaign.RequiredAmount,
                CollectedAmount = 0,
                Description = FrontCampaign.Description,
            };
            
            if(!_CampaignRepo.CreateCampain(newCampaign))
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
        [Route("Get/{Title}")]
        [HttpGet]
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
        //[HttpGet]
        //[ProducesResponseType(200, Type = typeof(List<Campaign>))]
        //public IActionResult GetAll()
        //{
        //    var campaigns = _CampaignRepo.GetAll();


        //    return Ok(campaigns);
        //}

    }
}
