using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

[ExcludeFromCodeCoverage]
public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(u => u.SaleNumber).IsRequired().HasMaxLength(50);
        builder.Property(u => u.SaleDate).IsRequired();
        builder.Property(u => u.CostumerId).HasColumnType("uuid").IsRequired();
        builder.Property(u => u.BranchId).HasColumnType("uuid").IsRequired();
        builder.Property(u => u.UserId).HasColumnType("uuid").IsRequired(); 
        builder.Property(u => u.UpdateAt).IsRequired();
        builder.Property(u => u.CreateAt).IsRequired();
        builder.Property(u => u.DeletedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValue(null)
            .IsRequired(false);

        builder.HasOne(s2 => s2.Costumer)
            .WithMany(s1 => s1.Sales)
            .HasForeignKey(s => s.CostumerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s2 => s2.Branch)
            .WithMany(s1 => s1.Sales)
            .HasForeignKey(s => s.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s2 => s2.User)
            .WithMany(s1 => s1.Sales)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s2 => s2.SaleItems)
            .WithOne(s1 => s1.Sale)
            .HasForeignKey(s => s.SaleId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}
