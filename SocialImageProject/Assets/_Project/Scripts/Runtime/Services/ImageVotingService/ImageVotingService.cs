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

        public Task<ImageData> VoteAsync(string imageId)
        {
            _voteDataService.AddVote(imageId);

            Debug.Log($"Vote recorded for image: {imageId} by player: {_playerId}");
            return Task.FromResult<ImageData>(null);
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