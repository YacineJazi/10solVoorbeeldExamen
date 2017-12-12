using DienstenCheques.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beerhall.Data.Mappers
{
    public class PrestatieConfiguration : IEntityTypeConfiguration<Prestatie>
    {
        public void Configure(EntityTypeBuilder<Prestatie> builder)
        {
            //Table
            builder.ToTable("Prestatie");

            //Relationships
            builder.HasOne(t => t.Onderneming)
               .WithMany().IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
