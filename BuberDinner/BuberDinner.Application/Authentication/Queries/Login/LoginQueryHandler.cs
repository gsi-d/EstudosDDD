using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Commands.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokengenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokengenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Valida se o usuário existe
        if(_userRepository.GetUserByEmail(query.Email) is not User user){
            return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
        }

        //Valida se a senha é correta
        if(user.Password != query.Password){
            return Domain.Common.Errors.Errors.Authentication.InvalidCredentials;
        }

        // Gera o JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
    
        return new AuthenticationResult(user, token);
    }
}