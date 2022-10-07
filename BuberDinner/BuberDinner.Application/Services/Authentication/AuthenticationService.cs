using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokengenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokengenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
    // Verfifica se o usuário já existe
    if(_userRepository.GetUserByEmail(email) is not null){
        return Errors.User.DuplicateEmail;
    }

    // Cria um usuário (gerando ID único)
    var user = new User{
        FirstName = firstName,
        LastName = lastName,
        Email = email,
        Password = password
    };

    _userRepository.Add(user);

    // Gera o JWT token
    var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // Valida se o usuário existe
        if(_userRepository.GetUserByEmail(email) is not User user){
            return Errors.Authentication.InvalidCredentials;
        }

        //Valida se a senha é correta
        if(user.Password != password){
            return Errors.Authentication.InvalidCredentials;
        }

        // Gera o JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
    
        return new AuthenticationResult(user, token);
    }
}
