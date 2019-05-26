using System;
using System.Collections.Generic;
using Renter.Core.Enums;

namespace Renter.Core.Domain
{
    public class Room // ValueObject
    {
        public double Size { get; protected set; }

        public RoomType RoomType { get; protected set; }

        public List<string> Photos { get; protected set; }
    }
}