using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DienstenCheques.Models.Domain;
using Beerhall.Data.Mappers;

namespace DienstenCheques.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Onderneming> Ondernemingen { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new BestellingConfiguration());
            builder.ApplyConfiguration(new DienstenChequeConfiguration());
            builder.ApplyConfiguration(new PrestatieConfiguration());
            builder.ApplyConfiguration(new GebruikerConfiguration());
        }
    }
}
