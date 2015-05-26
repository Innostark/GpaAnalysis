using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMD.Web.Models.Common
{
    public class BoolDropDownModel
    {
        public List<BoolDropDownModel> oList { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public BoolDropDownModel()
        {
           
        }

        public void InitilizeList()
        {
             oList = new List<BoolDropDownModel>();

            oList.Add(new BoolDropDownModel
            {
                Id = 0,
                Name = "ALL"
            });
            oList.Add(new BoolDropDownModel
            {
                Id = 1,
                Name = "Yes"
            });
            oList.Add(new BoolDropDownModel
            {
                Id = 2,
                Name = "No"
            });
        }
    }
}