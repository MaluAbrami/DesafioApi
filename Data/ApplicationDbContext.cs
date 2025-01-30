using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Raca> Racas {get;set;}
        public DbSet<Weight> Weights {get;set;}
        public DbSet<Gato> Gatos {get;set;}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    }
}