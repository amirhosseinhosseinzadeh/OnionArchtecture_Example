using Onion.Libraries.Domain.Bases;
using Onion.Libraries.Domain.Security;
using Onion.Libraries.Domain.Users;

namespace Onion.Domain.Security
{
    public class User_Role_MiddleTable : BaseEntity
    {
        public int CustomerId { get; set; }

        public int RoleId { get; set; }

        public int UserId { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }

    }
}
