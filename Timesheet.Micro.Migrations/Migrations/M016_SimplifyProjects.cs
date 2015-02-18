using System;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{

    [Migration(16)]
    public class M016_SimplifyProjects : BaseMigration
    {
        public override void Up()
        {
            Database.RenameColumn("Projects", "ProjectTypeId","ProjectType");

            Database.RemoveColumn("Projects", "ModifiedBy");
            Database.RemoveColumn("Projects", "CreatedBy");
            Database.RemoveColumn("Projects", "ModifiedDate");
            Database.RemoveColumn("Projects", "CreatedDate");
            Database.RemoveColumn("Projects", "ProjectCode");
            Database.RemoveColumn("Projects", "ExternalProjectCode");
            Database.RemoveColumn("Projects", "EstimateDuration");
            Database.RemoveColumn("Projects", "CompletionDate");
            Database.RemoveColumn("Projects", "ProjectManagerId");
        }

        public override void Down()
        {
            //aint no backing down from this one
            throw new NotSupportedException();
        }
    }
}