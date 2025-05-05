using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

[ExcludeFromCodeCoverage]
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(u => u.ProductCode).IsRequired().HasMaxLength(8);
        builder.Property(u => u.ProductName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Price).IsRequired().HasColumnType("decimal(10,2)");
        builder.Property(u => u.UserId).HasColumnType("uuid").IsRequired(); 
        builder.Property(u => u.UpdateAt).IsRequired();
        builder.Property(u => u.CreateAt).IsRequired();
        builder.Property(u => u.DeletedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValue(null)
            .IsRequired(false);


        builder.HasOne(s2 => s2.User)
            .WithMany(s1 => s1.Products)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(s2 => s2.SaleItems)
            .WithOne(s1 => s1.Product)
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}
