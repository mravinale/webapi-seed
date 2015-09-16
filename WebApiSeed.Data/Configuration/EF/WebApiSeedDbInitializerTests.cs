namespace WebApiSeed.Data.Configuration.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Domain;

    public class WebApiSeedDbInitializerTests : DropCreateDatabaseAlways<WebApiSeedDbContext>
    {
        protected override void Seed(WebApiSeedDbContext context)
        {
            var user1 = new User
            {
                Gender = Gender.Male,
                UserName = "SeanOPry",
            };

            var user2 = new User
            {
                Gender = Gender.Female,
                UserName = "AliceSilverstone",
            };

            var user3 = new User
            {
                Gender = Gender.Female,
                UserName = "AnnaPaquin",
            };

            var user4 = new User
            {
                Gender = Gender.Male,
                UserName = "JamesDean",
            };

            context.Entity<User>().AddOrUpdate(user => user.UserName, user1, user2, user3, user4);

            context.SaveChanges();
        }
    }
}