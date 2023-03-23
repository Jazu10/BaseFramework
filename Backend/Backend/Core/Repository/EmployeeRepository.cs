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

        public async Task<bool> DeleteNews(string newsId)
        {
            var result = await _context.NewsList.FindAsync(newsId);

            if (result == null)
                throw new Exception($"No News Found With Id: {newsId}");

            result.IsDeleted = true;
            _context.NewsList.Update(result);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateNews(NewsModel news)
        {
            await _context.NewsList.AddAsync(news);
            _context.SaveChanges();

            return true;
        }

        public async Task<List<NewsModel>> GetAllNews()
        {
            return await _context.NewsList.Include(item => item.User)
                .Where(item => item.IsDeleted == false).ToListAsync();
        }

        public async Task<List<NewsModel>> GetUsersNews(string userId)
        {
            return await _context.NewsList.Include(item => item.User)
                .Where(item => item.UserId == userId && item.IsDeleted == false).ToListAsync();
        }

        public async Task<bool> UpdateNews(NewsModel model)
        {
            _context.NewsList.Update(model);
            _context.SaveChanges();
            return true;
        }


        public async Task<bool> DeleteAdvertisements(string advertisementId)
        {
            var result =  await _context.AdvertisementList.FindAsync(advertisementId);

            if (result == null)
                throw new Exception($"No Advertisement Found With Id: {advertisementId}");

            result.IsDeleted = true;
            _context.AdvertisementList.Update(result);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateAdvertisements(AdvertisementModel model)
        {
            await _context.AdvertisementList.AddAsync(model);
            _context.SaveChanges();

            return true;
        }

        public async Task<List<AdvertisementModel>> GetAllAdvertisements()
        {
            return await _context.AdvertisementList.Include(item => item.User)
                .Where(item => item.IsDeleted == false).ToListAsync();
        }

        public async Task<List<AdvertisementModel>> GetUsersAdvertisements(string userId)
        {
            return await _context.AdvertisementList.Include(item => item.User)
                .Where(item => item.UserId == userId && item.IsDeleted == false).ToListAsync();
        }

        public async Task<bool> UpdateAdvertisements(AdvertisementModel model)
        {
            _context.AdvertisementList.Update(model);
            _context.SaveChanges();
            return true;
        }
    }
}
