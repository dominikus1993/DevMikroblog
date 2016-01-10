using DevMikroblog.Domain.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DevMikroblog.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DevMikroblog.Domain.DatabaseContext.Implementation.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DevMikroblog.Domain.DatabaseContext.Implementation.ApplicationDbContext context)
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

            context.ApplicationUsers.AddOrUpdate(new ApplicationUser
            {
                Email = "dominikus1910@gmail.com",
                EmailConfirmed = false,
                PasswordHash = "AFvWrrum1jF9zMCJ8c5bIZPZBeYK4ImgtL4mQnpn7rOpKTRnXZ3gO33NuPXmdJBCrg==",
                SecurityStamp = "af4fc158-acf4-4bbb-9450-2d292c6152d8",
                PhoneNumber = null,
                UserName = "dominikus1910@gmail.com"
            });

            context.ApplicationUsers.AddOrUpdate(new ApplicationUser
            {
                Email = "dominik.kotecki1@gmail.com",
                EmailConfirmed = false,
                PasswordHash = "ABkQmlndCCezemrbeUa9eAfiIN1Hp79Cg+xmwNtAfBlO9Qama7v72VnZier1yUR1bA==",
                SecurityStamp = "1a44df0f-983d-45a8-b792-79826c3713b9",
                PhoneNumber = null,
                UserName = "dominik.kotecki1@gmail.com"
            });


            context.Roles.AddOrUpdate(new IdentityRole { Id = "1" , Name = "Administrator" });
            context.Roles.AddOrUpdate(new IdentityRole { Id = "2", Name = "User" });
        }
    }
}
