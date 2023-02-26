using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KForceCars.Pages.Cars;

public class PriceModalModel : PageModel
{
    public PriceModalModel()
    {
    }

    [BindProperty(SupportsGet = true)]
    public long Id { get; set; }
    
    public void OnGet()
    {
    }
}