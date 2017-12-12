using System;
using DienstenCheques.Filters;
using DienstenCheques.Models.Domain;
using DienstenCheques.Models.ViewModels.BestellingenViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DienstenCheques.Controllers
{
    [ServiceFilter(typeof(GebruikerFilter))]
    public class BestellingenController : Controller
    {
        public ActionResult Index(Gebruiker gebruiker, int aantalMaanden=6)
        {
            throw new NotImplementedException();
        }

        public ActionResult Nieuw(Gebruiker gebruiker)
        {
            ViewData["ZichtWaarde"] = Bestelling.Bedragcheque;
            return View(new NieuwViewModel());
        }

        [HttpPost]
        public ActionResult Nieuw(Gebruiker gebruiker, NieuwViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
