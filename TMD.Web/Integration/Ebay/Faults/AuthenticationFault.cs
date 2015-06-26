namespace TMD.Web.Integration.Ebay.Faults
{
    /// <summary>
    /// Custom class for authentication faults
    /// All faults start from code 100
    /// </summary>
    public class AuthenticationFault: BaseCustomFault
    {
        public const string FaultCodeCredentialsCouldNotBeValidated = "100";
        public const string FaultMessageCredentialsCouldNotBeValidated = "The user name or password is incorrect, invalid access is not permitted.";

        public const string FaultCodeEmailNotConfirmed = "101";
        public const string FaultMessageEmailNotConfirmed = "The user has not confirmed the account via email.";

        public const string FaultCodeUserIsLocked = "102";
        public const string FaultMessageUserIsLocked = "The user is locked.";

        public const string FaultCodeInvalidUserToken = "103";
        public const string FaultMessageInvalidUserToken = "The user token in not valid.";
    }
}