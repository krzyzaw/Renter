using System;

namespace Renter.Core.Domain
{
    public class Tenant
    {
        public Guid Id { get; protected set; }

        public Guid UserId { get; protected set; }

        public Flat Flat { get; protected set; }
    }
}