using CRUD_C_.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_C_.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");         
    }    
}