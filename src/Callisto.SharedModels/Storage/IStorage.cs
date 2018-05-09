using System;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Storage
{
    /// <summary>
    /// Defines the <see cref="IStorage" />
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// The SaveFile
        /// </summary>
        /// <param name="file">The <see cref="IFormFile"/></param>
        /// <param name="relativePath">The <see cref="string"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task<string> SaveFile(byte[] fileData, string fileName, string reference);

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="fileName">The <see cref="string"/></param>
        /// <param name="reference">The <see cref="string"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task<bool> DeleteAsync(string fileName, string reference);

        /// <summary>
        /// The GetFile
        /// </summary>
        /// <param name="fileName">The <see cref="string"/></param>
        /// <param name="reference">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{Uri}"/></returns>
        Task<Uri> GetFileAsync(string fileName, string reference);
    }
}
