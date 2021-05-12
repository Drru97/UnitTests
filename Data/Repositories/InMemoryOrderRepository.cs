using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly OrderContext context;

        public InMemoryOrderRepository(OrderContext context)
        {
            this.context = context;
        }

        public Task<Order> GetOrderByIdAsync(int id, CancellationToken cancellationToken = default) =>
            context.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

        public Task<IEnumerable<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult(context.Orders.AsEnumerable());

        public async Task AddOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            await context.Orders.AddAsync(order, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteOrderAsync(int id, CancellationToken cancellationToken = default)
        {
            var order = await GetOrderByIdAsync(id, cancellationToken);
            if (order != null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
