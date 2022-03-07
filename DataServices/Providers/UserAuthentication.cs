using System;
using System.Configuration;
using System.Linq;
using DataEntities.Users;
using DataLog;

namespace DataServices
{
    public class UserAuthentication : IDisposable
    {
        private string encrypted(string plaintext)
        {
            var enthropy = ConfigurationManager.AppSettings.Get("enthropy");

            return plaintext + enthropy;
        }
        public UserIdentity ValidateUser(string username, string password)
        {
            try
            {
                using (var db = new DatabaseEntities())
                {
                    var encryptedPwd = encrypted(password);

                    var query = from u in db.users
                                join r in db.user_roles on u.role equals r.user_role_id
                                where u.email.ToLower() == username.ToLower()
                                        && u.active == true
                                        && u.password == encryptedPwd
                                        && u.active == true
                                select new UserIdentity { username = u.email, roleId = r.user_role_id, userId = u.user_id };

                    var usr = query.ToList<UserIdentity>().FirstOrDefault();
                    if (usr == null)
                        return null;
                    else
                        return usr;
                }
            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return null;
        }
        public void Dispose() { }
    }
}