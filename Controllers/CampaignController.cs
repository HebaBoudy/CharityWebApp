using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;
using WebAppTutorial.DTO;
using WebAppTutorial.Repos;
using Shared.Models;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;

namespace WebAppTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : Controller
    {
        public readonly ICampaignRepository _CampaignRepo;
        public readonly ICompanyRepository _CompanyRepo;
        public readonly IMapper _Mapper;
        public readonly IDonor_CampaignRepository _Donor_CampaignRepos;
        public readonly IUsersRegistration _UserRepos;
        public CampaignController(ICampaignRepository CampaignRepo, ICompanyRepository CompanyRepo,IUsersRegistration UserRepos, IMapper Mapper, IDonor_CampaignRepository Donor_CampaignRepos)
        {
            _CampaignRepo = CampaignRepo;
            _CompanyRepo = CompanyRepo;
            _Mapper = Mapper;
            _Donor_CampaignRepos = Donor_CampaignRepos;
            _UserRepos= UserRepos;
        }
        [Route("GetCampaignByTitle")]
        [HttpGet]
        public IActionResult GetCampaignByTitle([FromQuery] string Title)
        {
         Campaign campaign=  _CampaignRepo.GetCampaignByName(Title);
            Company company = _CompanyRepo.GetCompanyById((int)campaign.CompanyID);
            campaign.Company = company;
            return Ok(campaign);
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Campaign))]
        public IActionResult GetAllCampaigns([FromQuery] string userName)
        {

            Company company = _CompanyRepo.GetCompanyByUserName(userName);
            var campaigns = _CampaignRepo.GetAllCampaigns(company.ID);

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

            if (!_CampaignRepo.CreateCampain(newCampaign))
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
        [ProducesResponseType(200, Type = typeof(UpdateCampaignResponse))]
        public IActionResult UpdateCampaign([FromBody] FrontEndCampaign campaign, [FromQuery] string Oldtitle)
        {
            UpdateCampaignResponse response = new UpdateCampaignResponse();
            if (!_CampaignRepo.CampaignExists(Oldtitle))
            {
                response.message = "This Campaign doesn't exist";
                response.statusCode = 200;
                return Ok(response);
            }
            Campaign GetCampaign = _CampaignRepo.GetCampaignByName(Oldtitle);
            GetCampaign.RequiredAmount = campaign.RequiredAmount;
            GetCampaign.Description = campaign.Description;
            GetCampaign.Title = campaign.Title;

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
        [Route("Delete/{Title}")]
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteCampaign(string title)
        {

            Campaign campaign = _CampaignRepo.GetCampaignByName(title);

            if (campaign == null)
                return BadRequest();/*TODO*/
            //Donor_Campaign row = _Donor_CampaignRepos.Get(campaign.Title);
            //if (row != null)
            //    _Donor_CampaignRepos.Delete(row);
            if (!_CampaignRepo.DeleteCampaign(campaign))
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok("Deleted Successfully");



        }
        [Route("All")]

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Campaign))]
        public IActionResult GetAllCampaigns()
        {


            var campaigns = _CampaignRepo.GetAll();
            for (var i = 0; i < campaigns.Count; i++)
            {
                Company company = _CompanyRepo.GetCompanyById((int)campaigns[i].CompanyID);
                campaigns[i].Company = company;
            }
            if (campaigns == null)
                return NotFound();

            return Ok(campaigns);
        }
        [Route("Pay")]
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult EditCollectedAmount( [FromBody] Donor_Campaign Object)
        {
            Campaign getCampaign = _CampaignRepo.GetCampaignByName(Object.Title);
            getCampaign.CollectedAmount += Object.amount;
            if (getCampaign.CollectedAmount == getCampaign.RequiredAmount)
                getCampaign.Active = false;
            _CampaignRepo.UpdateCampaign(getCampaign);
            _Donor_CampaignRepos.Insert(Object);

            return Ok(getCampaign);


        }
        [Route("RetrieveDonors/{title}")]
        [ProducesResponseType(200,Type=typeof(Donor_Campaign_Response))]
        [HttpGet]
        public IActionResult GetDonors(string title)
        {
            var row = _Donor_CampaignRepos.Get(title);
            List<Donor_Campaign_Response> response=new List<Donor_Campaign_Response> ();
         
           
                for(var i=0;i<row.Count;i++)
                {
                Donor_Campaign_Response temp = new Donor_Campaign_Response()
                {
                    amount = row[i].amount,
                    email = _UserRepos.GetUserByUserName(row[i].UserName).Email,
                    Mobile = _UserRepos.GetUserByUserName(row[i].UserName).PhoneNo,
                    UserName = row[i].UserName

                };
                response.Add(temp); 

                }
               
            return Ok(response);
        }

    }
}
