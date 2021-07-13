using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountrysRestApi.Helpers;
using Microsoft.EntityFrameworkCore;
using ModelsCountrysRestApi.ModelsDB;

namespace CountrysRestApi.DB
{
    public class DataBase : DbContext
    {
        public DataBase()
        {
        }

        public DataBase(DbContextOptions<DataBase> options)
           : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Configuration.CONNECTION_BD_MYSQL_LOCAL_HOST);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("IX_EMAIL");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(128)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasColumnType("varchar(36)")
                    .HasDefaultValueSql("'0'");

              
            });

        }
    }
}
