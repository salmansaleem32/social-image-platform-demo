using Beamable.Common;
using Beamable.Player.CloudSaving;
using Beamable.Runtime.LightBeams;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Services
{
    [BeamContextSystem]
    public class VoteDataService : MonoBehaviour, ILightComponent
    {
        private const string SaveFileName = "imageVotes.sav";

        private LightBeam _ctx;
        private ICloudSavingService _cloudSavingService;
        private VoteData _voteData = new VoteData();

        public Promise OnInstantiated(LightBeam ctx)
        {
            _ctx = ctx;
            _cloudSavingService = _ctx.Scope.GetService<ICloudSavingService>();
            LoadVotes(); // Load data on init
            
            return Promise.Success; 
        }

        /// <summary>
        /// Call this to increment the vote count for a given image ID.
        /// </summary>
        public async void AddVote(string imageId)
        {
            if (!_voteData.ImageVotes.ContainsKey(imageId))
                _voteData.ImageVotes[imageId] = 0;

            _voteData.ImageVotes[imageId]++;
            await SaveVotes();
        }

        /// <summary>
        /// Get the vote count for a specific image ID.
        /// </summary>
        public int GetVoteCount(string imageId)
        {
            if (_voteData.ImageVotes.TryGetValue(imageId, out int count))
                return count;
            return 0;
        }

        private async void LoadVotes()
        {
            var result = await _cloudSavingService.LoadData<VoteData>(SaveFileName);
            if (result != null)
            {
                _voteData = result;
                Debug.Log("Vote data loaded.");
            }
            else
            {
                _voteData = new VoteData(); // start fresh
                Debug.Log("No vote data found. Starting new.");
            }
        }

        private async Task SaveVotes()
        {
            await _cloudSavingService.SaveData(SaveFileName, _voteData);
            Debug.Log("Vote data saved.");
        }

        [Serializable]
        public class VoteData
        {
            public Dictionary<string, int> ImageVotes = new Dictionary<string, int>();
        }
    }

}