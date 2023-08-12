using WebAppTutorial.Models;
namespace WebAppTutorial.Interfaces
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();
        Company GetCompanyById(int id);
        Company GetCompanyByUserName(string username);
        Company GetCompanyByName(string name);
        ICollection<Company> GetCompaniesByType(string T);
        bool CompanyExists(int id);
        bool CompanyExists(string  userName);
        bool AddCompany(Company company);
        bool UpdateCompany(Company company);
        bool DeleteCompany(Company company); 


    }
}
