using System;
using System.Collections.Generic;
using Renter.Core.Enums;

namespace Renter.Core.Domain
{
    public class Flat
    {
        public Guid Id { get; protected set; }

        public BuildingType BuildingType { get; protected set; }

        public string Name { get; protected set; }

        public IEnumerable<Room> Rooms { get; set; }

        public double Size { get; protected set; }

        public Address Address { get; protected set; }
    }
}