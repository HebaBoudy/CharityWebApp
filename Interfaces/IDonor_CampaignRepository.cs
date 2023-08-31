
using WebAppTutorial.Models;

namespace WebAppTutorial.Interfaces
{
    public interface IDonor_CampaignRepository
    {
        bool Delete(Donor_Campaign row);
        List<Donor_Campaign> Get( string title);

        bool Insert(Donor_Campaign row);

    }
}
