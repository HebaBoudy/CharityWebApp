
using WebAppTutorial.Models;

namespace WebAppTutorial.Interfaces
{
    public interface IDonor_CampaignRepository
    {
        bool Delete(Donor_Campaign row);
        Donor_Campaign Get( int Campaignid); 

      //  ICollection<UsersRegistration> GetAllDonors( int Campaignid);   

    }
}
