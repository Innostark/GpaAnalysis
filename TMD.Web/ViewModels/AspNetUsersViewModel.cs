using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMD.Models.DomainModels;
using TMD.Web.Models;

namespace TMD.Web.ViewModels
{
    public class AspNetUsersViewModel
    {
        public AspNetUserModel AspNetUserModel { get; set; }
        public List<AspNetRole> Roles { get; set; }
    }
}