using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Backend.Core.ViewModels
{
    public class RoleViewModel
    {
        public string? Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
