using Backend.Core.ViewModels;

namespace Backend.Core.Data.Entities
{
    public class UserResult
    {
        public UserResult()
        {
            AddRemove = new List<AddRemoveViewModel>();
        }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public List<AddRemoveViewModel> AddRemove { get; set; }
    }
}
