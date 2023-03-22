using System.Security.Claims;

namespace Backend.Core.Data.Entities
{
    public class ClaimStores
    {
        public static List<Claim> claims = new List<Claim>()
        {
            new Claim("Create Employee", "Create Employee"),
            new Claim("Edit Employee","Edit Employee"),
            new Claim("Delete Employee", "Delete Employee")
        };
    }
}
