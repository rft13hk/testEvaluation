using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

[ExcludeFromCodeCoverage]
public class SaleItemsConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(u => u.SaleId).HasColumnType("uuid").IsRequired();
        builder.Property(u => u.SaleDate)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("now()")
            .IsRequired();
        builder.Property(u => u.ProductId).HasColumnType("uuid").IsRequired();
        builder.Property(u => u.Price).IsRequired();
        builder.Property(u => u.Quantity).IsRequired();
        builder.Property(u => u.Discount).IsRequired();
        builder.Property(u => u.TotalPrice).IsRequired();
        builder.Property(u => u.CreateAt).IsRequired();
        builder.Property(u => u.UpdateAt).IsRequired();
        builder.Property(u => u.DeletedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValue(null)
            .IsRequired(false);

        builder.HasOne(s2 => s2.Sale)
            .WithMany(s1 => s1.SaleItems)
            .HasForeignKey(s => s.SaleId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(s2 => s2.Product)
            .WithMany(s1 => s1.SaleItems)
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s2 => s2.User)
            .WithMany(s1 => s1.SaleItems)
            .HasForeignKey(s => s.UserId)  
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}
