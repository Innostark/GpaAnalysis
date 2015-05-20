using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TMD.Web.Models
{
    public class AspNetUserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string ImageName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Qualification { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public string LockoutEnabledString { get; set; }
        public string IsConfirmedString { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string RegisterPayPalTxnID { get; set; }
        public DateTime  ? RegisterPayPalDate { get; set; }
        public double ? PayPalAmount { get; set; }
        public double ? PayPalAmountAfterDeduct { get; set; }

        public string PayPalMisc { get; set; }
        public int ? Package { get; set; }

        public string RoleName { get; set; }
        public string RoleId { get; set; }
    }
}
