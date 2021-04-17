using Onion.Domain.Security;
using Onion.Libraries.Domain.Bases;
using System.Collections.Generic;

namespace Onion.Libraries.Domain.Users
{
    public class User : BaseEntity
    {

        public User()
        {
            this._user_Role_MiddleTables = new List<User_Role_MiddleTable>();
        }
        private ICollection<User_Role_MiddleTable> _user_Role_MiddleTables;

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public long PhoneNumber { get; set; }

        public virtual ICollection<User_Role_MiddleTable> User_Role_MiddleTable
        {
            get => _user_Role_MiddleTables ?? (_user_Role_MiddleTables = new List<User_Role_MiddleTable>());

            protected set => _user_Role_MiddleTables = value;
        }
    }
}
