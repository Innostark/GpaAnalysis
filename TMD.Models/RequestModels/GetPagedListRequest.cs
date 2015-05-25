namespace TMD.Models.RequestModels
{
    public class GetPagedListRequest
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GetPagedListRequest()
        {
            IsAsc = true;
            SortBy = 1;
            PageNo = 1;
            PageSize = 10;
        }

        /// <summary>
        /// Search String
        /// </summary>
        public string SearchString { get; set; }

        /// <summary>
        /// user select page size or number of records to be displayed
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// PageNo
        /// </summary>
        private int _pageNo;

        /// <summary>
        /// Page No
        /// </summary>
        public int PageNo
        {
            get
            {
                return _pageNo;
            }
            set
            {
                _pageNo = value == 0 ? 1 : value;
            }
        }

        //sort order
        public bool IsAsc { get; set; }

        // delete item id
        public int Id { get; set; }

        /// <summary>
        /// Order By Name
        /// </summary>

        public short SortBy { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }


    }
}
