﻿using WebAppTutorial.Models;
using Shared.Models;
namespace WebAppTutorial.Interfaces
{
    public interface ICampaignRepository
    {
        List<Campaign> GetAll();
        ICollection<Campaign> GetAllCampaigns(int ID);
        bool CreateCampain(Campaign campaign);
        Campaign GetCampaignByName(string name);
        bool UpdateCampaign(Campaign campaign);
        bool DeleteCampaign(Campaign campaign);
        bool  CampaignExists(string title); 


    }
}
