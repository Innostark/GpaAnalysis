using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMD.Models.DomainModels;
using TMD.Web.Models;

namespace TMD.Web.ModelMappers
{
    public static class AspNetUserMapper
    {
        public static AspNetUserModel CreateFrom(this AspNetUser source)
        {
             AspNetUserModel oModel = new AspNetUserModel
            {

                Address = source.Address,
                DateOfBirth = source.DateOfBirth,
                Email = source.Email,
                FirstName = "First",
                LastName = "Last",
                Id = source.Id,
                ImageName = source.ImageName,
                Telephone = source.Telephone,
                UserName = source.UserName
                //RoleName = source.AspNetRoles.First().Name

            };
            return oModel;

        }
    }
}