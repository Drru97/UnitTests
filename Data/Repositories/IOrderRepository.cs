using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
        Task AddOrderAsync(Order order, CancellationToken cancellationToken = default);
        Task DeleteOrderAsync(int id, CancellationToken cancellationToken = default);
    }
}
