using Microsoft.EntityFrameworkCore;
using TodoAPP.Models;

namespace TodoAPP.Context
{
    public class TodoContext : DbContext
    {

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-TMTJUHH;Initial Catalog=mvcToDoList.Models;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }
    }
}
