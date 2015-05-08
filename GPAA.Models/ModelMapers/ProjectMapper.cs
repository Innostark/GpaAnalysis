using System;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class ProjectMapper
    {
        public static ResponseModels.Project CreateForDashboard(this Project source)
        {
            ResponseModels.Project project=new ResponseModels.Project();
            project.ProjectId = source.ProjectId;
            project.NameA = source.NameA;
            project.NameAShort = source.NameA.Length > 15 ? source.NameA.Substring(0, 15) + "..." : source.NameA;
            project.NameE = source.NameE;
            project.NameEShort = source.NameE.Length > 15 ? source.NameE.Substring(0, 15) + "..." : source.NameE;
            project.ProgressTotal = source.ProjectTasks.Any()
                ? source.ProjectTasks.Sum(projectTask => (projectTask.TaskProgress??0))
                : 0;
            return project;
        }
    }
}
