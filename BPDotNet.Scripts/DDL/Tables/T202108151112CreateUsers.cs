using FluentMigrator;

namespace BPDotNet.Scripts.DDL.Tables
{
    [Migration(202108151112, "Create table Users")]
    public class T202108151112CreateUsers : Migration
    {
        private const string tableName = "Users";
        
        public override void Down()
        {
            Delete.Table(tableName);
        }
        
        public override void Up()
        {
            var table = Create.Table(tableName).WithDescription("Table of users");
            
            
        }
    }
}