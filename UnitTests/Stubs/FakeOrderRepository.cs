using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories;

namespace UnitTests.Stubs
{
    public class FakeOrderRepository : IOrderRepository
    {
        public int Counter = 0;

        public Task<Order> GetOrderByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task AddOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            Counter = 1;
            return Task.CompletedTask;
        }

        public Task DeleteOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
