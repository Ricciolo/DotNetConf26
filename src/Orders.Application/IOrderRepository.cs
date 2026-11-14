using Orders.Domain;

namespace Orders.Application;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order?> FindAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Order>> ListAsync(CancellationToken cancellationToken = default);
}
