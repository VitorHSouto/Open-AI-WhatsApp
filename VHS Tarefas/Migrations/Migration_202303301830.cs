using FluentMigrator;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VHS_Tarefas.Migrations
{
    [Migration(202303301830)]
    public class Migration_202303301830 : FluentMigrator.Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Create.Table("channel")
                .WithColumn("id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("createdat").AsDateTime().NotNullable()
                .WithColumn("active").AsBoolean().NotNullable();

            Create.Table("chatcontext")
                .WithColumn("id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("channelid").AsGuid().NotNullable().ForeignKey("channel", "id")
                .WithColumn("createdat").AsDateTime().NotNullable()
                .WithColumn("updatedat").AsDateTime().NotNullable()
                .WithColumn("active").AsBoolean().NotNullable();

            Create.Table("message")
                .WithColumn("id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("createdat").AsDateTime().NotNullable()
                .WithColumn("active").AsBoolean().NotNullable()
                .WithColumn("chatcontextid").AsGuid().NotNullable().ForeignKey("chatcontext", "id")
                .WithColumn("role").AsString(20).NotNullable();
        }
    }
}
