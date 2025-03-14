﻿using CritterWebApi.Data.Models;
using CritterWebApi.Services.Authentication;
using MediatR;

namespace CritterWebApi.Data.Commands.User.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, HttpCookie?>
    {
        private readonly IAuthService _authenticationService;

        public LoginCommandHandler(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<HttpCookie?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authenticationService.Authenticate(request.Username, request.Password, request.RememberMe);
        }
    }
}
