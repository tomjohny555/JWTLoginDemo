using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginDemo.Models
{
    public class Signin
    {
        [Required(ErrorMessage = "Enter valid username")]
        public string userName { get; set; }
        [Required(ErrorMessage = "Enter valid password")]
        public string password { get; set; }

        [Required(ErrorMessage = "Enter valid name")]
        public string employeeName { get; set; }
        [Required(ErrorMessage = "Enter valid email")]
        public string email { get; set; }

    }
}