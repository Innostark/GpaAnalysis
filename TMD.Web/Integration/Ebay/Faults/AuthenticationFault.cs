namespace TMD.Web.Integration.Ebay.Faults
{
    public class AuthenticationFault: BaseCustomFault
    {
        public const string FaultCodeCredentialsCouldNotBeValidated = "101";
        public const string FaultMessageCredentialsCouldNotBeValidated = "The user name or password is incorrect, invalid access is not permitted.";

        public const string FaultCodeEmailNotConfirmed = "101";
        public const string FaultMessageEmailNotConfirmed = "The user has not confirmed the account via email.";

        public const string FaultCodeUserIsLocked = "102";
        public const string FaultMessageUserIsLocked = "The user is locked.";
    }
}