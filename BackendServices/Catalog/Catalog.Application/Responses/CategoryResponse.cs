namespace Catalog.Application.Responses;

public class CategoryResponse
{
    public string Id { get; set; }
    public string Name { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}