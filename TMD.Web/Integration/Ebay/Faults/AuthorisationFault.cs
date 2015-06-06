namespace TMD.Web.Integration.Ebay.Faults
{
    public class AuthorisationFault: BaseCustomFault
    {
        public const string FaultCodeUserIsNotAdmin = "200";
        public const string FaultMessageUserIsNotAdmin = "The user is not administrator.";
    }
}