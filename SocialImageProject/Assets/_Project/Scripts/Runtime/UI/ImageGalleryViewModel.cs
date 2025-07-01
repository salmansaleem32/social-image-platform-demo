using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Models;
using Services;
using UnityEngine;
using VContainer;

public class ImageGalleryViewModel : INotifyPropertyChanged
{
    private IImageVotingService _imageService;
    private List<ImageData> _images;
    private bool _isLoading;
    private string _errorMessage;
    private IImageProviderService _imageProviderService;
    VoteDataService _voteDataService;

    [Inject]
    public void Construct (IImageVotingService imageService, IImageProviderService imageProviderService, VoteDataService voteDataService)
    {
        _imageService = imageService;
        _imageProviderService = imageProviderService;
        _voteDataService = voteDataService;
        _images = new List<ImageData>();
    }

    public List<ImageData> Images
    {
        get => _images;
        set
        {
            _images = value;
            OnPropertyChanged();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            _isLoading = value;
            OnPropertyChanged();
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    public async Task LoadImagesAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = null;
            Images = await _imageProviderService.GetImagesAsync();

            foreach (var imageData in Images)
            {
                imageData.voteCount = _voteDataService.GetVoteCount(imageData.imageId);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load images: {ex.Message}";
            Debug.LogError(ex);
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task VoteForImageAsync(string imageId)
    {
        try
        {
            var updatedImage = _imageService.VoteAsync(imageId);

            // Update local data
            var index = Images.FindIndex(img => img.imageId == imageId);
            if (index >= 0)
            {
                Images[index] = updatedImage;
                OnPropertyChanged(nameof(Images));
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to vote: {ex.Message}";
            Debug.LogError(ex);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
