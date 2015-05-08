using System.Collections.Generic;
using TMD.Models.DomainModels;

namespace TMD.Models.MenuModels
{
    public class UserMenuResponse
    {
        public IList<MenuRight> MenuRights { get; set; }

        public IList<Menu> Menus { get; set; }

        public IList<AspNetRole> Roles { get; set; }
    }
}
