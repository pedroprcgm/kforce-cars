using KForceCars.Data;
using KForceCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KForceCars.Pages;

public class IndexModel : PageModel
{
    private const int AcceptMarginErrorPrice = 5000;
    private readonly CarDbContext _dbContext;
    public List<CarModel> Cars { get; set; }
    
    public IndexModel(CarDbContext dbContext)
    {
        _dbContext = dbContext;
        Cars = _dbContext.Car.ToList();
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
        return Partial("Cars/_DeleteCar", _dbContext.Car.FirstOrDefault(x => x.Id == id));
    }

    public async Task<IActionResult> OnPostDeleteCarAsync(long id)
    {
        var car = await _dbContext.Car.FirstOrDefaultAsync(x => x.Id == id);
        _dbContext.Car.Remove(car);
        await _dbContext.SaveChangesAsync();
        return RedirectToPage("/Index");
    }

    public IActionResult OnPost(long id)
    {
        var car = _dbContext.Car.FirstOrDefault(x => x.Id == id);
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