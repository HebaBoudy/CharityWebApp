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

        public List<Donor_Campaign> Get(string title)
        {
            return _dataContext.Donor_Campaign.Where(e => e.Title == title).ToList();
        }

        public bool Insert(Donor_Campaign row)
        {
            _dataContext.Donor_Campaign.Add(row);
            var added = _dataContext.SaveChanges();
            return (added > 0) ? true : false;

        }

        //public ICollection<UsersRegistration> GetAllDonors(int companyid)
        //{
        //  Company company=_d
        //}
    }
}
