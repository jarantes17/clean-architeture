using FluentMigrator;

namespace BPDotNet.Scripts.DDL.Tables
{
    [Migration(202108151112, "Create table users")]
    public class T202108151112CreateUsers : Migration
    {
        private const string tableName = "users";
        
        public override void Down()
        {
            Delete.Table(tableName);
        }
        
        public override void Up()
        {
            var table = Create.Table(tableName).WithDescription("Table of users");

            table.WithColumn("id")
                .AsString(100)
                .PrimaryKey()
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