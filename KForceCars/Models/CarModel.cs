namespace KForceCars.Models;

public class Car
{
    public long Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Doors { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
}