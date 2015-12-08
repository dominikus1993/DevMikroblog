using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMikroblog.Domain.Model;

namespace DevMikroblog.Domain.DatabaseContext.Interface
{
    public interface IDbContext:IDisposable
    {
        DbSet<ApplicationUser> ApplicationUsers { get; set; } 
    }
}
