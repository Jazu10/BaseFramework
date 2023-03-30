using System.ComponentModel;

namespace Backend.Core.ViewModels
{
    public class DashbordViewModel
    {
        public int PendingItemsCount { get; set; }

        public int ApprovedItemsCount { get; set; }

        public int RejectedItemsCount { get; set; }
    }
}
