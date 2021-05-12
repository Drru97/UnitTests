using System;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories;
using Domain.Models;
using Domain.Services;
using NSubstitute;
using UnitTests.Models;
using UnitTests.Stubs;
using Xunit;

namespace UnitTests
{
    public class PizzaOrderServiceTests
    {
        [Fact]
        public async Task PlaceOrderAsync_ThrowsException_WhenRequestIsNotSimpleRequest()
        {
            // Arrange
            var service = new PizzaOrderService(null, null, null, null);

            // Act
            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.PlaceOrderAsync(new NotSimpleOrderRequest()));
        }

        [Fact]
        public async Task PlaceOrderAsync_ShouldAddOrder()
        {
            // Arrange
            var fakeRepository = new FakeOrderRepository();
            var mockNotification = Substitute.For<INotificationService>();
            var mockDateTime = Substitute.For<IDateTimeService>();
            var mockDateThirdParty = Substitute.For<IThirdPartyService>();

            var service = new PizzaOrderService(fakeRepository, mockNotification, mockDateTime, mockDateThirdParty);

            // Act
            await service.PlaceOrderAsync(new SimpleOrderRequest());

            // Assert
            Assert.Equal(1, fakeRepository.Counter);
        }

        [Fact]
        public async Task PlaceOrderAsync_ShouldAddOrder_2()
        {
            // Arrange
            var fakeRepository = Substitute.For<IOrderRepository>();
            var mockNotification = Substitute.For<INotificationService>();
            var mockDateTime = Substitute.For<IDateTimeService>();
            var mockDateThirdParty = Substitute.For<IThirdPartyService>();

            var service = new PizzaOrderService(fakeRepository, mockNotification, mockDateTime, mockDateThirdParty);

            // Act
            await service.PlaceOrderAsync(new SimpleOrderRequest());

            // Assert
            await fakeRepository.Received(1).AddOrderAsync(Arg.Is<Order>(o => o.EstimatedDelivery == TimeSpan.FromMinutes(30)));
        }

        [Fact]
        public async Task PlaceOrderAsync_ShouldReturnNull_WhenAnalyticsMessageContainsReject()
        {
            // Arrange
            var fakeRepository = Substitute.For<IOrderRepository>();
            var mockNotification = Substitute.For<INotificationService>();
            var mockDateTime = Substitute.For<IDateTimeService>();
            var mockDateThirdParty = Substitute.For<IThirdPartyService>();

            mockDateThirdParty.SendRequestToThirdParty(Arg.Any<Guid>()).Returns("reject");

            var service = new PizzaOrderService(fakeRepository, mockNotification, mockDateTime, mockDateThirdParty);

            // Act
            var result = await service.PlaceOrderAsync(new SimpleOrderRequest());

            // Assert
            Assert.Null(result);
            await mockDateThirdParty.Received(1).SendRequestToThirdParty(Arg.Any<Guid>());
        }
    }
}
