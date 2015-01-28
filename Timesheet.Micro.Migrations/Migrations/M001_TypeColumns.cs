using System.Data;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(1)]
    public class M001_TypeColumns: BaseMigration
    {
        public override void Up()
        {
            var id_name_cols = new Column[2];
            id_name_cols[0] = new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity);
            id_name_cols[1] = new Column("Name", DbType.String, 255);

            Database.AddTable(addressTypes, id_name_cols);
            Database.AddTable(projTypes, id_name_cols);

            id_name_cols[0].ColumnProperty = ColumnProperty.PrimaryKey;
            Database.AddTable(roles,id_name_cols);

            InsertStaticData();
        }

        private void InsertStaticData()
        {
            Database.BeginTransaction();
            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Id],[Name]) VALUES (1,'User')", roles));
            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Id],[Name]) VALUES (2,'Staffer')", roles));
            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Id],[Name]) VALUES (4,'KeyAccountManager')", roles));
            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Id],[Name]) VALUES (8,'ProjectManager')", roles));
            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Id],[Name]) VALUES (16,'Invoicer')", roles));
            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Id],[Name]) VALUES (32,'HumanResourcesManager')", roles));
            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Id],[Name]) VALUES (64,'ReportAnalyst')", roles));
            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Id],[Name]) VALUES (128,'Administrator')", roles));

            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Name]) VALUES ('Besøksadresse')", addressTypes));
            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Name]) VALUES ('Postadresse')", addressTypes));

            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Name]) VALUES ('Internprosjekt')", projTypes));
            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Name]) VALUES ('Løpende timer')", projTypes));
            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Name]) VALUES ('Målpris')", projTypes));
            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Name]) VALUES ('Fastpris')", projTypes));

            Database.Commit();
        }

        public override void Down()
        {
            Database.RemoveTable(addressTypes);
            Database.RemoveTable(projTypes);
            Database.RemoveTable(roles);
        }
    }
}
