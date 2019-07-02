using LoginDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginDemo.Interface
{
    interface IEmployee
    {
        int RegisterCustomer(Signin objLog);
        string GenerateToken(Signin objLog, int LoginId, string base64Secret);

    }
}
