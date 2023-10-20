using CleanArchitecture.Application.Common.MediatR.Contracts;

namespace CleanArchitecture.Application.Features.Users.Queries.GetUser;

public record GetUserQuery : IRequest<GetUserDto>
{
    public Guid Id { get; set; }
}