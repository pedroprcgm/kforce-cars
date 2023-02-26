using KForceCars.Data;
using KForceCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

    public void CalculateGuess(long id)
    {
        var car = _dbContext.Car.FirstOrDefault(x => x.Id == id);
        // if (car is null)
        //     return NotFound();
        var price = decimal.Parse(Request.Form["price"].ToString());
        if (IsValueCorrect(car, price))
            ViewData["Message"] = $"Great job! The price is {car.Price} - {car.Id}";
        else 
            ViewData["Message"] = $"Not this time! :( \n Try again  - {car.Id}";
    }
    
    private bool IsValueCorrect(CarModel carModel, decimal price)
        => price <= carModel.Price + AcceptMarginErrorPrice
           && price >= carModel.Price - AcceptMarginErrorPrice;
}