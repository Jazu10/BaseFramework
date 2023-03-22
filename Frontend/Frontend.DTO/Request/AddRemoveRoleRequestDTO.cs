using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.DTO.Request
{ 
    public class AddRemoveRoles
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<Roles> AddRemove { get; set; }
    }
    public class Roles
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
