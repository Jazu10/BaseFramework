using Backend.Core.Data.Entities;

namespace Backend.Core.RepositoryInterface
{
    public interface IEmployeeRepository
    {
        public Task<List<NewsModel>> GetAllNews();
        public Task<List<NewsModel>> GetUsersNews(string userId);
        public Task UpdateNews(NewsModel model);
        public Task DeleteNews(string newsId);

    }
}
