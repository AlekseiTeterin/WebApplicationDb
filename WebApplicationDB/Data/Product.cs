namespace WebApplicationDB.Data;

public class Product
{
    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
    protected Product()
    {

    }
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; } = 100m;
}
