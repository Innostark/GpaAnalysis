namespace GPAA.Models.RequestModels
{
    public class GetPagedListRequest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GetPagedListRequest()
        {
            sSortDir_0 = "asc";
            iSortCol_0 = 1;
            iDisplayStart = 0;
            iDisplayLength = 10;
        }

        //user select page size or number of records to be displayed
        public int iDisplayLength { get; set; }

        public string SearchString { get; set; }
        public string sEcho { get; set; }
        /// <summary>
        /// PageNo
        /// </summary>
        private int _pageNo;

        /// <summary>
        /// Page No
        /// </summary>
        public int iDisplayStart
        {
            get
            {
                return _pageNo;
            }
            set
            {
                _pageNo = value == 0 ? 0 : value;
            }
        }

        //sort order
        public string sSortDir_0 { get; set; }

        // delete item id
        public int Id { get; set; }

        /// <summary>
        /// Order By Name
        /// </summary>
        private int _SortCol;
        public int iSortCol_0
        {
            get
            {
                return _SortCol;
            }
            set
            {
                _SortCol = value == 0 ? 1 : value;
            }
        }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }

    }
}
