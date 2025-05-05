using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

[ExcludeFromCodeCoverage]
public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(u => u.BranchName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.UserId).HasColumnType("uuid").IsRequired(); 
        builder.Property(u => u.UpdateAt).IsRequired();
        builder.Property(u => u.CreateAt).IsRequired();
        builder.Property(u => u.DeletedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValue(null)
            .IsRequired(false);

        builder.HasOne(s2 => s2.User)
            .WithMany(s1 => s1.Branches)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasMany(s2 => s2.Sales)
            .WithOne(s1 => s1.Branch)
            .HasForeignKey(s => s.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
