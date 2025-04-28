namespace CatalogoLivros.Application.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public BookSpecificationsDto Specifications { get; set; } = new();
    public decimal FreightPrice { get; set; }  // 20% calculado do preço
}
