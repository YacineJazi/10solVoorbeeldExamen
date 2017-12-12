using DienstenCheques.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beerhall.Data.Mappers
{
    public class BestellingConfiguration : IEntityTypeConfiguration<Bestelling>
    {
        public void Configure(EntityTypeBuilder<Bestelling> builder)
        {
            //Table name
            builder.ToTable("Bestelling");
        }
    }
}
