using Beamable;
// using Beamable.Api;
// using Beamable.Common.Api;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beamable.Common;
using Beamable.Player.CloudSaving;
using Beamable.Runtime.LightBeams;
using UnityEngine;
using VContainer;
using System.Threading;

namespace Services
{
    public class ImageVotingService : IImageVotingService
    {
        private LightBeam _ctx;
        private ICloudSavingService _cloudSavingService;

        private long _playerId;

        private VoteDataService _voteDataService;

        [Inject]
        public void Construct(VoteDataService voteDataService)
        {
            _voteDataService = voteDataService;
        }

        public async Task<ImageData> VoteAsync(ImageData imageId)
        {
            var voteCount = await _voteDataService.AddVote(imageId.imageId);
            imageId.voteCount = voteCount;
            Debug.Log($"Vote recorded for image: {imageId} by player: {_playerId}");
            return imageId;
        }

        public Promise OnInstantiated(LightBeam beam)
        {
            Debug.Log($"ImageVotingService instantiated with BeamContext: {beam.BeamContext}");

            _ctx = beam;
            _playerId = beam.BeamContext.PlayerId;
            _cloudSavingService = _ctx.Scope.GetService<ICloudSavingService>();

            return Promise.Success;
        }
    }

}