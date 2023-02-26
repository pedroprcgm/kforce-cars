using KForceCars.Data;
using KForceCars.Models;
using KForceCars.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KForceCars.Pages;

public class IndexModel : PageModel
{
    private const int AcceptMarginErrorPrice = 5000;
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
        var car = await _carService.GetByIdAsync(id);
        var price = decimal.Parse(Request.Form["price"].ToString());
        var messageKey = $"Message-{car.Id}";
        var statusKey = $"Status-{car.Id}";
        if (IsValueCorrect(car, price))
        {
            ViewData[messageKey] = $"Great job! The price is {car.Price}";
            ViewData[statusKey] = true;
        }
        else
        {
            ViewData[messageKey] = $"Not this time! :( \n Try again";
            ViewData[statusKey] = false;
        }

        return Page();
    }
    
    private static bool IsValueCorrect(CarModel carModel, decimal price)
        => price <= carModel.Price + AcceptMarginErrorPrice
           && price >= carModel.Price - AcceptMarginErrorPrice;
}