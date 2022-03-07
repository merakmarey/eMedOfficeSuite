using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataServices
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (UserAuthentication AuthStore = new UserAuthentication())
            {
                var user = AuthStore.ValidateUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "Username or password is incorrect");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Role, DataEntities.Users.UserRolesNames.RoleName(user.roleId)));
                identity.AddClaim(new Claim(ClaimTypes.UserData, user.roleId.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.username));
                context.Validated(identity);
            }
        }
    }
}