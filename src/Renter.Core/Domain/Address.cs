namespace Renter.Core.Domain
{
    public class Address // ValueObject
    {
        public string ZipCode { get; protected set; }

        public string City { get; protected set; }

        public string Street { get; protected set; }

        public string HouseNumber { get; protected set; }

        public string LocalNumber { get; protected set; }
    }
}