using System.ComponentModel;

namespace Frontend.Areas.Admin.Models
{
    public class DashbordViewModel
    {

        [DisplayName("Pending Post")]
        public int PendingItemsCount { get; set; }

        [DisplayName("Approved Post")]
        public int ApprovedItemsCount { get; set; }

        [DisplayName("Rejected Post")]
        public int RejectedItemsCount { get; set; }

    }
}
