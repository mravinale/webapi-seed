namespace WebApiSeed.Data.Configuration.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Domain;

    public class WebApiSeedDbInitializer : CreateDatabaseIfNotExists<WebApiSeedDbContext>
    {
        protected override void Seed(WebApiSeedDbContext context)
        {
            var user1 = new User
            {
                Gender = Gender.Male,
                UserName = "JohnDoe",
            };

            var user2 = new User
            {
                Gender = Gender.Female,
                UserName = "JaneDoe",
            };

            context.Entity<User>().AddOrUpdate(user => user.UserName, user1, user2);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}