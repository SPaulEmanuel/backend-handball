namespace aplicatieHandbal.Models;

using System.Text.Json.Serialization;

public class Users
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }

    /*[JsonIgnore]*/
    public string Password { get; set; }
    public string UserType { get; set; }
    public string ImageUrl { get; set; }
}