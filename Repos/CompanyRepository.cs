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

        public bool AddCompany(Company company)
        {

            var saved= _context.Add(company);
            return (_context.SaveChanges() > 0) ? true:false;

        }

        public bool CompanyExists(string userName)
        {
          return _context.Company.Any(e=>e.UserName==userName); 
        }

        public bool DeleteCompany(Company company)
        {
            _context.Company.Remove(company);

            var saved = _context.SaveChanges();
            return (saved > 0) ? true : false;
        }

        public Company GetCompanyByUserName(string username)
        {
            return _context.Company.Where(e => e.UserName == username).FirstOrDefault();
        }

        public bool UpdateCompany(Company company)
        {
           _context.Company.Update(company);
            var saved =_context.SaveChanges();
            return (saved > 0) ? true : false;
        }

        bool ICompanyRepository.CompanyExists(int id)
        {
            return _context.Company.Any(e => e.ID==id);
        }

        ICollection<Company> ICompanyRepository.GetCompanies()
        {
            return _context.Company.ToList();
        }

        ICollection<Company> ICompanyRepository.GetCompaniesByType(string T)
        {
            return _context.Company.Where(p => p.Type == T).ToList();
        }

        Company ICompanyRepository.GetCompanyById(int id)
        {
            return _context.Company.Where(p => p.ID == id).FirstOrDefault();
        }

        Company ICompanyRepository.GetCompanyByName(string name)
        {
            return _context.Company.Where(p => p.Name == name).FirstOrDefault();
        }
    }
}
