using DienstenCheques.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Beerhall.Data.Mappers
{
    public class GebruikerConfiguration : IEntityTypeConfiguration<Gebruiker>
    {
        public void Configure(EntityTypeBuilder<Gebruiker> builder)
        {
            // table & PK
            builder.ToTable("Gebruiker");
            builder.HasKey(t => t.GebruikersNummer);
            // properties
            builder.Property(t => t.Naam).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Voornaam).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Email).IsRequired().HasMaxLength(100);
            // associations
            builder.HasMany(t => t.Bestellingen).WithOne().IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.Prestaties).WithOne().IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.Portefeuille).WithOne().IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}