namespace TMD.Web.Integration.Ebay.Faults
{
    /// <summary>
    /// Custom class for authorisation faults
    /// All faults start from code 200
    /// </summary>
    public class AuthorisationFault: BaseCustomFault
    {
        public const string FaultCodeUserIsNotAdmin = "200";
        public const string FaultMessageUserIsNotAdmin = "The user is not administrator.";
    }
}