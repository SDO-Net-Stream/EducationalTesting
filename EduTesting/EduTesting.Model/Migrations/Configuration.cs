using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EduTesting.Model.Migrations
{
<<<<<<< HEAD
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /*internal sealed class Configuration : DbMigrationsConfiguration<EduTesting.Model.EduTestingDB>
=======
    internal sealed class Configuration : DbMigrationsConfiguration<EduTestingContext>
>>>>>>> 92242cc6867a61bb2fd17d0fdf647e5e3794ac6c
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "EduTesting.Model.EduTestingContext";
        }

		protected override void Seed(EduTestingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }*/
}
