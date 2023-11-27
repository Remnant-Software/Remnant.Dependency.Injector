using Remnant.Dependency.Injector;

namespace Remnant.Dependeny.Injector.Tests.TestObjects
{

	/// <summary>
	/// Note the class must be specified as partial
	/// </summary>
	public partial class AnimalSoundInjectUsingAttr
	{
		[Inject]
		private readonly IAnimal _animal;

		public string MakeSound() => _animal.Sound;
	}
}
