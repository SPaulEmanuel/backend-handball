using aplicatieHandbal.Models;

namespace aplicatieHandbal.Helpers;


public class AuthenticateResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; }
    public string ImageUrl { get; set; }


    public AuthenticateResponse(Users user, string token)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Token = token;
        ImageUrl = user.ImageUrl;
    }
}