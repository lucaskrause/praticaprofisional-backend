using Microsoft.EntityFrameworkCore;
using RUPsystem.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUPsystem.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Paises> Paises { get; set; }
    }
}
