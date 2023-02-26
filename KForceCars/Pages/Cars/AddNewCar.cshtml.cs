using KForceCars.Data;
using KForceCars.Models;
using KForceCars.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KForceCars.Pages.Cars;

public class AddNewCarModel : PageModel
{
    private readonly ICarService _carService;
    
    [BindProperty]
    public CarModel Car { get; set; }
    
    public AddNewCarModel(ICarService carService)
    {
        _carService = carService;
    }
    
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        await _carService.CreateAsync(Car);
        return RedirectToPage("/Index");
    }
}