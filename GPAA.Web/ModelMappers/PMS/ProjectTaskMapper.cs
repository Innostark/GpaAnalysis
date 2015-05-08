using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.PMS
{
    public static class ProjectTaskMapper
    {
        public static Models.ProjectTask CreateFromServerToClient(this ProjectTask source)
        {
            Models.ProjectTask projectTask = new Models.ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            projectTask.DescriptionE = source.DescriptionE;
            projectTask.DescriptionA = source.DescriptionA;
            projectTask.StartDate = Convert.ToDateTime(source.StartDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            projectTask.EndDate = Convert.ToDateTime(source.EndDate).ToString("dd/MM/yyyy", new CultureInfo("en"));
            projectTask.TotalCost = source.TotalCost;
            projectTask.TotalWeight = source.TotalWeight;
            projectTask.TaskProgress = source.TaskProgress ?? 0;
            projectTask.NotesE = source.NotesE;
            projectTask.NotesA = source.NotesA;
            projectTask.RecCreatedBy = source.RecCreatedBy;
            projectTask.RecCreatedDt = source.RecCreatedDt;
            projectTask.RecLastUpdatedBy = source.RecLastUpdatedBy;
            projectTask.RecLastUpdatedDt = source.RecLastUpdatedDt;
            if (source.Project != null)
            {
                projectTask.ProjectNameE = source.Project.NameE;
                projectTask.ProjectNameA = source.Project.NameA;
            }
            projectTask.RequisitTasks = source.PreRequisitTask.Select(x => x.CreateFromServerToClientChild()).ToList();
            if (source.TaskEmployees != null)
            {
                projectTask.TaskEmployees = source.TaskEmployees.Select(x => x.CreateFromServerToClient()).ToList();
            }
            if (source.PreRequisitTask.Count > 0)
            {
                foreach (var preRequisitTask in source.PreRequisitTask)
                {
                    projectTask.PreReqTasks = preRequisitTask.TaskNameE + " - " + projectTask.PreReqTasks;
                }
                projectTask.PreReqTasks = projectTask.PreReqTasks.Substring(0, projectTask.PreReqTasks.Length - 3);
            }
            if (source.TaskEmployees != null && source.TaskEmployees.Count > 0)
            {
                foreach (var employee in source.TaskEmployees)
                {
                    projectTask.EmployeesAssigned = employee.Employee.EmployeeNameE + " - " + projectTask.EmployeesAssigned;
                }
                projectTask.EmployeesAssigned = projectTask.EmployeesAssigned.Substring(0, projectTask.EmployeesAssigned.Length - 3);
            }
            return projectTask;
        }

        public static ProjectTask CreateFromClientToServer(this Models.ProjectTask source)
        {
            ProjectTask projectTask = new ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            projectTask.DescriptionE = source.DescriptionE;
            projectTask.DescriptionA = source.DescriptionA;
            projectTask.StartDate = source.StartDate == null ? (DateTime?)null : DateTime.ParseExact(source.StartDate, "dd/MM/yyyy", new CultureInfo("en"));
            projectTask.EndDate = source.EndDate == null ? (DateTime?)null : DateTime.ParseExact(source.EndDate, "dd/MM/yyyy", new CultureInfo("en"));
            projectTask.TotalCost = source.TotalCost;
            projectTask.TotalWeight = source.TotalWeight;
            projectTask.TaskProgress = source.TaskProgress;
            projectTask.NotesE = source.NotesE;
            projectTask.NotesA = source.NotesA;
            projectTask.RecCreatedBy = source.RecCreatedBy;
            projectTask.RecCreatedDt = source.RecCreatedDt;
            projectTask.RecLastUpdatedBy = source.RecLastUpdatedBy;
            projectTask.RecLastUpdatedDt = source.RecLastUpdatedDt;
            projectTask.PreRequisitTask = source.RequisitTasks.Select(x=>x.CreateFromClientToServer()).ToList();
            return projectTask;
        }

        public static Models.ProjectTask CreateFromServerToClientChild(this ProjectTask source)
        {
            Models.ProjectTask projectTask = new Models.ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            return projectTask;
        }
        public static Models.ProjectTask CreateFromServerToClientForEmployee(this ProjectTask source)
        {
            Models.ProjectTask projectTask = new Models.ProjectTask();
            projectTask.TaskId = source.TaskId;
            projectTask.ProjectId = source.ProjectId;
            projectTask.CustomerId = source.CustomerId;
            projectTask.ProjectNameE = source.Project.NameE;
            projectTask.ProjectNameA = source.Project.NameA;
            projectTask.TaskNameE = source.TaskNameE;
            projectTask.TaskNameA = source.TaskNameA;
            return projectTask;
        }
    }
}