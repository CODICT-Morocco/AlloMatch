using System.Collections.Generic;

namespace AlloMatch.DTOs
{
    public record AccessTokenDto(string Value, int Expiration);
    public record UserProfileDto(long Id, string FirstName, string LastName, string Email, IList<string> Roles);
}
