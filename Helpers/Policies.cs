using Microsoft.AspNetCore.Authorization;

namespace aplicatieHandbal.Helpers
{
    public class Policies
    {
        public const string Administrator = "Administrator";
        public const string CreatorDeContinut = "CreatorDeContinut";

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Administrator).Build();
        }

        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(CreatorDeContinut).Build();
        }
    }
}
