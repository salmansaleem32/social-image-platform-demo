using Beamable.Server;

namespace Beamable.VotingMicroservice
{
	[Microservice("VotingMicroservice")]
	public partial class VotingMicroservice : Microservice
	{
		[ClientCallable]
		public int Add(int a, int b)
		{
			return a + b;
		}
	}
}
