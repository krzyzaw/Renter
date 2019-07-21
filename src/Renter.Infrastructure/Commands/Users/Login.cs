using System;

namespace Renter.Infrastructure.Commands.Users
{
    public class Login : ICommand
    {
        public Guid TokenId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}