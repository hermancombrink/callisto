using Microsoft.AspNetCore.Http;
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
        Task SaveFile(IFormFile file, string relativePath);

        /// <summary>
        /// The GetFile
        /// </summary>
        /// <param name="path">The <see cref="string"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task GetFile(string path);
    }
}
