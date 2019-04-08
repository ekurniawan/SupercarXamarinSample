using System;
using System.Collections.Generic;
using System.Text;

namespace SampleRESTSecurity.Models
{
    public class VoteInputView
    {
        public int userId { get; set; }
        public int supercarId { get; set; }
        public string comments { get; set; }
    }
}
