using DienstenCheques.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beerhall.Data.Mappers
{
    public class DienstenChequeConfiguration : IEntityTypeConfiguration<DienstenCheque>
    {
        public void Configure(EntityTypeBuilder<DienstenCheque> builder)
        {
            //Table
            builder.ToTable("DienstenCheque");
            builder.HasKey(t => t.DienstenChequeNummer);

            //relationships
            builder.HasOne(t => t.Prestatie)
                .WithMany()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}