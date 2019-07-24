using System.Threading.Tasks;
using Renter.Core.Repositories;
using Renter.Core.Domain;
using Renter.Infrastructure.Commands;
using Renter.Infrastructure.Message.Commands;

namespace Renter.Infrastructure.Handlers.Discount
{
    public class CreateDiscountHandler : ICommandHandler<CreateDiscount>
    {
        private readonly IDiscountsRepository _discountsRepository;

        public CreateDiscountHandler(IDiscountsRepository discountsRepository)
        {
            _discountsRepository = discountsRepository;
        }

        public async Task HandleAsync(CreateDiscount command, ICorrelationContext correlationContext)
        {
            // custom validation

            // _logger.LogWarning($"Customer with id: '{command.CustomerId}' was not found.");
            // await _busPublisher.PublishAsync(new CreateDiscountRejected(command.CustomerId,
            //     $"Customer with id: '{command.CustomerId}' was not found.",
            //     "customer_not_found"), context);

            // return;

            var discount = new Core.Domain.Discount(command.Id, command.CustomerId,
                command.Code, command.Percentage);
            await _discountsRepository.AddAsync(discount);

            //await _busPublisher.PublishAsync(new DiscountCreated(command.Id,
            //    command.CustomerId, command.Code, command.Percentage), context);
            // Send an email about a new discount to the customer

        }
    }
}