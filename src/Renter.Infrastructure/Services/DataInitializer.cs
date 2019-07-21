using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Renter.Infrastructure.Services.Interfaces;

namespace Renter.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
       // private readonly ILogger _logger;

        public DataInitializer(IUserService userService)
        {
            //_logger = logger;
            _userService = userService;
        }

        public async Task SeedAsync()
        {
           // _logger.LogTrace("Initializing data...");
            var tasks = new List<Task>();

            for (int i = 1; i <= 10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";
                tasks.Add(_userService.RegisterAsync(userId, $"{username}@mail.com", username, $"secret{i}", "user"));
            }

            for (int i = 1; i <= 3; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"admin{i}";
                tasks.Add(_userService.RegisterAsync(userId, $"{username}@mail.com", username, $"secret{i}", "admin"));
            }

            await Task.WhenAll(tasks);
           // _logger.LogTrace("Data was initialized.");
        }
    }
}