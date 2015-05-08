using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class ProjectTaskMapper
    {
        public static ProjectTask CreateFromClientToServer(this ProjectTask source, ProjectTask projectTask)
        {
            source.TaskId = projectTask.TaskId;
            source.ProjectId = projectTask.ProjectId;
            source.CustomerId = projectTask.CustomerId;
            source.TaskNameE = projectTask.TaskNameE;
            source.TaskNameA = projectTask.TaskNameA;
            source.DescriptionE = projectTask.DescriptionE;
            source.DescriptionA = projectTask.DescriptionA;
            source.StartDate = projectTask.StartDate;
            source.EndDate = projectTask.EndDate;
            source.TotalCost = projectTask.TotalCost;
            source.TotalWeight = projectTask.TotalWeight;
            source.TaskProgress = projectTask.TaskProgress;
            source.NotesE = projectTask.NotesE;
            source.NotesA = projectTask.NotesA;
            source.RecCreatedBy = projectTask.RecCreatedBy;
            source.RecCreatedDt = projectTask.RecCreatedDt;
            source.RecLastUpdatedBy = projectTask.RecLastUpdatedBy;
            source.RecLastUpdatedDt = projectTask.RecLastUpdatedDt;
            return source;
        }
        public static TaskEmployee CreateFromServerToClient(this TaskEmployee source)
        {
            return new TaskEmployee
            {
                TaskEmployeeId = source.TaskEmployeeId,
                TaskId = source.TaskId,
                EmployeeId = source.EmployeeId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }
        public static ResponseModels.ProjectTaskResponse CreateForDashboard(this ProjectTask source)
        {
            return new ResponseModels.ProjectTaskResponse
            {
                TaskId = source.TaskId,
                TaskNameE = source.TaskNameE,
                TaskNameEShort = source.TaskNameE.Length > 15 ? source.TaskNameE.Substring(0, 15) + "..." : source.TaskNameE,
                TaskNameA = source.TaskNameA,
                TaskNameAShort = source.TaskNameA.Length > 15 ? source.TaskNameA.Substring(0, 15) + "..." : source.TaskNameA,
                TaskProgress = source.TaskProgress??0
            };
        }
    }
}
