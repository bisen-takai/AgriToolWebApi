using AgriToolWebApi.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgriToolWebApi.Infrastructure.Persistence.Contexts
{
    /// <summary>
    /// 各種テーブルのデータベースコンテキストを登録する
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UserPersistenceEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AppDbContext が属するアセンブリを指定
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
