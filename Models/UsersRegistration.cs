﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace WebAppTutorial.Models
{
    public class UsersRegistration
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
