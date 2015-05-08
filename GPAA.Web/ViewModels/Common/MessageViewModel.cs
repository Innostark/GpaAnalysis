
namespace GPAA.Web.ViewModels.Common
{
    public class MessageViewModel
    {
        public bool IsSaved { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsError { get; set; }
        public bool IsInfo { get; set; }

        private string _message = "";
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }
    }
}
