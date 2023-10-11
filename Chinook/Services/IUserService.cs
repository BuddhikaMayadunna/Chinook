namespace Chinook.Services
{
    public interface IUserService
    {
        Task<string> GetUserId();
    }
}
