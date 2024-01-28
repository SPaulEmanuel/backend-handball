using Microsoft.AspNetCore.Authorization;

namespace aplicatieHandbal.Helpers
{
    public class Policies
    {
        public const string Administrator = "admin";
        public const string Paul = "Paul";
        public const string CreatorDeContinut = "creatorContinut";

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
