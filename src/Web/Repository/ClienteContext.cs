using System;
using Microsoft.EntityFrameworkCore;
using CaseTempus.Models;

namespace CaseTempus.Repository
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options){}

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cliente>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}