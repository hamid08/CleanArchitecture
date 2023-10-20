﻿using CleanArchitecture.Application.Common.MediatR.Contracts;

namespace CleanArchitecture.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}