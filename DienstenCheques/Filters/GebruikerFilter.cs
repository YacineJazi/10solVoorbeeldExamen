using DienstenCheques.Models.Domain;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DienstenCheques.Filters
{
    public class GebruikerFilter : ActionFilterAttribute
    {
        private readonly IGebruikersRepository _gebruikerRepository;

        public GebruikerFilter(IGebruikersRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments["gebruiker"] = context.HttpContext.User.Identity.IsAuthenticated ? _gebruikerRepository.GetByEmail(context.HttpContext.User.Identity.Name) : null;
            base.OnActionExecuting(context);
        }
    }
}

