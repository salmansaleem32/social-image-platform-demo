using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class ImageGalleryView : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform imageContainer;
    [SerializeField] private GameObject imageItemPrefab;
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private Button refreshButton;

    private ImageGalleryViewModel _viewModel;
    private List<ImageItemView> _imageItems = new List<ImageItemView>();

    public void Initialize(ImageGalleryViewModel viewModel)
    {
        _viewModel = viewModel;
        _viewModel.PropertyChanged += OnViewModelPropertyChanged;

        refreshButton.onClick.AddListener(RefreshImages);

        // Load images on start
        RefreshImages();
    }

    private async void RefreshImages()
    {
        await _viewModel.LoadImagesAsync();
    }

    private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_viewModel.Images):
                UpdateImageDisplay();
                break;
            case nameof(_viewModel.IsLoading):
                break;
            case nameof(_viewModel.ErrorMessage):
                errorText.text = _viewModel.ErrorMessage ?? "";
                errorText.gameObject.SetActive(!string.IsNullOrEmpty(_viewModel.ErrorMessage));
                break;
        }
    }

    private void UpdateImageDisplay()
    {
        // Clear existing items
        foreach (var item in _imageItems)
        {
            if (item != null)
                Destroy(item.gameObject);
        }
        _imageItems.Clear();

        // Create new items
        foreach (var imageData in _viewModel.Images)
        {
            var itemGO = Instantiate(imageItemPrefab, imageContainer);
            var itemView = itemGO.GetComponent<ImageItemView>();
            itemView.Initialize(imageData, _viewModel);
            _imageItems.Add(itemView);
        }
    }

    private void OnDestroy()
    {
        if (_viewModel != null)
            _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
    }
}