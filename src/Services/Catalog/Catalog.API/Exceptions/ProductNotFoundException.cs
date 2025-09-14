namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException(Guid productId) : Exception($"Product with ID '{productId}' was not found.")
    {
    }
}
