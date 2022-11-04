using Microsoft.EntityFrameworkCore;
using TicTacDough.Shared.Domain;

namespace TicTacDough.Server.Data {
    public class ApplicationDbContext : DbContext {

        public ApplicationDbContext(DbContextOptions options) : base(options) {
        }

        public DbSet<Score> Scores { get; set; }

    }
}