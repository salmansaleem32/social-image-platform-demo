using Beamable.Server;
using System.Threading.Tasks;

namespace Beamable.VotingMicroservice
{
	public class Program
	{
		/// <summary>
		/// The entry point for the <see cref="VotingMicroservice"/> service.
		/// </summary>
		public static async Task Main()
		{
			// inject data from the CLI.
			await MicroserviceBootstrapper.Prepare<VotingMicroservice>();
			
			// run the Microservice code
			await MicroserviceBootstrapper.Start<VotingMicroservice>();
		}
	}
}
