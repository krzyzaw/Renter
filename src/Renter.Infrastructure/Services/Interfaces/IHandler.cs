using System;
using System.Threading.Tasks;

namespace Renter.Infrastructure.Services.Interfaces
{
    public interface IHandler : IService
    {
        IHandlerTask Run(Func<Task> run);

        IHandlerTaskRunner Validate(Func<Task> validate);

        Task ExecuteAllAsync();
    }
}