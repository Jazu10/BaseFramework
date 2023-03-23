using Backend.Core.Data.Entities;

namespace Backend.Core.RepositoryInterface
{
    public interface IEmployeeRepository
    {
        public Task<List<NewsModel>> GetAllNews();
        public Task<List<NewsModel>> GetUsersNews(string userId);
        public Task<bool> CreateNews(NewsModel model);
        public Task<bool> UpdateNews(NewsModel model);
        public Task<bool> DeleteNews(string newsId);

        public Task<List<AdvertisementModel>> GetAllAdvertisements();
        public Task<List<AdvertisementModel>> GetUsersAdvertisements(string userId);
        public Task<bool> CreateAdvertisements(AdvertisementModel model);
        public Task<bool> UpdateAdvertisements(AdvertisementModel model);
        public Task<bool> DeleteAdvertisements(string newsId);

        public Task<List<PostModel>> GetAllPosts();
        public Task<List<PostModel>> GetUsersPosts(string userId);
        public Task<bool> CreatePost(PostModel model);
        public Task<bool> UpdatePost(PostModel model);
        public Task<bool> DeletePost(string postId);

    }
}
