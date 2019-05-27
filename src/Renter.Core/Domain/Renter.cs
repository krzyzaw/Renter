using System;
using System.Collections.Generic;

namespace Renter.Core.Domain
{
    public class Renter
    {
        public Guid Id { get; protected set; }

        public Guid UserId { get; protected set; }

        public IEnumerable<Flat> Flats { get; protected set; }
    }
}