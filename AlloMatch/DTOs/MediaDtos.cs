using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch.DTOs
{
    public record MediaEntryDto(string FileName,string ContentType, long Size,  Stream Stream, string Extension);
    public record MediaModel(IFormFile File);

}
