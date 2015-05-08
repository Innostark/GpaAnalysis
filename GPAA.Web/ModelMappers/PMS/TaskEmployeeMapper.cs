using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.PMS
{
    public static class TaskEmployeeMapper
    {
        public static Models.TaskEmployee CreateFromServerToClient(this TaskEmployee source)
        {
            return new Models.TaskEmployee
            {
                TaskEmployeeId = source.TaskEmployeeId,
                TaskId = source.TaskId,
                EmployeeId = source.EmployeeId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                ProjectTask = source.ProjectTask.CreateFromServerToClientForEmployee()
            };
        }
    }
}