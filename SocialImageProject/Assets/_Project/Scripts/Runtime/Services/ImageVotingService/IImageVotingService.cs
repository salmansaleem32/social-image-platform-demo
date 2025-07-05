using Beamable.Runtime.LightBeams;
using Models;
using System.Threading.Tasks;

namespace Services
{
    public interface IImageVotingService : ILightComponent
    {
        Task<ImageData> VoteAsync(ImageData imageId);
    }
}
