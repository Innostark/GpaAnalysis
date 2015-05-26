using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMD.Models.RequestModels;
using TMD.Web.Models;

namespace TMD.Web.ViewModels
{


    public class EbayItemViewModel
    {

        
        
        public StagingEbayItemRequest SearchRequest { get; set; }
        
        public List<StagingEbayItemModel> data { get; set; }

        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int recordsTotal;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int recordsFiltered;

        public EbayItemViewModel()
        {
           
        }
    }
}