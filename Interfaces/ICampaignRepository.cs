using WebAppTutorial.Models;

namespace WebAppTutorial.Interfaces
{
    public interface ICampaignRepository
    {
        ICollection<Campaign> GetAllCampaigns(int id);
        bool CreateCampain(Campaign campaign);
        Campaign GetCampaignByName(string name);
        bool UpdateCampaign(Campaign campaign);
        bool DeleteCampaign(Campaign campaign);
        bool  CampaignExists(Campaign campaign); 

    }
}
