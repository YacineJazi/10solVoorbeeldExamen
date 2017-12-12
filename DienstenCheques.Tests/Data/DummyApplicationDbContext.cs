using System;
using DienstenCheques.Models.Domain;

namespace DienstenCheques.Tests.Data
{
    public class DummyApplicationDbContext
    {
        public Gebruiker Jan { get; set; }
        public Gebruiker An { get; set; }
        public Gebruiker Tine { get; set; }

        public DummyApplicationDbContext()
        {
            Onderneming onderneming = new Onderneming() { Naam = "Hogeschool Gent" };
            //Nog 2 openstaande prestaties, 1 cheque over
            Jan = new Gebruiker() { GebruikersNummer = 1000000000, Naam = "Peeters", Voornaam = "Jan", Email = "jan.peeters@hogent.be" };
            for (int i = 12; i >= 0; i--)
            {
                Prestatie pres = new Prestatie()
                {
                    AantalUren = 4,
                    DatumPrestatie = DateTime.Today.AddMonths(-i),
                    PrestatieType = PrestatieType.Schoonmaken,
                    Onderneming = onderneming,
                    Betaald = true
                };
               Jan.Prestaties.Add(pres);
            }
            Jan.GetPrestatie(11).Betaald = false;
            Jan.GetPrestatie(12).Betaald = false;
            int p = 0;
            int pi = 0;
            for (int i = 3; i > 0; i--)
            {
                Bestelling b = new Bestelling()
                {
                    AantalAangekochteCheques = 15,
                    Elektronisch = true
                };
                b.StelDatumsIn(DateTime.Today.AddMonths(-4 * i), DateTime.Today.AddMonths(-4 * i));
                Jan.Bestellingen.Add(b);
                for (int j = 1; j <= 15; j++)
                {
                    DienstenCheque d = new DienstenCheque(true, DateTime.Today.AddMonths(-4 * i));
                    if (p < 11)
                    {
                        d.Prestatie = Jan.GetPrestatie(p);
                        d.GebruiksDatum = d.Prestatie.DatumPrestatie;
                    }
                    Jan.Portefeuille.Add(d);
                    if (pi < 3)
                    {
                        pi++;
                    }
                    else
                    {
                        pi = 0;
                        p++;
                    }
                }
            }

            //alle cheques zijn toegewezen aan prestaties, geen openstaande prestaties meer
            An = new Gebruiker() { GebruikersNummer = 1000000001, Naam = "Pieters", Voornaam = "An", Email = "an.pieters@hogent.be" };
            Bestelling anBestelling = new Bestelling() { AantalAangekochteCheques = 20, Elektronisch = true };
            anBestelling.StelDatumsIn(DateTime.Today.AddMonths(-1), DateTime.Today.AddMonths(-1));
            An.Bestellingen.Add(anBestelling);
            for (int i = 4; i > 0; i--)
                An.Prestaties.Add(new Prestatie() { AantalUren = 5, DatumPrestatie = DateTime.Today.AddMonths(-i), PrestatieType = PrestatieType.Schoonmaken, Onderneming = onderneming, Betaald = true });
            for (int j = 0; j <= 19; j++)
            {
                DienstenCheque d = new DienstenCheque(true, DateTime.Today.AddMonths(-1))
                {
                    Prestatie = An.GetPrestatie(j / 5)
                };
                d.GebruiksDatum = d.Prestatie.DatumPrestatie;
                An.Portefeuille.Add(d);
            }

            //nog 2 cheques niet gebruikt, geen openstaande prestaties
            Tine = new Gebruiker() { GebruikersNummer = 1000000002, Naam = "Pieters", Voornaam = "Tine", Email = "tine.pieters@hogent.be" };
            Bestelling tineBestelling = new Bestelling() { AantalAangekochteCheques = 6, Elektronisch = true };
            tineBestelling.StelDatumsIn(DateTime.Today.AddMonths(-1), DateTime.Today.AddMonths(-1));
            Tine.Bestellingen.Add(tineBestelling);
            Tine.Prestaties.Add(new Prestatie() { AantalUren = 4, DatumPrestatie = DateTime.Today.AddDays(-10), PrestatieType = PrestatieType.Schoonmaken, Onderneming = onderneming, Betaald = true });
            for (int j = 1; j <= 6; j++)
            {
                DienstenCheque d = new DienstenCheque(true, DateTime.Today.AddMonths(-1));
                if (j < 5)
                {
                    d.Prestatie = Tine.GetPrestatie(0);
                    d.GebruiksDatum = d.Prestatie.DatumPrestatie;
                }
                Tine.Portefeuille.Add(d);
            }
        }
    }
}
