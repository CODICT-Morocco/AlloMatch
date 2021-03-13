using AlloMatch.DTOs;
using AlloMatch.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch.Services
{
    public interface IMediaService
    {
        /// <summary>
        /// Uploads a media entry with a random name into the specified directory.
        /// </summary>
        /// <param name="media">The media entry</param>
        /// <param name="subDirectory">An optional sub directory</param>
        /// <returns>The file's full path</returns>
        public Task<string> Upload(MediaEntryDto media, string subDirectory = null);

        /// <summary>
        /// Uploads a file stream into the specified path.
        /// </summary>
        /// <param name="stream">The file's stream</param>
        /// <param name="filename">The target full path</param>
        public Task Upload(Stream stream, string filename);

        /// <summary>
        /// Checks if a media is a valid image.
        /// </summary>
        /// <param name="media">The media entry</param>
        public bool IsImage(MediaEntryDto media);

        /// <summary>
        /// Deletes a file that belongs to a media entry.
        /// </summary>
        /// <param name="media">The media entry</param>
        public void Delete(Media media);

        /// <summary>
        /// Gets the url of a media.
        /// </summary>
        /// <param name="media">The media entry</param>
        /// <returns>The url</returns>
        public string GetMediaUrl(Media media);
    }
}
