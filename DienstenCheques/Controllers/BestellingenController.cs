using System;
using DienstenCheques.Filters;
using DienstenCheques.Models.Domain;
using DienstenCheques.Models.ViewModels.BestellingenViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace DienstenCheques.Controllers
{
    [Authorize(Policy = "Customer")]
    [ServiceFilter(typeof(GebruikerFilter))]
    public class BestellingenController : Controller
    {
        private readonly IGebruikersRepository _gebruikersRepository;

        public BestellingenController(IGebruikersRepository gebruikersRepository)
        {
            _gebruikersRepository = gebruikersRepository;
        }

        public ActionResult Index(Gebruiker gebruiker, int aantalMaanden = 6)
        {
            IndexViewModel vm = new IndexViewModel()
            {
                Bestellingen = gebruiker.GetBestellingen(aantalMaanden)
                                             .Select(b => new BestellingViewModel(b)),
                AantalBeschikbareCheques = gebruiker.AantalBeschikbareElektronischeCheques,
                AantalOpenstaandePrestatieUren = gebruiker.AantalOpenstaandePrestatieUren,
                AantalMaanden = aantalMaanden
            };
            return View(vm);
        }

        public ActionResult Nieuw(Gebruiker gebruiker)
        {
            ViewData["ZichtWaarde"] = Bestelling.Bedragcheque;
            return View(new NieuwViewModel());
        }

        [HttpPost]
        public ActionResult Nieuw(Gebruiker gebruiker, NieuwViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Bestelling b = gebruiker.AddBestelling(model.AantalCheques, model.Elektronisch, model.DebiteerDatum);
                    _gebruikersRepository.SaveChanges();
                    TempData["message"] = $"Uw bestelling voor een totaalbedrag van {b.TotaalBedrag:N0} € werd gecreëerd";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewData["ZichtWaarde"] = Bestelling.Bedragcheque;
            return View(model);
        }
    }
}
