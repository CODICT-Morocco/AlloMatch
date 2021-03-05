﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlloMatch.DTOs
{
    public record AccessTokenDto(string Value, int Expiration);
    public record UserProfileDto(long Id, string FirstName, string LastName, string Email, IList<string> Roles);
    public record RegisterProfessionalDto(
        [Required][EmailAddress] string Email,
        [Required] string Password,
        [Required] string FirstName,
        [Required] string LastName,
        [Required] string PhoneNumber,
        [Required] string OrganisationName);
}
