using FluentMigrator;
using UserManager.BusinessLogic.Model;

namespace UserManager.DataAccess.Migrations.Migrations
{
    [Migration(20201113194828)]
    public class AddRoleToUser : Migration
    {
        public override void Up()
        {
            Alter.Table("User")
                .AddColumn("Role").AsInt16().WithDefaultValue(Role.Basic.Id);
        }

        public override void Down()
        {
            Delete.Column("Role")
                .FromTable("User");
        }
    }
}
