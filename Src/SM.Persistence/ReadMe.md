# Persistence Layer

Via Package Manager Console
===========================

Update connection string in appsettings.json file of WebApi Project = "SMSqlDatabase": "Server=(localdb)\\mssqllocaldb;Database=ServiceManagement;Trusted_Connection=True;MultipleActiveResultSets=true;Application Name=ServiceManagement;"

Open Package Manager Console = Go to View => Other windows => Package Manager Console
Set Default Project to SM.Persistence in Package Manager Console

Run following commands as and when required:

add-migration -name "name-of-migration"	=> Adds a new migration
update-database							=> Updates the database to a specified migration		
remove-migration						=> Removes the last migration
script-migration						=> Generates a SQL script from migrations
update-database "MyFirstMigration"		=> Revert the database to the previous state

https://www.entityframeworktutorial.net/efcore/entity-framework-core-migration.aspx