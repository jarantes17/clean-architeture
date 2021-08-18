using BPDotNet.Core.Entities;
using BPDotNet.Data.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BPDotNet.Data.Persistence.Contexts
{
    public class EfContext: DbContext
    {
        public EfContext(DbContextOptions<EfContext> option): base(option)
        { }

        #region DbSets

        public DbSet<User> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}