﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Renter.Api.Controllers;
using Renter.Api.Extensions;
using Renter.Api.RabbitMq;
using Renter.Infrastructure.Commands;
using Renter.Infrastructure.Message.Commands;

namespace Renter.Publisher.Controllers
{
    public class DiscountController : ApiControllerBase
    {
        private readonly IBusPublisher _busPublisher;

        public DiscountController(ICommandDispatcher commandDispatcher, IBusPublisher busPublisher) : base(commandDispatcher)
        {
            _busPublisher = busPublisher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateDiscount command)
            => await SendAsync(command.BindId(c => c.Id).Bind(c => c.CustomerId, UserId),
                resourceId: command.Id, resource: "discounts");

        private async Task<IActionResult> SendAsync<T>(T command,
            Guid? resourceId = null, string resource = "") where T : ICommand
        {
            var context = GetContext<T>(resourceId, resource);
            await _busPublisher.SendAsync(command, context);

            return Accepted(context);
        }

        protected ICorrelationContext GetContext<T>(Guid? resourceId = null, string resource = "") where T : ICommand
        {
            if (!string.IsNullOrWhiteSpace(resource))
            {
                resource = $"{resource}/{resourceId}";
            }

            return CorrelationContext.Create<T>(Guid.NewGuid(), UserId, resourceId ?? Guid.Empty,
                HttpContext.TraceIdentifier, HttpContext.Connection.Id, "tracer",
                Request.Path.ToString(), "pl-Pl", resource);


        }
    }
}