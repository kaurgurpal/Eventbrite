﻿using Common.Messaging;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CartApi.Models;
using MassTransit;

namespace CartApi.Messaging.Consumers
{
    public class OrderCompletedEventConsumer : IConsumer<OrderCompletedEvent>
    {
        private readonly ICartRepository _repository;
        private readonly ILogger<OrderCompletedEventConsumer> _logger;
        public OrderCompletedEventConsumer(ICartRepository repository, ILogger<OrderCompletedEventConsumer> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderCompletedEvent> context)
        {
            _logger.LogWarning("We are in consume method now...");
            _logger.LogWarning("BuyerId:" +context.Message.BuyerId);
            return _repository.DeleteCartAsync(context.Message.BuyerId);
             
        }
    }
}
