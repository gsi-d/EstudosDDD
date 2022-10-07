using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interfaces.Authentication;

public interface IJwtTokengenerator{
    string GenerateToken(User user);
}