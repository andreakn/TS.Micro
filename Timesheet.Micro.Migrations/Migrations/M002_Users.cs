using System.Data;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(2)]
    public class M002_Users:BaseMigration
    {
        public override void Up()
        {
            Database.AddTable(users,
                new Column("Id",DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("Username",DbType.String,255,ColumnProperty.NotNull),
                new Column("PasswordHash", DbType.String, 255, ColumnProperty.NotNull),
                new Column("PasswordSalt", DbType.String, 255, ColumnProperty.NotNull)
                );
            InsertStaticData();
        }

        private void InsertStaticData()
        {
            Database.BeginTransaction();
            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([Username],[PasswordHash],[PasswordSalt]) VALUES ('admin','O286Hzp4RJsmJl68xRUl48v7zWFMSJu8MjyvCDWt8SCYIGNo579dxuXjF4Tz3cGponz3OehYFTEOUQF8pPq9ug==','JCoLDEcAd33rfKz9DJ3NPM0AP/quMwHa+jajdNGHmIu9bCLiDb/SaBsA0OmBuW2xHq3357n0pvjRwhDGbvPwqQ==')", users));
            Database.Commit();
        }

        public override void Down()
        {
            Database.RemoveTable(users);
        }
    }
}
