namespace BlueWhale.Security.Services
{
    public interface ITokenService
    {
        string Generate(string username, string password);
    }
}
