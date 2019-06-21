namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertarDataEnGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES(1,'Comedia')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(2,'Acción')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(3,'Suspenso')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(4,'Romance')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(5,'Animadas')");
            Sql("INSERT INTO Genres (Id, Name) VALUES(6,'Ciencia Ficción')");
        }
        
        public override void Down()
        {
        }
    }
}
