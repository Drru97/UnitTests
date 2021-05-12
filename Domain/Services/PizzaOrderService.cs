using System;
using System.Threading;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories;
using Domain.Models;

namespace Domain.Services
{
    public class PizzaOrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly INotificationService notificationService;
        private readonly IDateTimeService dateTimeService;
        private readonly IThirdPartyService analyticsService;

        public PizzaOrderService(IOrderRepository orderRepository, INotificationService notificationService,
            IDateTimeService dateTimeService, IThirdPartyService analyticsService)
        {
            this.orderRepository = orderRepository;
            this.notificationService = notificationService;
            this.dateTimeService = dateTimeService;
            this.analyticsService = analyticsService;
        }

        public async Task<OrderResult> PlaceOrderAsync(OrderRequest request,
            CancellationToken cancellationToken = default)
        {
            if (request is not SimpleOrderRequest pizzaRequest)
            {
                throw new InvalidOperationException($"Only SimpleOrderRequest is supported ATM.");
            }

            var order = new Order
            {
                Item = pizzaRequest.Item,
                Count = pizzaRequest.Count,
                EstimatedDelivery = TimeSpan.FromMinutes(30),
                PlacedDate = dateTimeService.GetUtc()
            };
            await orderRepository.AddOrderAsync(order, cancellationToken);

            var orderResult = new SimpleOrderResult(order);

            var message = await analyticsService.SendRequestToThirdParty(orderResult.OrderId);
            if (message.Contains("reject"))
            {
                return null;
            }

            var notification = new EmailNotification
            {
                From = "pizza@gmail.com",
                To = "client@gmail.com",
                Subject = "Your order was placed",
                Body = message
            };
            await notificationService.SendNotificationAsync(notification, cancellationToken);

            return orderResult;
        }
    }
}
