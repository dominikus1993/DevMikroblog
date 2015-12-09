using System;
using System.Data.Entity;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.DatabaseContext.Interface
{
    public interface IDbContext:IDisposable
    {
        DbSet<ApplicationUser> ApplicationUsers { get; set; }

        DbSet<Post> Posts { get; set; }

        DbSet<Comment> Comments { get; set; }

        DbSet<Vote> Votes { get; set; }

        DbSet<Tag> Tags { get; set; }

        int SaveChanges();
    }
}
