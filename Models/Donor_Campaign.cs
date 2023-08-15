namespace WebAppTutorial.Models
{
    public class Donor_Campaign/*Many To many Rel*/
    {
        public int ID { get; set; }
        public int CampaignId { get; set; }
        public int DonorID { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual UsersRegistration UsersRegistration { get; set; }
    }
}
