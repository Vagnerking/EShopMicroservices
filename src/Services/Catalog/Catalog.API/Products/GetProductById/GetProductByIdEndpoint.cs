namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdResponse(Product? Product);

    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetProductByIdQuery(id);
                var result = await sender.Send(query, cancellationToken);

                if(result is null)
                {
                    return Results.NotFound(result.Adapt<ProductNotFoundException>());
                }

                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
        }
    }
}
