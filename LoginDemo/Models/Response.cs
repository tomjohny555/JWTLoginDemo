using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginDemo.Models
{
    public class Response
    {
        public ResponseStatus responseStatus { get; set; }
        public dynamic responseContent { get; set; }
    }
}