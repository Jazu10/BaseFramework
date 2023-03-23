using Backend.Core.Data;
using Backend.Core.Data.Entities;
using Backend.Core.RepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace Backend.Core.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Task<bool> DeleteNews(string newsId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateNews(NewsModel news)
        {
            throw new NotImplementedException();
        }

        public async Task<List<NewsModel>> GetAllNews()
        {
            return await _context.NewsList.Include(item => item.User).Where(item => item.IsDeleted == false).ToListAsync();
        }

        public async Task<List<NewsModel>> GetUsersNews(string userId)
        {
            return await _context.NewsList.Include(item => item.User).Where(item => item.UserId == userId).ToListAsync();
        }

        public async Task<bool> UpdateNews(NewsModel model)
        {
            _context.NewsList.Update(model);
            return true;
        }



        public Task<bool> DeleteAdvertisements(string advertisementId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAdvertisements(AdvertisementModel model)
        {
            var result = await _context.AdvertisementList.AddAsync(model);
            _context.SaveChanges();
            return true;
        }

        public async Task<List<AdvertisementModel>> GetAllAdvertisements()
        {
            return await _context.AdvertisementList.Include(item => item.User).Where(item => item.IsDeleted == false).ToListAsync();
        }

        public async Task<List<AdvertisementModel>> GetUsersAdvertisements(string userId)
        {
            return await _context.AdvertisementList.Include(item => item.User).Where(item => item.UserId == userId).ToListAsync();
        }

        public async Task<bool> UpdateAdvertisements(AdvertisementModel model)
        {
            _context.AdvertisementList.Update(model);
            return true;
        }

        public async Task<List<PostModel>> GetAllPosts()
        {
            return await _context.PostList.Include(item => item.User).Where(item => item.IsDeleted == false).ToListAsync();
        }

        public async Task<List<PostModel>> GetUsersPosts(string userId)
        {
            return await _context.PostList.Include(item => item.User).Where(item => item.UserId == userId).ToListAsync();
        }

        public async Task<bool> CreatePost(PostModel model)
        {
            var result = await _context.PostList.AddAsync(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> UpdatePost(PostModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePost(string newsId)
        {
            throw new NotImplementedException();
        }
    }
}
