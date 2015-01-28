using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Data.Repos
{
    public interface IProjectMemberRepository: IRepository<ProjectMember>
    {
    }

    public class ProjectMemberRepository : BaseRepo<ProjectMember>, IProjectMemberRepository
    {
    }
}