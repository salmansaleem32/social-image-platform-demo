using System.Collections.Generic;
using Beamable.Common;
using Beamable.Player.CloudSaving;
using Beamable.Runtime.LightBeams;
using Models;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class ImageItemView : MonoBehaviour, ILightComponent
{
    [Header("UI References")]
    [SerializeField] private RawImage imageDisplay;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI voteCountText;
    [SerializeField] private Button voteButton;

    private ImageData _imageData;
    private ImageGalleryViewModel _viewModel;

    private LightBeam _ctx;
    private ICloudSavingService _cloudSavingService;

    private string _playerId; 
    
    IImageVotingService _imageService;

    [Inject]
    public void Construct (IImageVotingService imageService)
    {
        _imageService = imageService;
    }
    
    public Promise OnInstantiated(LightBeam beam)
    {
        _ctx = beam;
        _playerId = _ctx.BeamContext.PlayerId.ToString();
            
        return Promise.Success;
    }
    
    public void Initialize(ImageData imageData, ImageGalleryViewModel viewModel)
    {
        _imageData = imageData;
        _viewModel = viewModel;

        titleText.text = imageData.title;
        voteCountText.text = $"Votes: {imageData.voteCount}";

        voteButton.onClick.AddListener(OnVoteClicked);

        // Load image from URL (simplified - in production use proper image loading)
        StartCoroutine(LoadImageFromURL(imageData.imageUrl));
    }

    private async void OnVoteClicked()
    {
        await _viewModel.VoteForImageAsync(_imageData.imageId);
        voteCountText.text = $"Votes: {_imageData.voteCount}";
    }

    private System.Collections.IEnumerator LoadImageFromURL(string url)
    {
        using (var www = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                var texture = UnityEngine.Networking.DownloadHandlerTexture.GetContent(www);
                imageDisplay.texture = texture;
            }
        }
    }
}