using Onion.Domain.Bases;
using Onion.Domain.Users;
using Onion.Libraries.Domain.Security;

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
