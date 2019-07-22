using System;
using System.Threading.Tasks;

namespace Renter.Infrastructure.Services.Interfaces
{
    public interface IHandlerTaskRunner : IService
    {
        IHandlerTask Run(Func<Task> run);   
    }
}