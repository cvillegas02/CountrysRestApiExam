using Microsoft.EntityFrameworkCore;
using ModelsCountrysRestApi.ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountrysRestApi.DB
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            
                modelBuilder.Entity<Country>().HasData(
                            new Country { alpha = "MX", name = "Mexico", independent = true, numeric_code = 001 },
                            new Country { alpha = "GT", name = "Guatemala", independent = true, numeric_code = 002 },
                            new Country { alpha = "AX", name = "Aland Islands", independent = true, numeric_code = 003 },
                            new Country { alpha = "DZ", name = "Algeria", independent = true, numeric_code = 004 },
                            new Country { alpha = "AO", name = "Angola", independent = true, numeric_code = 005 },
                            new Country { alpha = "AI", name = "Anguilla", independent = true, numeric_code = 006 },
                            new Country { alpha = "AQ", name = "Antarctica", independent = true, numeric_code = 007 },
                            new Country { alpha = "AR", name = "Argentina", independent = true, numeric_code = 008 },
                            new Country { alpha = "GN", name = "Guinea", independent = true, numeric_code = 009 },
                            new Country { alpha = "HT", name = "Haiti", independent = true, numeric_code = 010 },
                            new Country { alpha = "HN", name = "Honduras", independent = true, numeric_code = 011 }
                            );

            modelBuilder.Entity<Subdivision>().HasData(
                            new Subdivision { alpha = "AO", name = "Bengo", code = "AO-BGO	" },
                            new Subdivision { alpha = "AO", name = "Benguela", code = "AO-BGU" },
                            new Subdivision { alpha = "AO", name = "Bié", code = "AO-BIE" },
                            new Subdivision { alpha = "AO", name = "Cabinda", code = "AO-CAB" },
                            new Subdivision { alpha = "AO", name = "Cunene", code = "AO-CNN" }
                            );

            modelBuilder.Entity<User>().HasData(
                          new User { id = 1, role = "Admin", email = "test@domain.com", password = "CBBCC5FE04B937F376DC38BD824D32397B67FFBE3583747ADDD3058BEEB7264C6A74B826FB2EEB2C4BA9ADDCA3150FAF610822560717B5994489817D27FF0F4C", salt = "7278f1d5-b52a-4a0e-93b8-f57bad21adba"}
                           );


        }
    }
}
