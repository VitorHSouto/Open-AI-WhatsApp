using FluentMigrator;

namespace VHS_Tarefas.Migrations
{
    [Migration(202303302100)]
    public class Migration_202303302100 : FluentMigrator.Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Alter.Table("chatcontext")
                .AddColumn("isclosed").AsBoolean().WithDefaultValue(false).Nullable();

            Alter.Table("channel")
                .AddColumn("phonenumber").AsString(20).NotNullable();

            Alter.Table("message")
                .AddColumn("text").AsString(4550).Nullable()
                .AddColumn("mediaurl").AsString(1024).Nullable();
        }
    }
}
