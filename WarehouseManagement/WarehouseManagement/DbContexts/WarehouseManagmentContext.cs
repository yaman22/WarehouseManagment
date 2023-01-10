using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Entities;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WarehouseManagement.Auth;

namespace WarehouseManagement.DbContexts
{
    public class WarehouseManagmentContext : IdentityDbContext<ApplicationUser>
    {
        public WarehouseManagmentContext(DbContextOptions<WarehouseManagmentContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Warehouse>().HasMany(w=>w.Products).WithMany(p=>p.Warehouses).UsingEntity<Product_Warehouse>(
            //        j=>j.HasOne(wp=>wp.Warehouse).WithMany(pw=>pw.Product_Warehouses).HasForeignKey(f=>f.WarehouseId),
            //        j=>j.HasOne(wp=>wp.Product).WithMany(pw=>pw.Product_Warehouses).HasForeignKey(f=>f.ProductId)
            //    );

            //modelBuilder.Entity<Product_Warehouse>().HasMany(p => p.BillDetails).WithMany(b => b.Product_Warehouses).UsingEntity<Product_Bill>(
            //        k=>k.HasOne(pb=>pb.Product_Warehouse).WithMany(bp=>bp.Product_Bills).HasForeignKey(f=>f.Product_Warehouse_Id),
            //        k=>k.HasOne(pb=>pb.BillDetail).WithMany(bp=>bp.Product_Bills).HasForeignKey(f=>f.BillDetailsId)
            //    );

            //modelBuilder.Entity<Warehouse>().HasOne(w => w.MainWarehouse).WithMany().HasForeignKey(f => f.MainWarehouseId)
            //    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product_Warehouse>()
                .HasKey(pw=> pw.Id);

            modelBuilder.Entity<Product_Warehouse>()
                .HasIndex(pw => new {pw.WarehouseId,pw.ProductId})
                .IsUnique();

            modelBuilder.Entity<Product_Warehouse>()
                .HasOne(wp => wp.Warehouse)
                .WithMany(w => w.Product_Warehouses)
                .HasForeignKey(wp=>wp.WarehouseId);

            modelBuilder.Entity<Product_Warehouse>()
                .HasOne(pw => pw.Product)
                .WithMany(p => p.Product_Warehouses)
                .HasForeignKey(pw => pw.ProductId);


            modelBuilder.Entity<Product_Bill>()
                .HasKey(pb => new { pb.Product_Warehouse_Id, pb.BillDetailsId });

            modelBuilder.Entity<Product_Bill>()
                .HasOne(pb => pb.Product_Warehouse)
                .WithMany(pw => pw.Product_Bills)
                .HasForeignKey(pb => pb.Product_Warehouse_Id);

            modelBuilder.Entity<Product_Bill>()
                .HasOne(bp => bp.BillDetail)
                .WithMany(bd => bd.Product_Bills)
                .HasForeignKey(bp => bp.BillDetailsId);


            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = Guid.Parse("87bb64c6-1eb0-4e26-b245-afc6359147d5"),
                    Name = "Sugar",
                    Price = 5000
                },
                new Product()
                {
                    Id = Guid.Parse("9e9c1db5-afe3-4948-915f-34806db86462"),
                    Name = "Salt",
                    Price = 1000
                },
                new Product()
                {
                    Id = Guid.Parse("81b13ee0-2ca3-4f2a-b9d5-70f06a4f29cb"),
                    Name = "Indomi",
                    Price = 1500
                },
                new Product()
                {
                    Id = Guid.Parse("9a66da83-ec29-4f75-8b8d-a36d543c17b5"),
                    Name = "Coffe",
                    Price = 55000
                },
                new Product()
                {
                    Id = Guid.Parse("d558dc08-c5a8-4dcc-ad35-00c170bb0330"),
                    Name = "Tee",
                    Price = 60000
                }
                );


            modelBuilder.Entity<Manager>().HasData(
                new Manager()
                {
                    Id = Guid.Parse("702b0a47-e7cd-42ca-bed6-e08b66abb653"),
                    Name = "Ahmad"
                },
                new Manager()
                {
                    Id = Guid.Parse("927ef47d-2231-4218-a8a8-f570b12c7fde"),
                    Name = "Ali"
                },
                new Manager()
                {
                    Id = Guid.Parse("d0f1d718-05c8-48d6-a4d0-62a06f2d0d29"),
                    Name = "Yamen"
                },
                new Manager()
                {
                    Id = Guid.Parse("a90e41b6-be95-4382-97cc-4f6970b1978c"),
                    Name = "Mohannad"
                },
                new Manager()
                {
                    Id = Guid.Parse("90515313-fb47-4ab4-bec1-9303c7661e48"),
                    Name = "Abdo"
                }
            );


            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse()
                {
                    Id = Guid.Parse("1330cde5-971d-4727-989a-efff06576c86"),
                    Name = "Alzahraa",
                    Address = "Alzahraa",
                    ManagerId = Guid.Parse("702b0a47-e7cd-42ca-bed6-e08b66abb653")

                },
                new Warehouse()
                {
                    Id = Guid.Parse("734f2c3c-0a2a-4860-a6c6-27c013b6aa21"),
                    Name = "Mohafaza",
                    Address = "Mohafaza",
                    ManagerId = Guid.Parse("927ef47d-2231-4218-a8a8-f570b12c7fde")

                },
                new Warehouse()
                {
                    Id = Guid.Parse("86bb2b95-d023-4390-8dc1-2d27da8a8a15"),
                    Name = "Mogambo",
                    Address = "Mogambo",
                    ManagerId = Guid.Parse("d0f1d718-05c8-48d6-a4d0-62a06f2d0d29")

                },
                new Warehouse()
                {
                    Id = Guid.Parse("c22d0d3e-4e42-4a3b-a930-f628c14b874d"),
                    Name = "Andalos",
                    Address = "Andalos",
                    ManagerId = Guid.Parse("a90e41b6-be95-4382-97cc-4f6970b1978c")

                },
                new Warehouse()
                {
                    Id = Guid.Parse("5af29802-9a62-42e9-bb5e-b3459cb81f40"),
                    Name = "Hamdania",
                    Address = "Hamdania",
                    ManagerId = Guid.Parse("90515313-fb47-4ab4-bec1-9303c7661e48")

                },

                //subWarehouses

                new Warehouse()
                {
                    Id = Guid.Parse("fcc58645-f4cd-4700-a667-fec39323fded"),
                    Name = "Alzahraa.1",
                    Address = "Alzahraa",
                    ManagerId = Guid.Parse("702b0a47-e7cd-42ca-bed6-e08b66abb653"),
                    MainWarehouseId = Guid.Parse("1330cde5-971d-4727-989a-efff06576c86")
                },
                new Warehouse()
                {
                    Id = Guid.Parse("3daf5dd6-dbb4-44d8-88ac-15c65a6741d4"),
                    Name = "Mohafaza.1",
                    Address = "Mohafaza",
                    ManagerId = Guid.Parse("927ef47d-2231-4218-a8a8-f570b12c7fde"),
                    MainWarehouseId = Guid.Parse("734f2c3c-0a2a-4860-a6c6-27c013b6aa21")
                },
                new Warehouse()
                {
                    Id = Guid.Parse("4dd38fd3-8fcc-4de5-b56c-169785e1586f"),
                    Name = "Mogambo.1",
                    Address = "Mogambo",
                    ManagerId = Guid.Parse("d0f1d718-05c8-48d6-a4d0-62a06f2d0d29"),
                    MainWarehouseId = Guid.Parse("86bb2b95-d023-4390-8dc1-2d27da8a8a15")
                },
                new Warehouse()
                {
                    Id = Guid.Parse("11ec5b78-7e2b-41c2-ae9f-5634a1105887"),
                    Name = "Andalos.1",
                    Address = "Andalos",
                    ManagerId = Guid.Parse("a90e41b6-be95-4382-97cc-4f6970b1978c"),
                    MainWarehouseId = Guid.Parse("c22d0d3e-4e42-4a3b-a930-f628c14b874d")
                },
                new Warehouse()
                {
                    Id = Guid.Parse("e24e4c78-aa68-4f6e-9749-56da8bb6f67a"),
                    Name = "Hamdania.1",
                    Address = "Hamdania",
                    ManagerId = Guid.Parse("90515313-fb47-4ab4-bec1-9303c7661e48"),
                    MainWarehouseId = Guid.Parse("5af29802-9a62-42e9-bb5e-b3459cb81f40")
                },
                new Warehouse()
                {
                    Id = Guid.Parse("879eb9b1-1bfb-4fd5-96e2-9a4bbc500c29"),
                    Name = "Alzahraa.2",
                    Address = "Alzahraa",
                    ManagerId = Guid.Parse("702b0a47-e7cd-42ca-bed6-e08b66abb653"),
                    MainWarehouseId = Guid.Parse("1330cde5-971d-4727-989a-efff06576c86")
                },
                new Warehouse()
                {
                    Id = Guid.Parse("f27a7dd1-b44a-423f-9a84-2462a0f2ad48"),
                    Name = "Andalos.2",
                    Address = "Andalos",
                    ManagerId = Guid.Parse("a90e41b6-be95-4382-97cc-4f6970b1978c"),
                    MainWarehouseId = Guid.Parse("c22d0d3e-4e42-4a3b-a930-f628c14b874d")
                },
                new Warehouse()
                {
                    Id = Guid.Parse("53965e59-5ad4-4d1b-bf98-b743fb1d8681"),
                    Name = "Andalos.3",
                    Address = "Andalos",
                    ManagerId = Guid.Parse("a90e41b6-be95-4382-97cc-4f6970b1978c"),
                    MainWarehouseId = Guid.Parse("c22d0d3e-4e42-4a3b-a930-f628c14b874d")
                }
               );


            modelBuilder.Entity<BillDetails>().HasData(
                new BillDetails
                {
                    Id = Guid.Parse("67ac8156-7c8a-44f3-aad8-8140a0f95e01"),
                    type = OperationType.Import,
                    Date = DateTime.Now,
                    managerId = Guid.Parse("702b0a47-e7cd-42ca-bed6-e08b66abb653"),
                    warehouseId = Guid.Parse("1330cde5-971d-4727-989a-efff06576c86"),
                    TotalCost = 2000000
                },

                new BillDetails
                {
                    Id = Guid.Parse("c9dab83a-8739-4365-935d-20b6ad033ea0"),
                    type = OperationType.Export,
                    Date = DateTime.Now,
                    managerId = Guid.Parse("702b0a47-e7cd-42ca-bed6-e08b66abb653"),
                    warehouseId = Guid.Parse("1330cde5-971d-4727-989a-efff06576c86"),
                    TotalCost = 1000000
                }
                );


            modelBuilder.Entity<Product_Warehouse>().HasData(
                new Product_Warehouse
                {
                    Id = Guid.Parse("18c2c451-0e25-4ed4-b68d-ab95eb3e8820"),
                    WarehouseId = Guid.Parse("1330cde5-971d-4727-989a-efff06576c86"),
                    ProductId = Guid.Parse("d558dc08-c5a8-4dcc-ad35-00c170bb0330"),
                    Amount = 5
                },
                new Product_Warehouse
                {
                    Id = Guid.Parse("386163bf-fe58-4812-9fb7-46a7673febed"),
                    WarehouseId = Guid.Parse("1330cde5-971d-4727-989a-efff06576c86"),
                    ProductId = Guid.Parse("87bb64c6-1eb0-4e26-b245-afc6359147d5"),
                    Amount = 100
                },
                new Product_Warehouse
                {
                    Id = Guid.Parse("31280ecc-509a-40ce-8eee-e322f9d98238"),
                    WarehouseId = Guid.Parse("1330cde5-971d-4727-989a-efff06576c86"),
                    ProductId = Guid.Parse("81b13ee0-2ca3-4f2a-b9d5-70f06a4f29cb"),
                    Amount = 100
                },
                new Product_Warehouse
                {
                    Id = Guid.Parse("c88e1397-289a-4dd2-8500-cd2b6b283d2b"),
                    WarehouseId = Guid.Parse("1330cde5-971d-4727-989a-efff06576c86"),
                    ProductId = Guid.Parse("9e9c1db5-afe3-4948-915f-34806db86462"),
                    Amount = 50
                }
                );


            modelBuilder.Entity<Product_Bill>().HasData(
                new Product_Bill
                {
                    Id = Guid.Parse("834554e1-aea5-4ab1-bbc7-d79a8dd90527"),
                    BillDetailsId = Guid.Parse("67ac8156-7c8a-44f3-aad8-8140a0f95e01"),
                    Product_Warehouse_Id = Guid.Parse("386163bf-fe58-4812-9fb7-46a7673febed"),
                    Type = OperationType.Import,
                    Date = DateTime.Now,
                    Amount = 200,
                    Cost = 1000000
                },
                new Product_Bill
                {
                    Id = Guid.Parse("ad3711b7-67aa-47b0-b73d-f5ac22a91e3a"),
                    BillDetailsId = Guid.Parse("67ac8156-7c8a-44f3-aad8-8140a0f95e01"),
                    Product_Warehouse_Id = Guid.Parse("18c2c451-0e25-4ed4-b68d-ab95eb3e8820"),
                    Type = OperationType.Import,
                    Date = DateTime.Now,
                    Amount = 10,
                    Cost = 600000
                },
                new Product_Bill
                {
                    Id = Guid.Parse("94157a7c-964a-40d0-9bcd-37a608e08c07"),
                    BillDetailsId = Guid.Parse("67ac8156-7c8a-44f3-aad8-8140a0f95e01"),
                    Product_Warehouse_Id = Guid.Parse("31280ecc-509a-40ce-8eee-e322f9d98238"),
                    Type = OperationType.Import,
                    Date = DateTime.Now,
                    Amount = 200,
                    Cost = 300000
                },
                new Product_Bill
                {
                    Id = Guid.Parse("0a06c1f7-7b44-4e5b-b3df-9a0e24a705ec"),
                    BillDetailsId = Guid.Parse("67ac8156-7c8a-44f3-aad8-8140a0f95e01"),
                    Product_Warehouse_Id = Guid.Parse("c88e1397-289a-4dd2-8500-cd2b6b283d2b"),
                    Type = OperationType.Import,
                    Date = DateTime.Now,
                    Amount = 100,
                    Cost = 100000
                },


                new Product_Bill
                {
                    Id = Guid.Parse("3fb50473-7c2e-46ff-b6b1-75a2b31e7ff6"),
                    BillDetailsId = Guid.Parse("c9dab83a-8739-4365-935d-20b6ad033ea0"),
                    Product_Warehouse_Id = Guid.Parse("386163bf-fe58-4812-9fb7-46a7673febed"),
                    Type = OperationType.Export,
                    Date = DateTime.Now,
                    Amount = 100,
                    Cost = 500000
                },
                new Product_Bill
                {
                    Id = Guid.Parse("eebb2de5-1b4b-4026-9028-fc22dc718653"),
                    BillDetailsId = Guid.Parse("c9dab83a-8739-4365-935d-20b6ad033ea0"),
                    Product_Warehouse_Id = Guid.Parse("18c2c451-0e25-4ed4-b68d-ab95eb3e8820"),
                    Type = OperationType.Export,
                    Date = DateTime.Now,
                    Amount = 5,
                    Cost = 300000
                },
                new Product_Bill
                {
                    Id = Guid.Parse("d4c64d8e-4ce3-4368-9685-c23190c71fe8"),
                    BillDetailsId = Guid.Parse("c9dab83a-8739-4365-935d-20b6ad033ea0"),
                    Product_Warehouse_Id = Guid.Parse("31280ecc-509a-40ce-8eee-e322f9d98238"),
                    Type = OperationType.Export,
                    Date = DateTime.Now,
                    Amount = 100,
                    Cost = 150000
                },
                new Product_Bill
                {
                    Id = Guid.Parse("eae46677-a94d-406f-a3d8-504357c910eb"),
                    BillDetailsId = Guid.Parse("c9dab83a-8739-4365-935d-20b6ad033ea0"),
                    Product_Warehouse_Id = Guid.Parse("c88e1397-289a-4dd2-8500-cd2b6b283d2b"),
                    Type = OperationType.Export,
                    Date = DateTime.Now,
                    Amount = 50,
                    Cost = 50000
                }

                );




            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<BillDetails> BillDetails { get; set; }
        public DbSet<Product_Warehouse> Products_Warehouses { get; set; }
        public DbSet<Product_Bill> Products_Bills { get;set; }


    }
}
