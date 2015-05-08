namespace GPAA.Models.DomainModels
{
    public class AspNetUserLogin
    {
        public int AspNetUserLoginsId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
