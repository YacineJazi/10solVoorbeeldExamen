using System;
using System.Collections.Generic;
using System.Linq;

namespace DienstenCheques.Models.Domain
{
    public class Gebruiker
    {
        #region Properties
        public int GebruikersNummer { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Email { get; set; }

        public ICollection<Bestelling> Bestellingen { get; set; }
        public ICollection<DienstenCheque> Portefeuille { get; set; }
        public ICollection<Prestatie> Prestaties { get; set; }
        public int AantalOpenstaandePrestatieUren {
            get { return GetOpenstaandePrestaties().Sum(p => p.AantalUren); }
        }

        public int AantalBeschikbareElektronischeCheques {
            get { return GetBeschikbareElektronischeDienstenCheques().Count(); }
        }

        #endregion

        #region Constructors
        public Gebruiker()
        {
            Bestellingen = new List<Bestelling>();
            Prestaties = new List<Prestatie>();
            Portefeuille = new List<DienstenCheque>();
        }
        #endregion

        #region Methods
        private IEnumerable<Prestatie> GetOpenstaandePrestaties()
        {
            return Prestaties.Where(p => !p.Betaald);
        }

        private IEnumerable<DienstenCheque> GetBeschikbareElektronischeDienstenCheques()
        {
            //Implementeer
            return new List<DienstenCheque>();
        }

        private int GetAantalBesteldeCheques(int jaar)
        {
            //implementeer
            return 0;
        }

        public IEnumerable<Bestelling> GetBestellingen(int aantalMaanden)
        {
            return Bestellingen.Where(b => (DateTime.Today.AddMonths(-aantalMaanden) <= b.CreatieDatum)).OrderByDescending(b => b.CreatieDatum);
        }

        private void BetaalPrestatie(Prestatie p)
        {
            //implementeer
        }

        public Bestelling AddBestelling(int aantalCheques, bool elektronisch, DateTime debiteerDatum)
        {
            //vervolledig implementatie
            Bestelling b = new Bestelling(aantalCheques, elektronisch, debiteerDatum);
            Bestellingen.Add(b);
            return b;
        }

        public Prestatie GetPrestatie(int index)
        {
            return Prestaties.ToList()[index];
        }
        #endregion
    }
}