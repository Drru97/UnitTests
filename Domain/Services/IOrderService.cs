using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services
{
    public interface IOrderService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        Task<OrderResult> PlaceOrderAsync(OrderRequest request, CancellationToken cancellationToken = default);
    }
}
