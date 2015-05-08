using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels.EmployeeResponseModel;

namespace EPMS.Models.ModelMapers
{
    public static class EmployeeMapper
    {
        public static void UpdateTo(this Employee source, Employee target)
        {
            target.EmployeeId = source.EmployeeId;
            target.EmployeeNameE = source.EmployeeNameE;
            target.EmployeeNameA = source.EmployeeNameA;
            target.EmployeeImagePath = source.EmployeeImagePath;
            target.JobTitleId = source.JobTitleId;
            target.EmployeeJobId = source.EmployeeJobId;
            target.EmployeeMobileNum = source.EmployeeMobileNum;
            target.EmployeeLandlineNum = source.EmployeeLandlineNum;
            target.MaritalStatus = source.MaritalStatus;
            target.EmployeeDOB = source.EmployeeDOB;
            target.EmployeeNationality = source.EmployeeNationality;
            target.EmployeeIqama = source.EmployeeIqama;
            target.EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt;
            target.EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt;
            target.EmployeePassportNum = source.EmployeePassportNum;
            target.EmployeePassportExpiryDt = source.EmployeePassportExpiryDt;
            target.EmployeeDetailsE = source.EmployeeDetailsE;
            target.EmployeeDetailsA = source.EmployeeDetailsA;
            target.RecCreatedBy = source.RecCreatedBy;
            target.RecCreatedDt = source.RecCreatedDt;
            target.RecLastUpdatedBy = source.RecLastUpdatedBy;
            target.RecLastUpdatedDt = source.RecLastUpdatedDt;
            target.Email = source.Email;
        }
        public static Employee ServerToServer(this Employee source)
        {
            return new Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.EmployeeNameE,
                EmployeeNameA = source.EmployeeNameA,
                EmployeeImagePath = source.EmployeeImagePath,
                JobTitleId = source.JobTitleId,
                EmployeeJobId = source.EmployeeJobId,
                EmployeeMobileNum = source.EmployeeMobileNum,
                EmployeeLandlineNum = source.EmployeeLandlineNum,
                MaritalStatus = source.MaritalStatus,
                EmployeeDOB = source.EmployeeDOB,
                EmployeeNationality = source.EmployeeNationality,
                EmployeeIqama = source.EmployeeIqama,
                EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt,
                EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt,
                EmployeePassportNum = source.EmployeePassportNum,
                EmployeePassportExpiryDt = source.EmployeePassportExpiryDt,
                EmployeeDetailsE = source.EmployeeDetailsE,
                EmployeeDetailsA = source.EmployeeDetailsA,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                Email = source.Email
            };
        }
    }
}
