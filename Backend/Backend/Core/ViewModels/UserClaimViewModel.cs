using Backend.Core.Data.Entities;

namespace Backend.Core.ViewModels
{
    public class UserClaimViewModel
    {
        public UserClaimViewModel()
        {
            UserClaims = new List<UserClaim>();
        }
        public string UserId { get; set; }
        public List<UserClaim> UserClaims { get; set; }
    }
}
