using System.Collections.Generic;

namespace GPAA.Models.MenuModels
{
    /// <summary>
    /// MainMenu class for dynamic menu population from database
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Menu Id
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// Menu Key
        /// </summary>
        public int MenuKey { get; set; }
        /// <summary>
        /// Menu Title
        /// </summary>
        public string MenuTitle { get; set; }
        /// <summary>
        /// Sort Order
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// Menu Target Url
        /// </summary>
        public string MenuTargetController { get; set; }
        /// <summary>
        /// Menu Image Path
        /// </summary>
        public string MenuImagePath { get; set; }
        public string MenuItemClass { get; set; }
        /// <summary>
        /// Menu Function
        /// </summary>
        public string MenuFunction { get; set; }
        /// <summary>
        /// Permission Key
        /// </summary>
        public string PermissionKey { get; set; }
        /// <summary>
        /// Menu Root Item Check
        /// </summary>
        public bool IsRootItem { get; set; }
        /// <summary>
        /// Is Menu Item Check: for Buttons in Any Menu
        /// </summary>
        public bool IsMenuItem { get; set; }

        public int? ParentItem_Menu { get; set; }
        /// <summary>
        /// Menu Parent Item
        /// </summary>
        public virtual Menu ParentItem { get; set; }
        public virtual ICollection<MenuRight> MenuRights { get; set; }
    }
}
