using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTutorial.Models
{
    public class Campaign
    {
        public Campaign()
        {
            Active = true;
        }
       public int ID { get; set; }
       public int CompanyID { get; set; }
       public  virtual Company Company { get; set; }
       public string Title { get; set; }
       public string  Description { get; set; }
       public int RequiredAmount { get; set; }
       public int CollectedAmount { get; set; }
       public bool Active { get; set; } 
    }
}
