namespace IWantApp.Endpoints.Categories;

public class CategoryRequest
{
    public string Name { get; set; }
    public string CreatedBy { get; set; }
    public string EditedBy { get; set; }
    public bool Active { get; set; }
}