using WebAppTutorial.Models;
namespace WebAppTutorial.Interfaces
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();
        Company GetCompanyById(int id);
        Company GetCompanyByName(string name);
        ICollection<Company> GetCompaniesByType(string T);
        bool CompanyExists(int id);
 
    }
}
