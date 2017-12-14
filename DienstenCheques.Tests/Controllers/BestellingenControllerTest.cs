using System;
using System.Linq;
using DienstenCheques.Controllers;
using DienstenCheques.Models.Domain;
using DienstenCheques.Models.ViewModels.BestellingenViewModels;
using DienstenCheques.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;

namespace DienstenCheques.Tests.Controllers
{
    public class BestellingenControllerTest
    {
        private readonly BestellingenController _controller;
        private readonly Gebruiker _jan;
        private readonly Mock<IGebruikersRepository> _mockGebruikersRepository;
        private readonly NieuwViewModel _model;
        private readonly NieuwViewModel _modelMetFout;

        public BestellingenControllerTest()
        {
            DummyApplicationDbContext context = new DummyApplicationDbContext();
            _mockGebruikersRepository = new Mock<IGebruikersRepository>();
            _jan = context.Jan;
            _controller = new BestellingenController(_mockGebruikersRepository.Object);
            _controller.TempData = new Mock<ITempDataDictionary>().Object;
            _model = new NieuwViewModel()
            {
                Elektronisch = true,
                AantalCheques = 20,
                DebiteerDatum = DateTime.Today
            };
            _modelMetFout = new NieuwViewModel()
            {
                Elektronisch = true,
                AantalCheques = 70,
                DebiteerDatum = DateTime.Today
            };
        }

        #region "Index"

        [Fact]
        public void Index_Given6Months_PassesIndexViewModelInViewResultModel()
        {
            ViewResult result = _controller.Index(_jan) as ViewResult;
            IndexViewModel indexViewModel = (IndexViewModel)result?.Model;
            Assert.Equal(1, indexViewModel?.AantalBeschikbareCheques);
            Assert.Equal(8, indexViewModel?.AantalOpenstaandePrestatieUren);
            Assert.Equal(1, indexViewModel?.Bestellingen.Count());
            Assert.Equal(DateTime.Today.AddMonths(-4), indexViewModel?.Bestellingen.ToArray()[0].CreatieDatum);
        }

        [Fact]
        public void Index_Given12Months_PassesIndexViewModelInViewResultModel()
        {
            ViewResult result = _controller.Index(_jan, 12) as ViewResult;
            IndexViewModel indexViewModel = (IndexViewModel)result?.Model;
            Assert.Equal(1, indexViewModel?.AantalBeschikbareCheques);
            Assert.Equal(8, indexViewModel?.AantalOpenstaandePrestatieUren);
            Assert.Equal(3, indexViewModel?.Bestellingen.Count());
            Assert.Equal(DateTime.Today.AddMonths(-12), indexViewModel?.Bestellingen.Last().CreatieDatum);
        }

        #endregion

        #region Nieuw
        [Fact]
        public void Nieuw_PassesNieuwViewModelInViewResultModel()
        {
            ViewResult result = _controller.Nieuw(_jan) as ViewResult;
            NieuwViewModel nieuwViewModel = result?.Model as NieuwViewModel;
            Assert.Equal(20, nieuwViewModel?.AantalCheques);
            Assert.Equal(DateTime.Today, nieuwViewModel?.DebiteerDatum);
            Assert.True(nieuwViewModel?.Elektronisch);
        }

        [Fact]
        public void Nieuw_PassesZichtWaardeInViewData()
        {
            ViewResult result = _controller.Nieuw(_jan) as ViewResult;
            Assert.Equal(9M, (decimal)result?.ViewData["ZichtWaarde"]);
        }

        #endregion

        #region HttpPost Nieuw
        [Fact]
        public void NieuwPost_Successfull_RedirectsToIndex()
        {
            RedirectToActionResult result = _controller.Nieuw(_jan, _model) as RedirectToActionResult;
            Assert.Equal("Index", result?.ActionName);
        }

        [Fact]
        public void NieuwPost_Successful_AddsBestelling()
        {
            int aantal = _jan.Bestellingen.Count;
            _controller.Nieuw(_jan, _model);
            Assert.Equal(aantal + 1, _jan.Bestellingen.Count);
            _mockGebruikersRepository.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void NieuwPost_Unsuccessful_PassesZichtWaardeInViewData()
        {
            ViewResult result = _controller.Nieuw(_jan, _modelMetFout) as ViewResult;
            Assert.Equal(9M, (decimal)result?.ViewData["ZichtWaarde"]);
        }

        [Fact]
        public void NieuwPost_Unsuccessful_DoesNotAddBestelling()
        {
            _controller.Nieuw(_jan, _modelMetFout);
            Assert.Equal(3, _jan.Bestellingen.Count);
            _mockGebruikersRepository.Verify(m => m.SaveChanges(), Times.Never);
        }

        [Fact]
        public void NieuwPos_Unsuccessful_PassesNieuwViewModelInViewResultModel()
        {
            ViewResult result = _controller.Nieuw(_jan, _modelMetFout) as ViewResult;
            NieuwViewModel nieuwViewModel = (NieuwViewModel)result?.Model;
            Assert.Equal(70, nieuwViewModel?.AantalCheques);
            Assert.Equal(DateTime.Today, nieuwViewModel?.DebiteerDatum);
            Assert.Equal(true, nieuwViewModel?.Elektronisch);
        }
        #endregion
    }
}
