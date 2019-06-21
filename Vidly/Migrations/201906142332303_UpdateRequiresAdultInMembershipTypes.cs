namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRequiresAdultInMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET RequiresAdult= 0 WHERE id = 1");
            Sql("UPDATE MembershipTypes SET RequiresAdult= 1 WHERE id = 2");
            Sql("UPDATE MembershipTypes SET RequiresAdult= 1 WHERE id = 3");
            Sql("UPDATE MembershipTypes SET RequiresAdult= 1 WHERE id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
