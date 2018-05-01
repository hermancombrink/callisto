using Callisto.SharedKernel;
using Callisto.SharedModels.Assets.ViewModels;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Asset
{
    /// <summary>
    /// Defines the <see cref="IAssetsModule" />
    /// </summary>
    public interface IAssetsModule
    {
        Task<RequestResult> AddAssetAsync(AssetAddViewModel model);
    }
}
