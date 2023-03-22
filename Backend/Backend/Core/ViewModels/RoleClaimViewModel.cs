using Backend.Core.Data.Entities;

namespace Backend.Core.ViewModels
{
    public class RoleClaimViewModel
    {
        public RoleClaimViewModel()
        {
            userClaims = new List<UserClaim>();
        }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<UserClaim> userClaims { get; set; }
    }
}
