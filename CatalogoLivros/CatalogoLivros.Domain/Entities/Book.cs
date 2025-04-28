namespace CatalogoLivros.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Specification Specifications { get; set; } = new Specification();

    public decimal CalcularFrete() => Price * 0.20m;
}
