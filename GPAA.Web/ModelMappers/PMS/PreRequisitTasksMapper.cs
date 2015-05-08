using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.PMS
{
    public static class PreRequisitTasksMapper
    {
        public static Models.PreRequisitTask CreateFromServerToClient(this PreRequisitTask source)
        {
            Models.PreRequisitTask caseType = new Models.PreRequisitTask
            {
                TaskId = source.TaskId,
                PreReqTask = source.PreReqTaskId,
            };
            return caseType;
        }
    }
}