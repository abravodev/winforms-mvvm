using FluentMigrator;

namespace UserManager.DataAccess.Migrations.Migrations
{
    [Migration(20201024202825)]
    public class AddUserTable : Migration
    {
        public override void Up()
        {
            Create.Table("User")
                .WithIdColumn()
                .WithColumn("FirstName").AsString(50).NotNullable()
                .WithColumn("LastName").AsString(100).NotNullable()
                .WithColumn("Email").AsString(50).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("User");
        }
    }
}
