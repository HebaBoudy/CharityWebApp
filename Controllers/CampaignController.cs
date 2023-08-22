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
        // [Route("Delete/{username}")]
        [Route("Update")]
        [HttpPost]
       [ProducesResponseType(200,Type=typeof(UpdateCampaignResponse))]
        public IActionResult UpdateCampaign([FromBody]FrontEndCampaign campaign, [FromQuery] string Oldtitle)
        {
            UpdateCampaignResponse response=new UpdateCampaignResponse();    
            if (!_CampaignRepo.CampaignExists(Oldtitle))
            {
                response.message = "This Campaign doesn't exist";
                response.statusCode = 200;
                return Ok(response);
            }
            Campaign GetCampaign = _CampaignRepo.GetCampaignByName(Oldtitle);
             GetCampaign.RequiredAmount=campaign.RequiredAmount;
             GetCampaign.Description=campaign.Description;   
             GetCampaign.Title=campaign.Title; 
            
            if (!_CampaignRepo.UpdateCampaign(GetCampaign))
            {
                response.message = "Something went wrong on updated";
                response.statusCode = 200;
                return Ok(response);
            }
            if (!ModelState.IsValid)
                return BadRequest();
            response.message = "Updated Successfully";
            response.statusCode = 200;
            return Ok(response);
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
