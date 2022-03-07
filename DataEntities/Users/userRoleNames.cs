using System;

namespace DataEntities.Users
{
    public static class UserRolesNames
    {
        public enum Roles { Super=1, Therapist=2 };

        public const string Super = "Super";
        public const string Therapist = "Therapist";

        public static string RoleName(int role) { return ((Roles)role).ToString(); }
        public static string RoleName(string roleId) { return ((Roles)Int32.Parse(roleId)).ToString(); }
    }
}
