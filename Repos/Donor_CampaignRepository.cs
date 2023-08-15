using WebAppTutorial.Interfaces;
using WebAppTutorial.Data;
using WebAppTutorial.Models;

namespace WebAppTutorial.Repos
{
    public class Donor_CampaignRepository: IDonor_CampaignRepository
    {
        public readonly DataContext _dataContext;
        public Donor_CampaignRepository(DataContext dataContext)
        {
            _dataContext = dataContext; 

        }

        public bool Delete(Donor_Campaign row)
        {
            _dataContext.Donor_Campaign.Remove(row);
            var deleted = _dataContext.SaveChanges();
            return (deleted > 0) ? true : false;
        }

        public Donor_Campaign Get(int Campaignid)
        {
            return _dataContext.Donor_Campaign.Where(e => e.CampaignId == Campaignid).FirstOrDefault();
        }

        //public ICollection<UsersRegistration> GetAllDonors(int companyid)
        //{
        //  Company company=_d
        //}
    }
}
