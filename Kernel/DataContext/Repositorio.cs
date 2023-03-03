using Kernel.Settings;
using Kernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.DataContext
{
    public  class Repositorio : DbContext
    {
        public Repositorio() { }

        public Repositorio(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(DatabaseSetting.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");
            builder.Entity<Produto>().ToTable("produto");
            builder.Entity<Fornecedor>().ToTable("fornecedor");
        }
    }
}
