using FluentMigrator;

namespace UserManager.DataAccess.Migrations.Migrations
{
    [Migration(20210114223404)]
    public class AddCreationDateToUser : Migration
    {
        public override void Up()
        {
            Alter.Table("User")
                .AddColumn("CreationDate").AsDateTimeOffset().Nullable();

            Update.Table("User").Set(new { CreationDate = RawSql.Insert("SYSDATETIMEOFFSET()") }).AllRows();

            Alter.Table("User")
                .AlterColumn("CreationDate").AsDateTimeOffset().NotNullable();
        }

        public override void Down()
        {
            Delete.Column("CreationDate")
                .FromTable("User");
        }
    }
}
