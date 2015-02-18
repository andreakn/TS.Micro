using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Data.Repos
{
    public interface IProjectMemberRepository: IRepository<ProjectMember>
    {
        IEnumerable<ProjectMember> GetForProject(int projectId);
    }

    public class ProjectMemberRepository : BaseRepo<ProjectMember>, IProjectMemberRepository
    {
        public IEnumerable<ProjectMember> GetForProject(int projectId)
        {
            using (var conn = GetConn())
            {
                return conn.Query<ProjectMember>("select * from ProjectMembers where ProjectId = @projectId", new { projectId});
            }

        }
    }
}