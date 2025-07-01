using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface IImageProviderService
    {
        public Task<List<ImageData>> GetImagesAsync();
    }
}