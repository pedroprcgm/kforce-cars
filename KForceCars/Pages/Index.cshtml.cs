using KForceCars.Data;
using KForceCars.Models;
using KForceCars.Services;
using KForceCars.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KForceCars.Pages;

public class IndexModel : PageModel
{
    private readonly ICarService _carService;
    public List<CarModel> Cars { get; set; }
    
    public IndexModel(ICarService carService)
    {
        _carService = carService;
        Cars = _carService.GetAsync().OrderBy(x => x.Id).ToList();
    }

    public void OnGet()
    {
    }
    
    public async Task<PartialViewResult> OnGetCarAsync(long id)
    {
        return Partial("Cars/PriceModal", id);
    }
    
    public async Task<PartialViewResult> OnGetDeleteCarAsync(long id)
    {
        return Partial("Cars/_DeleteCar", await _carService.GetByIdAsync(id));
    }

    public async Task<IActionResult> OnPostDeleteCarAsync(long id)
    {
        await _carService.DeleteAsync(id);
        return RedirectToPage("/Index");
    }

    public async Task<IActionResult> OnPostAsync(long id)
    {
        var price = decimal.Parse(Request.Form["price"].ToString());
        var messageKey = $"Message-{id}";
        var statusKey = $"Status-{id}";
        var (isCorrect, realPrice) = await _carService.IsPriceGuessCorrectAsync(id, price);
        if (isCorrect)
        {
            ViewData[messageKey] = $"Great job! The price is ${realPrice}";
            ViewData[statusKey] = true;
        }
        else
        {
            ViewData[messageKey] = $"Not this time! :( \n The price is ${realPrice}";
            ViewData[statusKey] = false;
        }

        return Page();
    }
}