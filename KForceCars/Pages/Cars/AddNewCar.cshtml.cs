using KForceCars.Data;
using KForceCars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KForceCars.Pages.Cars;

public class AddNewCarModel : PageModel
{
    private readonly CarDbContext _dbContext;
    
    [BindProperty]
    public CarModel Car { get; set; }
    
    public AddNewCarModel(CarDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        _dbContext.Car.Add(Car);
        await _dbContext.SaveChangesAsync();
        
        return RedirectToPage("/Index");
    }
}