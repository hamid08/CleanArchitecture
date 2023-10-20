namespace CleanArchitecture.Application.Features.Authentication.Queries.Login;

public class LoginQuery : IRequest<UserDto>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}