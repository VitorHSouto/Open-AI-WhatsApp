using FluentMigrator;

namespace VHS_Tarefas.Migrations
{
    [Migration(202304011522)]
    public class Migration_202304011522 : FluentMigrator.Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Delete.Column("role").FromTable("message");

            Alter.Table("message")
                .AddColumn("isuser").AsBoolean().Nullable().WithDefaultValue(false)
                .AddColumn("isbot").AsBoolean().Nullable().WithDefaultValue(false);
        }
    }
}
