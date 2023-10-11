namespace Chinook.Services
{
    public class UserService : IUserService
    {
        private readonly ChinookContext DbContext;


        public UserService(ChinookContext dbContext)
        {
            this.DbContext = dbContext;
        }
        public Task<string> GetUserId()
        {
            throw new NotImplementedException();
        }

    }
}
