using TMD.Models.DomainModels;

namespace TMD.Models.MenuModels
{
    /// <summary>
    /// MenuRights class for menu assoication with role
    /// </summary>
    public class MenuRight
    {
        /// <summary>
        /// Menu Right Id
        /// </summary>
        public int MenuRightId { get; set; }
        public int Menu_MenuId { get; set; }
        public string Role_Id { get; set; }
        /// <summary>
        /// Menu
        /// </summary>
        public virtual Menu Menu { get; set; }
        /// <summary>
        /// Role
        /// </summary>
        public virtual AspNetRole AspNetRole { get; set; }
    }
}