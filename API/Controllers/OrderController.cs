using System.Threading.Tasks;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost, Route("place")]
        public async Task<IActionResult> PlaceOrder(SimpleOrderRequest request) =>
            Ok(await orderService.PlaceOrderAsync(request));
    }
}
