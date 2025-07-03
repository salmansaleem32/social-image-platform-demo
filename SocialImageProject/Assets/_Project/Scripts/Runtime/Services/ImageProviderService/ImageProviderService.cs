using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public class ImageProviderService : IImageProviderService
    {
        public Task<List<ImageData>> GetImagesAsync()
        {
            // Simulate fetching images from a server or database
            // In a real application, this would involve an API call or database query
            return Task.FromResult(InitializeDemoData());
        }

        private List<ImageData> InitializeDemoData()
        {
            var demoImages = new[]
            {
                new ImageData
                {
                    imageId = "img1",
                    title = "Mountain Landscape",
                    imageUrl = "https://picsum.photos/400/300?random=1",
                    voteCount = 5
                },
                new ImageData
                {
                    imageId = "img2",
                    title = "City Skyline",
                    imageUrl = "https://picsum.photos/400/300?random=2",
                    voteCount = 3
                },
                new ImageData
                {
                    imageId = "img3",
                    title = "Ocean View",
                    imageUrl = "https://picsum.photos/400/300?random=3",
                    voteCount = 8
                },
                new ImageData
                {
                    imageId = "img4",
                    title = "Forest Path",
                    imageUrl = "https://picsum.photos/400/300?random=4",
                    voteCount = 12
                },
                new ImageData
                {
                    imageId = "img5",
                    title = "Desert Sunset",
                    imageUrl = "https://picsum.photos/400/300?random=5",
                    voteCount = 7
                },
                new ImageData
                {
                    imageId = "img6",
                    title = "Northern Lights",
                    imageUrl = "https://picsum.photos/400/300?random=6",
                    voteCount = 15
                },
                new ImageData
                {
                    imageId = "img7",
                    title = "Tropical Beach",
                    imageUrl = "https://picsum.photos/400/300?random=7",
                    voteCount = 9
                },
                new ImageData
                {
                    imageId = "img8",
                    title = "Urban Architecture",
                    imageUrl = "https://picsum.photos/400/300?random=8",
                    voteCount = 4
                },
                new ImageData
                {
                    imageId = "img9",
                    title = "Wildflower Meadow",
                    imageUrl = "https://picsum.photos/400/300?random=9",
                    voteCount = 11
                },
                new ImageData
                {
                    imageId = "img10",
                    title = "Snowy Mountains",
                    imageUrl = "https://picsum.photos/400/300?random=10",
                    voteCount = 6
                },
                new ImageData
                {
                    imageId = "img11",
                    title = "Sunrise Lake",
                    imageUrl = "https://picsum.photos/400/300?random=11",
                    voteCount = 10
                },
                new ImageData
                {
                    imageId = "img12",
                    title = "Countryside Road",
                    imageUrl = "https://picsum.photos/400/300?random=12",
                    voteCount = 2
                },
                new ImageData
                {
                    imageId = "img13",
                    title = "Autumn Leaves",
                    imageUrl = "https://picsum.photos/400/300?random=13",
                    voteCount = 13
                },
                new ImageData
                {
                    imageId = "img14",
                    title = "Foggy Morning",
                    imageUrl = "https://picsum.photos/400/300?random=14",
                    voteCount = 5
                },
                new ImageData
                {
                    imageId = "img15",
                    title = "River Rapids",
                    imageUrl = "https://picsum.photos/400/300?random=15",
                    voteCount = 8
                },
                new ImageData
                {
                    imageId = "img16",
                    title = "Lavender Fields",
                    imageUrl = "https://picsum.photos/400/300?random=16",
                    voteCount = 7
                },
                new ImageData
                {
                    imageId = "img17",
                    title = "Starry Night",
                    imageUrl = "https://picsum.photos/400/300?random=17",
                    voteCount = 14
                },
                new ImageData
                {
                    imageId = "img18",
                    title = "Rainy Window",
                    imageUrl = "https://picsum.photos/400/300?random=18",
                    voteCount = 3
                },
                new ImageData
                {
                    imageId = "img19",
                    title = "Vast Canyon",
                    imageUrl = "https://picsum.photos/400/300?random=19",
                    voteCount = 9
                },
                new ImageData
                {
                    imageId = "img20",
                    title = "Cherry Blossoms",
                    imageUrl = "https://picsum.photos/400/300?random=20",
                    voteCount = 12
                }
            };

            // foreach (var img in demoImages)
            // {
            //     _images[img.imageId] = img;
            // }

            return new List<ImageData>(demoImages);
        }
    }
}