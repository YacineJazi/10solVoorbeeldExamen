using System;
using DienstenCheques.Models.Domain;
using Xunit;

namespace DienstenCheques.Tests.Models
{
    public class DienstenChequeTest
    {
        [Fact]
        public void NewDienstenChequeSuccessful_CreatesCheque()
        {
            DienstenCheque dc = new DienstenCheque(true);

            Assert.True(dc.Elektronisch);
            Assert.Equal(DateTime.Today, dc.CreatieDatum);
            Assert.Null(dc.Prestatie);
            Assert.Null(dc.GebruiksDatum);
        }
    }
}
