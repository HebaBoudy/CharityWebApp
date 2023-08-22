using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;
using WebAppTutorial.Data;
using Shared.Models;
namespace WebAppTutorial.Repos
{
    public class CampaignRepository : ICampaignRepository
    {
        public readonly DataContext _dataContext;

        public CampaignRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CampaignExists(Campaign campaign)
        {
          return _dataContext.Campaign.Any(e=>e.Title==campaign.Title&&e.CompanyID==campaign.CompanyID);
        }

        public bool CreateCampain(Campaign campaign)
        {
                
          _dataContext.Campaign.Add(campaign);
            var added = _dataContext.SaveChanges();
            return (added > 0) ? true: false;
        }

        public bool DeleteCampaign(Campaign campaign)
        {
           _dataContext.Campaign.Remove(campaign);
            var deleted = _dataContext.SaveChanges();
            return (deleted > 0) ? true : false;
        }

        public ICollection<Campaign> GetAll()
        {
           return  _dataContext.Campaign.ToList();
        }

        public ICollection<Campaign> GetAllCampaigns(int id)
        {
            return _dataContext.Campaign.Where(e => e.CompanyID == id).ToList();
        }

      

        public Campaign GetCampaignByName(string name)
        {
            return _dataContext.Campaign.Where(e => e.Title == name).FirstOrDefault();
        }



        public bool UpdateCampaign(Campaign campaign)
        {
          _dataContext.Campaign.Update(campaign);
            var updated = _dataContext.SaveChanges();
            return (updated > 0) ? true : false;
        }
    }
}
