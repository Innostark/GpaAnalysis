namespace GPAA.Web.Models
{
    /// <summary>
    /// Applicant model used for Job Applicants List
    /// </summary>
    public class ApplicantModel
    {
        /// <summary>
        /// Job Applicant Id
        /// </summary>
        public long JobApplicantId { get; set; }
        /// <summary>
        /// Job Applicant Name
        /// </summary>
        public string ApplicantName { get; set; }
        /// <summary>
        /// Job Applicant Email
        /// </summary>
        public string ApplicantEmail { get; set; }
        /// <summary>
        /// Job Applicant Mobile
        /// </summary>
        public string ApplicantMobile { get; set; }
        /// <summary>
        /// Job Offered (Job Title Name)
        /// </summary>
        public string JobOffered { get; set; }
        /// <summary>
        /// Job Offering Department
        /// </summary>
        public string DepartmentName { get; set; }
    }
}