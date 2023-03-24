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
            await _context.SaveChangesAsync();

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
            var result = await _context.NewsList.Where(x => x.NewsId == model.NewsId)
                                            .AsNoTracking().FirstOrDefaultAsync();

            if (result == null)
                throw new Exception($"No News Found With Id: {model.NewsId}");

            _context.NewsList.Update(model);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteAdvertisements(string advertisementId)
        {
            var result = await _context.AdvertisementList.FindAsync(advertisementId);

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

            model.Images.ForEach(item =>
            {
                item.AdvertisementId = model.AdvertisementId;
            });

            await _context.ImageList.AddRangeAsync(model.Images);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<AdvertisementModel>> GetAllAdvertisements()
        {
            var result = await (from advertisement in _context.AdvertisementList
                                join
                          user in _context.UserList on advertisement.UserId equals user.UserId
                                select (new AdvertisementModel
                                {
                                    UserId = advertisement.UserId,
                                    AdvertisementId = advertisement.AdvertisementId,
                                    CreatedAt = advertisement.CreatedAt,
                                    Content = advertisement.Content,
                                    IsActive = advertisement.IsActive,
                                    IsDeleted = advertisement.IsDeleted,
                                    Subject = advertisement.Subject,
                                    Images = _context.ImageList
                                                .Where(item => item.AdvertisementId == advertisement.AdvertisementId)
                                                .ToList(),
                                    User = _context.UserList.Include(item => item.User)
                                                .Where(item => item.UserId == advertisement.UserId)
                                                .FirstOrDefault()
                                })).ToListAsync();
            return result;
            //return await _context.AdvertisementList.Include(item => item.User)
            //    .Where(item => item.IsDeleted == false).ToListAsync();
        }

        public async Task<List<AdvertisementModel>> GetUsersAdvertisements(string userId)
        {
            var result = await (from advertisement in _context.AdvertisementList
                                join
                          user in _context.UserList on advertisement.UserId equals user.UserId
                                select (new AdvertisementModel
                                {
                                    UserId = advertisement.UserId,
                                    AdvertisementId = advertisement.AdvertisementId,
                                    CreatedAt = advertisement.CreatedAt,
                                    Content = advertisement.Content,
                                    IsActive = advertisement.IsActive,
                                    IsDeleted = advertisement.IsDeleted,
                                    Subject = advertisement.Subject,
                                    Images = _context.ImageList
                                                .Where(item => item.AdvertisementId == advertisement.AdvertisementId)
                                                .ToList(),
                                    User = _context.UserList.Include(item => item.User)
                                                .Where(item => item.UserId == advertisement.UserId)
                                                .FirstOrDefault()
                                })).Where(item => item.UserId == userId).ToListAsync();
            return result;
        }

        public async Task<AdvertisementModel> GetSingleAdvertisement(string advertisementId)
        {
            var result = await (from advertisement in _context.AdvertisementList
                                join
                          user in _context.UserList on advertisement.UserId equals user.UserId
                                select (new AdvertisementModel
                                {
                                    UserId = advertisement.UserId,
                                    AdvertisementId = advertisement.AdvertisementId,
                                    CreatedAt = advertisement.CreatedAt,
                                    Content = advertisement.Content,
                                    IsActive = advertisement.IsActive,
                                    IsDeleted = advertisement.IsDeleted,
                                    Subject = advertisement.Subject,
                                    Images = _context.ImageList
                                                .Where(item => item.AdvertisementId == advertisement.AdvertisementId)
                                                .ToList(),
                                    User = _context.UserList.Include(item => item.User)
                                                .Where(item => item.UserId == advertisement.UserId)
                                                .FirstOrDefault()
                                })).Where(item => item.AdvertisementId == advertisementId).FirstOrDefaultAsync();

            if (result == null)
                throw new Exception($"No Advertisement Found With This Id {advertisementId}");

            return result;
        }

        public async Task<bool> UpdateAdvertisements(AdvertisementModel model)
        {
            var result = await _context.AdvertisementList.Where(x => x.AdvertisementId == model.AdvertisementId)
                .AsNoTracking().FirstOrDefaultAsync();

            if (result == null)
                throw new Exception($"No Advertisement Found With Id: {model.AdvertisementId}");


            _context.AdvertisementList.Update(model);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
