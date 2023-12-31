﻿using CleanArchitecture.Application.Common.MediatR;
using CleanArchitecture.Presentation.Filter;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;

[Route("[controller]/[action]"), CatchExceptionFilter, ApiController]
public abstract class BaseController : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
