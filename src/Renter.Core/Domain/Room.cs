using System;
using System.Collections.Generic;

namespace Renter.Core.Domain
{
    public class Room
    {
        public Guid Id { get; protected set; }

        public double Size { get; protected set; }

        public string RoomType { get; protected set; } // single, double, etc -> enum

        public List<string> Photos { get; protected set; }
    }
}