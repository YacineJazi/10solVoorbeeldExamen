using DienstenCheques.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beerhall.Data.Mappers
{
    public class OndernemingConfiguration : IEntityTypeConfiguration<Onderneming>
    {
        public void Configure(EntityTypeBuilder<Onderneming> builder)
        {
            //Table name
            builder.ToTable("Onderneming");
        }
    }
}
