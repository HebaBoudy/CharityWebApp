using WebAppTutorial.Data;
using WebAppTutorial.Interfaces;
using WebAppTutorial.Models;

namespace WebAppTutorial.Repos
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly DataContext _context;

        public CompanyRepository(DataContext Context)
        {
            _context = Context;
        }

      

        bool ICompanyRepository.CompanyExists(int id)
        {
            return _context.Company.Any(e => e.CompanyId==id);
        }

        ICollection<Company> ICompanyRepository.GetCompanies()
        {
            return _context.Company.OrderBy(e => e.CompanyId).ToList();
        }

        ICollection<Company> ICompanyRepository.GetCompaniesByType(string T)
        {
            return _context.Company.Where(p => p.type == T).ToList();
        }

        Company ICompanyRepository.GetCompanyById(int id)
        {
            return _context.Company.Where(p => p.CompanyId == id).FirstOrDefault();
        }

        Company ICompanyRepository.GetCompanyByName(string name)
        {
            return _context.Company.Where(p => p.Name == name).FirstOrDefault();
        }
    }
}
