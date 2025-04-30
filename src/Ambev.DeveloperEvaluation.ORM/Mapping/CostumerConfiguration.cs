using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CostumerConfiguration : IEntityTypeConfiguration<Costumer>
{
    public void Configure(EntityTypeBuilder<Costumer> builder)
    {
        builder.ToTable("Costumers");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(u => u.CPF)
            .IsRequired()
            .HasMaxLength(11)
            .HasConversion(
                v => v,
                v => Regex.Replace(v, @"[^\d]", string.Empty))
            .HasComment("Must be valid and not null or empty.");
        builder.Property(u => u.CostumerName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.UserId).HasColumnType("uuid").IsRequired(); 
        builder.Property(u => u.UpdateAt).IsRequired();
        builder.Property(u => u.CreateAt).IsRequired();
        builder.Property(u => u.DeletedAt)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValue(null)
            .IsRequired(false);

        builder.HasOne(s2 => s2.Users)
            .WithMany(s1 => s1.Costumers)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s2 => s2.Sales)
            .WithOne(s1 => s1.Costumer)
            .HasForeignKey(s => s.CostumerId)
            .OnDelete(DeleteBehavior.Restrict);



    }






}
