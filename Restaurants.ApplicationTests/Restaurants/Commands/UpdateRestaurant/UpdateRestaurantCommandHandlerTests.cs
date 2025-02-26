using Xunit;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Restaurants.Application.Restaurants.Commands.CreateRestaurantCommand;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using System.Security.AccessControl;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
        private readonly Mock<IRestaurantRepository> _restaurantsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationServiceMock;

        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
            _restaurantsRepositoryMock = new Mock<IRestaurantRepository>();
            _mapperMock = new Mock<IMapper>();
            _restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

            _handler = new UpdateRestaurantCommandHandler(
                _restaurantsRepositoryMock.Object,
                _loggerMock.Object,
                _mapperMock.Object,
                _restaurantAuthorizationServiceMock.Object);
        }

        [Fact()]
        public async Task Handle_WithValidRequest_ShouldUpdateRestaurants()
        {
            // arrange
            var restaurantId = 1;
            var command = new UpdateRestaurantCommand()
            {
                Id = restaurantId,
                Name = "New Test",
                Description = "New Description New Description New Description New Description",
                HasDelivery = true,
            };

            var restaurant = new Restaurant()
            {
                Id = restaurantId,
                Name = "Test",
                Description = "Test Test Test Test TestTest Test",
            };

            _restaurantsRepositoryMock.Setup(r => r.Get(restaurantId))
                .ReturnsAsync(restaurant);

            _restaurantAuthorizationServiceMock.Setup(m => m.Authorize(restaurant, Domain.Constants.ResourceOperation.Update))
                .Returns(true);


            // act
            await _handler.Handle(command, CancellationToken.None);

            // assert

            _restaurantsRepositoryMock.Verify(r => r.SaveChanges(), Times.Once);
            _mapperMock.Verify(m => m.Map(command, restaurant), Times.Once);
        }

        [Fact]
        public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException()
        {
            // Arrange
            var restaurantId = 2;
            var request = new UpdateRestaurantCommand
            {
                Id = restaurantId
            };

            _restaurantsRepositoryMock.Setup(r => r.Get(restaurantId))
                    .ReturnsAsync((Restaurant?)null);

            // act

            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<NotFoundException>()
                    .WithMessage($"Restaurant with id: {restaurantId} doesn't exist");

        }

        [Fact]
        public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
        {
            // / Arrange
            var restaurantId = 3;
            var request = new UpdateRestaurantCommand
            {
                Id = restaurantId
            };

            var existingRestaurant = new Restaurant
            {
                Id = restaurantId
            };

            _restaurantsRepositoryMock
                .Setup(r => r.Get(restaurantId))
                    .ReturnsAsync(existingRestaurant);

            _restaurantAuthorizationServiceMock
                .Setup(a => a.Authorize(existingRestaurant, ResourceOperation.Update))
                    .Returns(false);

            // act

            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ForbidException>();
        }
    }
}