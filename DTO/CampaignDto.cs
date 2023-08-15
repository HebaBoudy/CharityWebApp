using WebAppTutorial.Models;

namespace WebAppTutorial.DTO
{
    public class CampaignDto
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RequiredAmount { get; set; }
        public int CollectedAmount { get; set; }
    }
}
