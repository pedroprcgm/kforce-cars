using KForceCars.Models;
using KForceCars.Services;
using KForceCars.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KForceCars.Pages.Cars;

public class EditCarModel : PageModel
{
    private readonly ICarService _carService;
    
    [BindProperty(SupportsGet = true)]
    public long Id { get; set; }

    [BindProperty]
    public CarModel Car { get; set; }
    
    public EditCarModel(ICarService carService)
    {
        _carService = carService;
    }
    
    public async Task OnGetAsync()
    {
        Car = await _carService.GetByIdAsync(Id);
    }
    
    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        await _carService.UpdateAsync(Car);
            
        return RedirectToPage("/Index");
    }
}