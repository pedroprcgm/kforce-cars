using KForceCars.Data;
using KForceCars.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KForceCars.Pages.Cars;

public class EditCarModel : PageModel
{
    private readonly CarDbContext _dbContext;
    
    [BindProperty(SupportsGet = true)]
    public long Id { get; set; }

    [BindProperty]
    public CarModel Car { get; set; }
    
    public EditCarModel(CarDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void OnGet()
    {
        Car = _dbContext.Car.FirstOrDefault(x => x.Id == Id);
    }
    
    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        Car.Id = Id;
        _dbContext.Car.Update(Car);
        await _dbContext.SaveChangesAsync();
        
        return RedirectToPage("/Index");
    }
}