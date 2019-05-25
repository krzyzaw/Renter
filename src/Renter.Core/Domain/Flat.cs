using System;
using System.Collections.Generic;

namespace Renter.Core.Domain
{
    public class Flat
    {
        public Guid Id { get; protected set; }

        public string BuildingType { get; protected set; } // enum -> apartment, block, etc

        public string Name { get; protected set; }

        public IEnumerable<Room> Rooms { get; set; }

        public double Size { get; protected set; }

        public Address Address { get; protected set; }
    }
}