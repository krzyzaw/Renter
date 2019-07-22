using System;
using System.Threading.Tasks;
using Renter.Core.Domain;

namespace Renter.Infrastructure.Services.Interfaces
{
    public interface IHandlerTask : IService
    {
        IHandlerTask Always(Func<Task> always);

        IHandlerTask OnCustomError(Func<RenterException, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnError(Func<Exception, Task> onError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnSuccess(Func<Task> onSuccess);

        IHandlerTask PropagateException();

        IHandlerTask DoNotPropagateException();

        IHandler Next();

        Task ExecuteAsync();
    }
}