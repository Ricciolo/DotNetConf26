using System.Collections.Concurrent;
using Orders.Application;
using Orders.Domain;

namespace Orders.Infrastructure;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<Guid, Order> _orders = new();

    public Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(order);
        cancellationToken.ThrowIfCancellationRequested();

        if (!_orders.TryAdd(order.Id, order))
        {
            throw new InvalidOperationException("Order already exists.");
        }

        return Task.CompletedTask;
    }

    public Task<Order?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _orders.TryGetValue(id, out Order? order);
        return Task.FromResult(order);
    }

    public Task<IReadOnlyList<Order>> ListAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        IReadOnlyList<Order> orders = _orders.Values
            .OrderBy(order => order.CreatedAtUtc)
            .ThenBy(order => order.Id)
            .ToArray();
        return Task.FromResult(orders);
    }
}
