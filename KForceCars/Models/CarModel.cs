using Microsoft.EntityFrameworkCore;

namespace KForceCars.Models;

[PrimaryKey("Id")]
public class CarModel
{
    public long Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Doors { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
}