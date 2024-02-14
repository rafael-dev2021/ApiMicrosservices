﻿using ApiMicrosservicesShoppingCart.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMicrosservicesShoppingCart.Context;


public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<CartHeader> CartHeaders { get; set; }

    //Fluent API
    protected override void OnModelCreating(ModelBuilder mb)
    {
        //Product
        mb.Entity<Product>()
            .HasKey(c => c.Id);

        //Product
        mb.Entity<Product>().
           Property(c => c.Id)
            .ValueGeneratedNever();

        mb.Entity<Product>().
           Property(c => c.Name).
             HasMaxLength(100).
               IsRequired();

        mb.Entity<Product>().
          Property(c => c.Description).
               HasMaxLength(10000).
                   IsRequired();

        mb.Entity<Product>().
          Property(c => c.Images).
              HasMaxLength(600).
                  IsRequired();

        mb.Entity<Product>().
           Property(c => c.CategoryName).
               HasMaxLength(100).
                IsRequired();

        mb.Entity<Product>().
           Property(c => c.Price).
             HasPrecision(12, 2);

        //CartHeader
        mb.Entity<CartHeader>().
             Property(c => c.UserId).
             HasMaxLength(255).
                 IsRequired();

        mb.Entity<CartHeader>().
           Property(c => c.CouponCode).
              HasMaxLength(100);
    }
}