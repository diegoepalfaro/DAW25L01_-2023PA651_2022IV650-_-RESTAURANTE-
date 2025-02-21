﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2023PA651_2022IV650.Models
{
    public class RestauranteContext : DbContext
    {

        public RestauranteContext(DbContextOptions<RestauranteContext> options) : base(options) 
        { 
        }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Plato> Platos { get; set; }

        public DbSet<Motorista> Motorista { get; set; }
    }
}
