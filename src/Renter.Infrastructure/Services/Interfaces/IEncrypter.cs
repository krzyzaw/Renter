namespace Renter.Infrastructure.Services.Interfaces
{
    public interface IEncrypter : IService
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);
    }
}