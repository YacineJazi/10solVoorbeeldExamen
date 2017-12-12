using System;

namespace DienstenCheques.Models.ViewModels.BestellingenViewModels
{
    public class NieuwViewModel
    {
        public bool Elektronisch { get; set; }

        public int AantalCheques { get; set; }

        public DateTime DebiteerDatum { get; set; }

        public NieuwViewModel()
        {
            DebiteerDatum = DateTime.Today;
            Elektronisch = true;
            AantalCheques = 20;
        }
    }
}
