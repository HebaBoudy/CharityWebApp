using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTutorial.Models
{
    public class Login
    {
        //  public int ID { get; set; }
        [Key]
        public string UserName { get; set; }   
        public string Password { get; set; }
       
        public virtual UsersRegistration ? UserRegistration { get; set; }
      
        public virtual Company ? Company { get; set; } 
        public bool Type { get; set; } //0: company
                                       //1: Donors 
    }
}
