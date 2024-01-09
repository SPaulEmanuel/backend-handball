namespace aplicatieHandbal.Services;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using aplicatieHandbal.Helpers;
using aplicatieHandbal.Models;
using aplicatieHandbal.Data;
using Microsoft.EntityFrameworkCore;
using aplicatieHandbal.Validators;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    Task<List<UserDto>> GetAll();
    /*Task<Users> AddUser(Users model);*/
    Users GetById(int id);
}

public class UserService : IUserService
{
    private List<Users> _users = new List<Users>();

    private readonly AppSettings _appSettings;
    private readonly AplicatieDBContext _aplicatieDBContext;

    public UserService(IOptions<AppSettings> appSettings, AplicatieDBContext dbContext)
    {
        _aplicatieDBContext = dbContext;
        _appSettings = appSettings.Value;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        var user = await _aplicatieDBContext.Users.SingleOrDefaultAsync(x => x.Username == model.Username);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = generateJwtToken(user);

        return new AuthenticateResponse(user,token);
    }

    /*public async Task<Users> AddUsers(Users model)
    {
        var validator = new PlayerValidator();
        validator.ValidateAndThrow(model);
        model.PlayerID = Guid.NewGuid();
        _aplicatieDBContext.Players.Add(model);
        await _aplicatieDBContext.SaveChangesAsync();

        return model;

    }*/

    public async Task<List<UserDto>> GetAll()
    {
        var allUsers = await _aplicatieDBContext.Users
                    .Select(user => new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Username = user.Username,
                        UserType = user.UserType
                    })
                    .ToListAsync();

        return allUsers;
    }

    public Users GetById(int id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }

    // helper methods

    private string generateJwtToken(Users user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}