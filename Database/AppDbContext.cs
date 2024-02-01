using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCrud.People;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "Data Source=Banco.sqlite");
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }
    }
}