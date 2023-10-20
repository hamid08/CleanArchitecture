using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Features.Users.Queries.GetUser;

public class GetUserDto
{
    public Guid Id { get; set; }
    public Gender Gender { get; set; }
    public string Email { get; set; }
}