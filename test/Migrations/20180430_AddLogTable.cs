
using FluentMigrator;

namespace test.Migrations
{
    [Migration(20180430121800)]
    public class AddLogTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Log");
        }

        public override void Up()
        {
            Create.Table("Log")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("Text").AsString();
        }
    }
}