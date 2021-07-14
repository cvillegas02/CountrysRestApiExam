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

        public virtual DbSet<Country> Country { get; set; }

        public virtual DbSet<Subdivision> Subdivision { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                optionsBuilder.UseMySql(Configuration.CONNECTION_BD_MYSQL_LOCAL_HOST);
#elif RELEASE
                    optionsBuilder.UseMySql(Configuration.CONNECTION_BD_MYSQL_DEV);
#endif
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.email)
                    .HasName("IX_EMAIL");

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(128)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.salt)
                    .IsRequired()
                    .HasColumnName("salt")
                    .HasColumnType("varchar(36)")
                    .HasDefaultValueSql("'0'");

              
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries");

                entity.HasKey(e => e.alpha)
                   .HasName("alpha");

                entity.Property(e => e.alpha)
                    .HasColumnName("alpha")
                    .HasColumnType("varchar(2)");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.numeric_code)
                    .IsRequired()
                    .HasColumnName("numeric_code")
                    .HasColumnType("int(3)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.independent)
                    .IsRequired()
                    .HasColumnName("independent")
                    .HasColumnType("SMALLINT(2)")
                    .HasDefaultValueSql("'1'");

            });

            modelBuilder.Entity<Subdivision>(entity =>
            {
                entity.ToTable("subdivisions");

                entity.HasKey(e => e.code)
                   .HasName("code");

                entity.Property(e => e.code)
                    .HasColumnName("code")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.alpha)
                    .IsRequired()
                    .HasColumnName("alpha")
                    .HasColumnType("varchar(3)");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");


            });
            
        }
    }
}
