using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginDemo.Models
{
    public class ResponseStatus
    {
        public string status { get; set; }
        public string Description { get; set; }
        public int statusCode { get; set; }
    }
}