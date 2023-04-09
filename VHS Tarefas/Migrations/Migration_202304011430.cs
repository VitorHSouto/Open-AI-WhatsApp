using FluentMigrator;

namespace VHS_Tarefas.Migrations
{
    [Migration(202304011430)]
    public class Migration_202304011430 : FluentMigrator.Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Execute.Sql("DELETE FROM message");
            Execute.Sql("DELETE FROM chatcontext");

            Create.Table("contactchannel")
                .WithColumn("id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("createdat").AsDateTime().NotNullable()
                .WithColumn("active").AsBoolean().NotNullable()
                .WithColumn("phonenumber").AsString(20).NotNullable()
                .WithColumn("name").AsString(20).Nullable();

            Alter.Table("chatcontext")
                .AddColumn("contactchannelid").AsGuid().NotNullable().ForeignKey("contactchannel", "id");
        }
    }
}
