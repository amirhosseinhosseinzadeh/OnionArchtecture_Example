using Onion.Domain.Bases;
using Onion.Domain.Security;
using System.Collections.Generic;

namespace Onion.Libraries.Domain.Security
{
    public class Role : BaseEntity
    {

        public Role()
        {
            this._user_Role_MiddleTables = new List<User_Role_MiddleTable>();
        }

        public Role(string roleName)
        {
            this.RoleName = roleName;
        }

        private ICollection<User_Role_MiddleTable> _user_Role_MiddleTables;

        public string RoleName { get; set; }

        public virtual ICollection<User_Role_MiddleTable> Users_Roles_MiddleTable
        {
            get => _user_Role_MiddleTables ?? (_user_Role_MiddleTables = new List<User_Role_MiddleTable>());

            protected set => _user_Role_MiddleTables = value;
        }
    }
}