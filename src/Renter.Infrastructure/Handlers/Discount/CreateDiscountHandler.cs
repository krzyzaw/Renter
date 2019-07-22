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
            var discount = new Core.Domain.Discount(command.Id, command.CustomerId,
                command.Code, command.Percentage);
            await _discountsRepository.AddAsync(discount);


        }
    }
}