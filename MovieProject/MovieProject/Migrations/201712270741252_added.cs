namespace MovieProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "HashCode", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Users", "isLoggedIn", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "isAdmin", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Users", "Admin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Admin", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Users", "isAdmin");
            DropColumn("dbo.Users", "isLoggedIn");
            DropColumn("dbo.Users", "HashCode");
        }
    }
}
