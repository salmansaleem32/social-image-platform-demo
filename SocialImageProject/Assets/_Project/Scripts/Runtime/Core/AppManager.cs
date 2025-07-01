using Beamable;
using Beamable.Runtime.LightBeams;
using Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;
public class AppManager : LifetimeScope
{
    [SerializeField] private ImageGalleryView imageGalleryView;
    [SerializeField] private VoteDataService voteDataService;
    [SerializeField] private CanvasGroup loadingBlocker;
    [SerializeField] private RectTransform contentContainer;

    protected override void Configure(IContainerBuilder builder)
    {
        // Register Beamable context
        // builder.Register<BeamContext>(Lifetime.Singleton);

        // Register services
        builder.Register<IImageProviderService, ImageProviderService>(Lifetime.Singleton);
        builder.Register<IImageVotingService, ImageVotingService>(Lifetime.Singleton);

        // Register ViewModels
        builder.Register<ImageGalleryViewModel>(Lifetime.Singleton);

        // Register Views
        builder.RegisterComponent(imageGalleryView);
        builder.RegisterComponent(voteDataService);
    }

    private async void Start()
    {
        // Initialize views with their ViewModels
        var galleryViewModel = Container.Resolve<ImageGalleryViewModel>();

        var beamContext = BeamContext.Default;
        await beamContext.OnReady;

        var lightBeam = await beamContext.CreateLightBeam(contentContainer, loadingBlocker, builder =>
                {
                    builder.AddLightComponent(voteDataService);
                });

        await lightBeam.Scope.Start<VoteDataService>();

        imageGalleryView.Initialize(galleryViewModel);
    }
}



