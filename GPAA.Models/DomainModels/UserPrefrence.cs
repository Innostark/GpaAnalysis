namespace GPAA.Models.DomainModels
{
    public class UserPrefrence
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Culture { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
