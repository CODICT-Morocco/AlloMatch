using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AlloMatch.Entities;

namespace AlloMatch.DTOs

{
    public record OrganisationDto(string Name, string PhoneNumber);
    public record OpeningHourDto(WeekDay weekDay, TimeSpan From, TimeSpan To);
}
