using Beamable;
using Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;
public class AppManager : LifetimeScope
{
    [SerializeField] private ImageGalleryView imageGalleryView;

    protected override void Configure(IContainerBuilder builder)
    {
        // Register Beamable context
        // builder.Register<BeamContext>(Lifetime.Singleton);

        // Register services
        builder.Register<IImageProviderService, ImageProviderService>(Lifetime.Singleton);
        builder.Register<IImageVotingService, ImageVotingService>(Lifetime.Singleton);

        // Register ViewModels
        builder.Register<ImageGalleryViewModel>(Lifetime.Singleton);
        builder.RegisterComponentInHierarchy<VoteDataService>();

        // Register Views
        builder.RegisterComponent(imageGalleryView);
    }

    private void Start()
    {
        // Initialize views with their ViewModels
        var galleryViewModel = Container.Resolve<ImageGalleryViewModel>();
        // var detailViewModel = Container.Resolve<ImageDetailViewModel>();

        imageGalleryView.Initialize(galleryViewModel);
    }
}



