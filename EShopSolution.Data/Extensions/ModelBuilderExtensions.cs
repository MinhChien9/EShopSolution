﻿using EShopSolution.Data.Entities;
using EShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig()
                {
                    Key = "HomeTitle",
                    Value = "This is home page of EShopSolution"
                },
                new AppConfig()
                {
                    Key = "HomeKeyword",
                    Value = "This is keyword of EShopSolution"
                },
                new AppConfig()
                {
                    Key = "HomeDescription",
                    Value = "This is description of EShopSolution"
                }
                );

            modelBuilder.Entity<Language>().HasData(
                new Language()
                {
                    Id = "vi-VN",
                    Name = "Tiếng Việt",
                    IsDefault = true
                },
                new Language()
                {
                    Id = "en-US",
                    Name = "English",
                    IsDefault = false
                }
                );

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active
                },
                new Category()
                {
                    Id = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 2,
                    Status = Status.Active
                }
                );
            modelBuilder.Entity<CategoryTranslation>().HasData(
                new CategoryTranslation()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Áo nam",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nam",
                    SeoDescription = "Sản phẩm áo thời trang nam",
                    SeoTitle = "Sản phẩm áo thời trang nam"
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Men Shirt",
                    LanguageId = "en-US",
                    SeoAlias = "men-shirt",
                    SeoDescription = "The shirt product for men",
                    SeoTitle = "The shirt product for men"
                },
                new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Áo nữ",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nu",
                    SeoDescription = "Sản phẩm áo thời trang nữ",
                    SeoTitle = "Sản phẩm áo thời trang nữ"
                },
                new CategoryTranslation()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Women Shirt",
                    LanguageId = "en-US",
                    SeoAlias = "women-shirt",
                    SeoDescription = "The shirt product for women",
                    SeoTitle = "The shirt product for women"
                }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 200000,
                    Price = 100000,
                    Stock = 0,
                    ViewCount = 0
                }
                );

            modelBuilder.Entity<ProductTranslation>().HasData(
                    new ProductTranslation()
                    {
                        Id = 1,
                        ProductId = 1,
                        Name = "Áo sơ mi nam trắng Việt Tiến",
                        LanguageId = "vi-VN",
                        SeoAlias = "ao-so-mi-nam-trang-viet-tien",
                        SeoDescription = "Áo sơ mi nam trắng Việt Tiến",
                        SeoTitle = "Áo sơ mi nam trắng Việt Tiến",
                        Details = "Áo sơ mi nam trắng Việt Tiến",
                        Description = "Áo sơ mi nam trắng Việt Tiến"
                    },
                    new ProductTranslation()
                    {
                        Id = 2,
                        ProductId = 1,
                        Name = "Viet Tien Men T-Shirt",
                        LanguageId = "en-US",
                        SeoAlias = "viet-tien-men-t-shirt",
                        SeoDescription = "Viet Tien Men T-Shirt",
                        SeoTitle = "Viet Tien Men T-Shirt",
                        Details = "Viet Tien Men T-Shirt",
                        Description = "Viet Tien Men T-Shirt"
                    }
                );

            modelBuilder.Entity<ProductInCategory>().HasData(
                    new ProductInCategory()
                    {
                        ProductId = 1,
                        CategoryId = 1
                    }
                );

            var roleId = new Guid("E24409CB-0A1C-4D7C-A43A-25FB9D47BA65");
            var adminId = new Guid("BAE8863E-1029-4E70-8056-5BA1379DDE32");
            var userRoleId = new Guid("534DBE06-BA7A-481E-A2A7-7B2724427D7C");
            var userId = new Guid("5AADC56F-8EAA-4FE0-9A23-BF8C4320024C");

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole()
                {
                    Id = roleId,
                    Name = "admin",
                    NormalizedName = "admin",
                    Description = "Admin Role"

                },
                new AppRole()
                {
                    Id = userRoleId,
                    Name = "user",
                    NormalizedName = "user",
                    Description = "User Role"

                });

            var hasher = new PasswordHasher<AppUser>();

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser()
                {
                    Id = adminId,
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "chien17099@gmail.com",
                    NormalizedEmail = "chien17099@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "admin1999"),
                    SecurityStamp = string.Empty,
                    FirstName = "Chien",
                    LastName = "Huynh",
                    Dob = new DateTime(1999, 10, 17)

                },
                new AppUser()
                {
                    Id = userId,
                    UserName = "user1",
                    NormalizedUserName = "user1",
                    Email = "user1@gmail.com",
                    NormalizedEmail = "user1@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "user1999"),
                    SecurityStamp = string.Empty,
                    FirstName = "User",
                    LastName = "01",
                    Dob = new DateTime(1999, 11, 27)

                });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = roleId,
                    UserId = adminId
                },
                new IdentityUserRole<Guid>
                {
                    RoleId = userRoleId,
                    UserId = userId
                });

        }
    }
}
