using FluentMigrator;

namespace BPDotNet.Scripts.DDL.Tables
{
    [Migration(202108151112, "Create table users")]
    public class T202108151112CreateUsers : Migration
    {
        private const string TableName = "users";
        
        public override void Down()
        {
            Delete.Table(TableName);
        }
        
        public override void Up()
        {
            var table = Create.Table(TableName).WithDescription("Table of users");

            table.WithColumn("id")
                .AsInt32()
                .PrimaryKey()
                .Identity()
                .NotNullable();
            
            table.WithColumn("name")
                .AsString(100)
                .NotNullable();

            table.WithColumn("email")
                .AsString(50)
                .Unique()
                .NotNullable();
            
            table.WithColumn("password")
                .AsAnsiString()
                .NotNullable();
            
            table.WithColumn("created_at")
                .AsDateTime();
            
            table.WithColumn("updated_at")
                .AsDateTime();
            
            table.WithColumn("is_deleted")
                .AsBoolean();
        }
    }
}