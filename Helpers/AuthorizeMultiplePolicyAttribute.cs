using Microsoft.AspNetCore.Mvc;

namespace CSU_Suceava_BE.Application.JwtUtils
{
    public class AuthorizeMultiplePolicyAttribute : TypeFilterAttribute
    {
        public AuthorizeMultiplePolicyAttribute(string policies, bool IsAll) : base(typeof(AuthorizeMultiplePolicyFilter))
        {
            Arguments = new object[] { policies, IsAll };
        }
    }
}
