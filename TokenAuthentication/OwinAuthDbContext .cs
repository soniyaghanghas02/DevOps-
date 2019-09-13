using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokenAuthentication
{
    public class EmployeeDBContext : IdentityDbContext
    {
        public EmployeeDBContext()
            : base("EmployeeDBContext")
        {
        }
    }
}