using System.Collections.Generic;
using TMD.Models.MenuModels;

namespace TMD.Models.DomainModels
{
    public partial class AspNetRole
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspNetUser> AspNetUsers{ get; set; }
        public virtual ICollection<MenuRight> MenuRights { get; set; }
    }
}
