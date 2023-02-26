using KForceCars.Data;
using KForceCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KForceCars.Pages;

public class IndexModel : PageModel
{
    private readonly CarDbContext _db;
    private const int AcceptMarginErrorPrice = 5000;
    public List<CarModel> Cars { get; set; }
    
    public IndexModel(CarDbContext dbContext)
    {
        _db = dbContext;
        Cars = _db.Car.ToList();
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        var id = int.Parse(Request.Form["id"].ToString());
        var car = Cars.FirstOrDefault(x => x.Id == id);
        if (car is null)
            return NotFound();
        var price = decimal.Parse(Request.Form["price"].ToString());
        if (IsValueCorrect(car, price))
            ViewData[$"Message-{car.Id}"] = $"Great job! The price is {car.Price} - {car.Id}";
        else 
            ViewData[$"Message-{car.Id}"] = $"Not this time! :( \n Try again  - {car.Id}";

        return Page();
    }

    private bool IsValueCorrect(CarModel carModel, decimal price)
        => price <= carModel.Price + AcceptMarginErrorPrice
           && price >= carModel.Price - AcceptMarginErrorPrice;
}