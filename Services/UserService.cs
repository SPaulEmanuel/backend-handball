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
using FluentValidation;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    Task<List<UserDto>> GetAll();
    Task<Users> AddUser(Users model);
    Task<Users> UpdateUser(Guid id, Users updatedUser);
    Users GetById(Guid id);
    Task<Users> DeleteUser(Guid id);

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

    public async Task<Users> AddUser(Users model)
    {
        var validator = new UserValidator();
        validator.ValidateAndThrow(model);
        model.Id = Guid.NewGuid();
        _aplicatieDBContext.Users.Add(model);
        await _aplicatieDBContext.SaveChangesAsync();

        return model;
    }

    public async Task<Users> UpdateUser(Guid id, Users updateUserReq)
    {
        var user = await _aplicatieDBContext.Users.FindAsync(id);
        if (user is not null)
        {
            //var validator = new PlayerValidator();
            //validator.ValidateAndThrow(updatePlayerReq);
            user.FirstName = updateUserReq.FirstName;
            user.LastName = updateUserReq.LastName;
            user.Username = updateUserReq.Username;
            user.Password = updateUserReq.Password;
            user.UserType = updateUserReq.UserType;
            user.ImageUrl = updateUserReq.ImageUrl;

            await _aplicatieDBContext.SaveChangesAsync();
            return user;
        }
        throw new Exception("ID not found");
    }


    public async Task<List<UserDto>> GetAll()
    {
        var allUsers = await _aplicatieDBContext.Users
                    .Select(user => new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Username = user.Username,
                        UserType = user.UserType,
                        ImageUrl = user.ImageUrl,
                    })
                    .ToListAsync();

        return allUsers;
    }

    public Users GetById(Guid id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }

    public async Task<Users> DeleteUser(Guid id)
    {
        var user = await _aplicatieDBContext.Users.FindAsync(id);
        if (user is not null)
        {
            _aplicatieDBContext.Users.Remove(user);
            await _aplicatieDBContext.SaveChangesAsync();
            return user;
        }
        throw new Exception("Cannot delete this user! User not exist!");

    }

    // helper methods

    private string generateJwtToken(Users user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("Role", user.UserType)}),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}