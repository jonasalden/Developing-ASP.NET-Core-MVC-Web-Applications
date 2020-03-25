using AspNetCore_07_EfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore_07_EfCore.Data
{
    public class CupcakeContext : DbContext
    {
        public CupcakeContext(DbContextOptions<CupcakeContext> options) : base(options)
        {
        }

        public DbSet<CupCake> Cupcakes { get; set; }
        public DbSet<Bakery> Bakeries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bakery>().HasData(
                new Bakery
                {
                    BakeryId = 1,
                    BakeryName = "Jonas Bakery",
                    Address = "Olmbroroad 6",
                    Quantity = 13
                },
                new Bakery
                {
                    BakeryId = 2,
                    BakeryName = "Jannes Cupcakes",
                    Address = "Kvinnersta 13",
                    Quantity = 2
                });

            modelBuilder.Entity<CupCake>().HasData(
                new CupCake
                {
                    CupcakeId = 1,
                    CupcakeType = CupcakeType.Birthday,
                    Description = "Birthday cake",
                    GlutenFree = true,
                    Price = 2.5,
                    BakeryId = 1,
                    ImageMimeType = "image/jpeg",
                    ImageName = "birthday-cupcake.jpg",
                    CaloricValue = 432
                },
                    new CupCake
                    {
                        CupcakeId = 2,
                        CupcakeType = CupcakeType.Birthday,
                        Description = "Chocolate muffin",
                        GlutenFree = false,
                        Price = 1.2,
                        BakeryId = 2,
                        ImageMimeType = "image/jpeg",
                        ImageName = "pink-cupcake.jpg",
                        CaloricValue = 232
                    },
                        new CupCake
                        {
                            CupcakeId = 3,
                            CupcakeType = CupcakeType.Birthday,
                            Description = "Strawberry panncake",
                            GlutenFree = true,
                            Price = 3.6,
                            BakeryId = 1,
                            ImageMimeType = "image/jpeg",
                            ImageName = "turquoise-cupcake.jpg",
                            CaloricValue = 15
                        }
                );
        }
    }
}
