using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GPAA.Models.DomainModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GPAA.Models.IdentityModels.ViewModels
{
    public class RoleViewModel : IdentityDbContext
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }

// ReSharper disable once CSharpWarnings::CS0108
        public IEnumerable<AspNetRole> Roles { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}