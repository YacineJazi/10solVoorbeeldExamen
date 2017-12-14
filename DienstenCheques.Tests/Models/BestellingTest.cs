using System;
using DienstenCheques.Models.Domain;
using Xunit;

namespace DienstenCheques.Tests.Models
{
    public class BestellingTest
    {
        [Fact]
        public void NewBestelling_Successful_CreatesBestelling()
        {
            Bestelling b = new Bestelling(10, true, DateTime.Today);
            Assert.Equal(10, b.AantalAangekochteCheques);
            Assert.True(b.Elektronisch);
            Assert.Equal(DateTime.Today, b.CreatieDatum);
        }

        [Fact]
        public void NewBestelling_InvalidAantal_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Bestelling(70, true, DateTime.Today));
        }

        [Fact]
        public void NewBestelling_InvalidDebiteerDatum_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Bestelling(-10, true, DateTime.Today.AddMonths(2)));
        }

        [Fact]
        public void TotaalBedrag_ReturnsTheTotal()
        {
            Bestelling b = new Bestelling(10, true, DateTime.Today);
            Assert.Equal(90, b.TotaalBedrag);
        }
    }
}
