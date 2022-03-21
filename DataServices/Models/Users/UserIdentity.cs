using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities.Users
{
    [Serializable]
    public class UserIdentity
    {
        public int roleId { get; set; }
        public string username { get; set; }
        public string roleName { get { return Users.UserRolesNames.RoleName(roleId); }  }
        public string token { get; set; }
        public int userId { get; set; }
    }
}
