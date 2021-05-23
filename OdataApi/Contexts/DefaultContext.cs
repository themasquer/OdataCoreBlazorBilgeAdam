using Microsoft.EntityFrameworkCore;
using OdataApi.Entities;

namespace OdataApi.Contexts
{
    public class DefaultContext : DbContext
    {
        public DbSet<Oyun> Oyunlar { get; set; }
        public DbSet<Yapimci> Yapimcilar { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }

        public DefaultContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}