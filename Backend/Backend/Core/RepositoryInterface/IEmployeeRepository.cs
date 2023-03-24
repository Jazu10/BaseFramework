﻿using Backend.Core.Data.Entities;

namespace Backend.Core.RepositoryInterface
{
    public interface IEmployeeRepository
    {
        public Task<List<NewsModel>> GetAllNews();
        public Task<List<NewsModel>> GetUsersNews(string userId);
        public Task<bool> CreateNews(NewsModel model);
        public Task<bool> UpdateNews(NewsModel model);
        public Task<bool> DeleteNews(string newsId);


        public Task<List<AdvertisementModel>> GetAllAdvertisements(string? search);
        public Task<List<AdvertisementModel>> GetUsersAdvertisements(string userId);
        public Task<AdvertisementModel> GetSingleAdvertisement(string advertisementId);
        public Task<bool> CreateAdvertisements(AdvertisementModel model);
        public Task<bool> UpdateAdvertisements(AdvertisementModel model);
        public Task<bool> DeleteAdvertisements(string newsId);


        public Task<List<PostModel>> GetAllPosts(string? search);
        public Task<List<PostModel>> GetUsersPosts(string userId);
        public Task<bool> CreatePost(PostModel model);
        public Task<bool> UpdatePost(PostModel model);
        public Task<bool> DeletePost(string postId);

        //public Task<List<CommentModel>> GetAllComments(string psotId);
        public Task<bool> CreateComment(CommentModel model);
        public Task<bool> DeleteComment(int commentId);


    }
}
