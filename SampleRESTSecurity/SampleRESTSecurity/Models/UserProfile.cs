using System;
using System.Collections.Generic;
using System.Text;

namespace SampleRESTSecurity.Models
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsAdmin { get; set; }
        public string Password { get; set; }
    }
}
