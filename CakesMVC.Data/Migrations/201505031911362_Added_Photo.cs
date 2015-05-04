namespace CakesMVC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Photo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.ImageId);
            
            AlterColumn("dbo.Albums", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Cakes", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Cakes", "Thumbnail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cakes", "Thumbnail", c => c.String());
            AlterColumn("dbo.Cakes", "Title", c => c.String());
            AlterColumn("dbo.Albums", "Title", c => c.String());
            DropTable("dbo.Photos");
        }
    }
}
