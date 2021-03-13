using AlloMatch.DTOs;
using AlloMatch.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch.Services
{
    public class MediaService : IMediaService
    {
        public string MediasFolder { get; set; } = Path.Combine("Uploads", "Medias");
        public string MediasRequestPath { get; set; } = "/Uploads/Medias";

        private static readonly HashSet<string> ImageExtensions = new HashSet<string>(4)
        {
            "jpg",
            "jpeg",
            "png",
            "gif"
        };
        private static readonly HashSet<string> ImageContentTypes = new HashSet<string>(6)
        {
            "image/jpg",
            "image/jpeg",
            "image/pjpeg",
            "image/gif",
            "image/x-png",
            "image/png"
        };

        public async Task<string> Upload(MediaEntryDto media, string subDirectory = null)
        {
            var filename = $"{Guid.NewGuid()}.{media.Extension}";
            var filepath = Path.Combine(MediasFolder, subDirectory ?? string.Empty, filename);
            await Upload(media.Stream, filepath);
            return filepath;
        }
        public async Task Upload(Stream stream, string filepath)
        {
            await using var fs = new FileStream(filepath, FileMode.Create);
            await stream.CopyToAsync(fs);
        }

        public bool IsImage(MediaEntryDto media)
        {
            if (!ImageExtensions.Contains(media.Extension.ToLowerInvariant()))
                return false;

            if (!ImageContentTypes.Contains(media.ContentType.ToLowerInvariant()))
                return false;

            return true;
        }

        public void Delete(Media media)
        {
            if (!File.Exists(media.FileName))
                return;

            try
            {
                File.Delete(media.FileName);
            }
            catch
            {

            }
        }

        public string GetMediaUrl(Media media) => $"{MediasRequestPath }/{Path.GetFileName(media.FileName)}";
    }
}
