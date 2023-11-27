using Remnant.Dependency.Injector;

namespace Remnant.Dependeny.Injector.Tests.TestObjects
{

	/// <summary>
	/// Note the class must be specified as partial
	/// </summary>
	public partial class AnimalSoundInjectUsingAttrWithName
	{
		[Inject("Dog")]
		private readonly IAnimal _dog;
		[Inject("Cat")]
		private readonly IAnimal _cat;

		public string MakeSound() => $"dog = {_dog.Sound} and cat = {_cat.Sound}";
	}
}
