using Beamable;
using Beamable.Common;
using Beamable.Player.CloudSaving;
using Beamable.Runtime.LightBeams;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

namespace Services
{
    [BeamContextSystem]
    public class VoteDataService : MonoBehaviour, ILightComponent
    {
        private const string SaveFileName = "imageVotes.sav";

        private LightBeam _ctx;
        private ICloudSavingService _cloudSavingService;
        private VoteData _voteData = new VoteData();

        Promise ILightComponent.OnInstantiated(LightBeam beam)
        {
            _ctx = beam;
            _cloudSavingService = _ctx.Scope.GetService<ICloudSavingService>();
            Debug.Log($"VoteDataService instantiated with BeamContext: {beam.BeamContext}");
            // LoadVotes(); // Load data on init

            return Promise.Success;
        }

        /// <summary>
        /// Call this to increment the vote count for a given image ID.
        /// </summary>
        public async Task<int> AddVote(string imageId)
        {
            if (!_voteData.ImageVotes.ContainsKey(imageId))
                _voteData.ImageVotes[imageId] = 0;

            _voteData.ImageVotes[imageId]++;

            Debug.Log($"Vote added for image: {imageId}. New count: {_voteData.ImageVotes[imageId]}");
            await SaveVotes();

            return _voteData.ImageVotes[imageId];
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

        public async Task<VoteData> LoadVotes()
        {
            Debug.Log("Loading vote data...");
            _cloudSavingService = GetCloudSavingService();
            await InitCloudSaving();

            Debug.Log ("LocalDataFullPath : " + _cloudSavingService.LocalDataFullPath);

            var resultString = await _cloudSavingService.LoadDataString(SaveFileName);
            var result = JsonConvert.DeserializeObject<VoteData>(resultString);
            if (result != null)
            {
                _voteData = result;
                Debug.Log("Vote data loaded." + 
                          $"ImageVotes count: {_voteData.ImageVotes.Count}");
            }
            else
            {
                _voteData = new VoteData(); // start fresh
                Debug.Log("No vote data found. Starting new.");
            }

            return result;
        }

        private async Task InitCloudSaving()
        {
            var beamContext = BeamContext.Default;
            await beamContext.OnReady;

            Debug.Log($"BeamContext ready: {beamContext.Api.User.id}");

            await _cloudSavingService.Init(1);
            Debug.Log("CloudSavingService initialized.");
        }

        private async Task SaveVotes()
        {
            _cloudSavingService = GetCloudSavingService();
            
            var convertedData = JsonConvert.SerializeObject(_voteData);
            await _cloudSavingService.SaveData(SaveFileName, convertedData);
            Debug.Log("Vote data saved.");
        }

        private ICloudSavingService GetCloudSavingService()
        {
            if (_ctx == null)
            {
                try
                {
                    return Beamable.BeamContext.Default.ServiceProvider.GetService<ICloudSavingService>();
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Failed to get CloudSavingService from BeamContext: {ex.Message}");
                    return null;
                }
            }

            try
            {
                return _ctx.Scope.GetService<ICloudSavingService>();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to get CloudSavingService from LightBeam: {ex.Message}");
                // Fallback: try to get from BeamContext
                try
                {
                    return Beamable.BeamContext.Default.ServiceProvider.GetService<ICloudSavingService>();
                }
                catch (System.Exception ex2)
                {
                    Debug.LogError($"Failed to get CloudSavingService from BeamContext: {ex2.Message}");
                    return null;
                }
            }
        }

        [Serializable]
        public class VoteData
        {
            public Dictionary<string, int> ImageVotes = new Dictionary<string, int>();
        }

    }

}