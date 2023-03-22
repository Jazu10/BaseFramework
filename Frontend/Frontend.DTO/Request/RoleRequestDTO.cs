using System.ComponentModel;


namespace Frontend.DTO.Request
{
    public class RoleRequestDTO
    {
        [DisplayName("Role ID")]
        public string? Id { get; set; }

        [DisplayName("Role Name")]
        public string Name { get; set; }
    }
}
