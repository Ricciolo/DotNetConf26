using Orders.Api.Contracts;
using Orders.Application;
using Orders.Domain;
using Orders.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);
builder.Services.AddScoped<OrderService>();

WebApplication app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

app.MapGet("/orders", async (OrderService service, CancellationToken cancellationToken) =>
{
    IReadOnlyList<Order> orders = await service.ListAsync(cancellationToken);
    return Results.Ok(orders.Select(OrderResponse.From).ToArray());
});

app.MapGet("/orders/{id:guid}", async (Guid id, OrderService service, CancellationToken cancellationToken) =>
{
    Order? order = await service.FindAsync(id, cancellationToken);
    return order is null ? Results.NotFound() : Results.Ok(OrderResponse.From(order));
});

app.MapPost("/orders", async (
    CreateOrderRequest request,
    OrderService service,
    CancellationToken cancellationToken) =>
{
    try
    {
        CreateOrderCommand command = new(
            request.Customer,
            request.Lines?.Select(line =>
                new CreateOrderLine(line.Sku, line.Quantity, line.UnitPrice)).ToArray());

        Order order = await service.CreateAsync(command, cancellationToken);
        return Results.Created($"/orders/{order.Id}", OrderResponse.From(order));
    }
    catch (DomainRuleException exception)
    {
        return Results.Problem(
            title: "Validazione dell'ordine non riuscita",
            detail: exception.Message,
            statusCode: StatusCodes.Status400BadRequest);
    }
});

app.Run();

// Consente l'uso di WebApplicationFactory<Program> nei test di integrazione.
public partial class Program { }
