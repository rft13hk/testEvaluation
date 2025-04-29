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
    }
}
