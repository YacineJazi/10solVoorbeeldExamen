using System;
using System.ComponentModel.DataAnnotations;

namespace DienstenCheques.Models.ViewModels.BestellingenViewModels
{
    public class NieuwViewModel
    {
        public bool Elektronisch { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Aantal dienstencheques")]
        [Range(1, 50, ErrorMessage = "U kan {1} tot {2} dienstencheques bestellen")]
        public int AantalCheques { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum debitering")]
        public DateTime DebiteerDatum { get; set; }

        public NieuwViewModel()
        {
            DebiteerDatum = DateTime.Today;
            Elektronisch = true;
            AantalCheques = 20;
        }
    }
}
