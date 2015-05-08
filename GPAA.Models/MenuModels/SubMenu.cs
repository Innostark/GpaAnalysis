using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.MenuModels
{
    /// <summary>
    /// SubMenu class for dynamic menu population from database
    /// </summary>
    public class SubMenu
    {
        /// <summary>
        /// Sub Menu Id
        /// </summary>
        public int SubMenuId { get; set; }
        /// <summary>
        /// Sub Menu Key
        /// </summary>
        public int SubMenuKey {get; set;}
        /// <summary>
        /// Sub Menu Title
        /// </summary>
        public string SubMenuTitle {get; set;}
        /// <summary>
        /// Sort Order
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// Sub Menu Target Url
        /// </summary>
        public string SubMenuTargetUrl { get; set; }
        /// <summary>
        /// Sub Menu Image Path
        /// </summary>
        public String SubMenuImagePath { get; set; }
        /// <summary>
        /// Main Menu 
        /// </summary>
        public virtual Menu MainMenu { get; set; }
    }
}
