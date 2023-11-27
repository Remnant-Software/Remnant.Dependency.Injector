using Remnant.Dependency.Injector;

namespace Remnant.Dependeny.Injector.Tests.TestObjects
{
	/// <summary>
	/// Note the class must be specified as partial, also since the inject fields can't be done on the constructor they can't be readonly
	/// </summary
	public partial class AnimalSoundInjectUsingCreate
	{
		[Inject]
		private IAnimal _animal;

		public AnimalSoundInjectUsingCreate()
		{
			// this blocks the Analyzer to create a constructor, so the generated static method 'Create' must be used which does the injection
		}

		public string MakeSound() => _animal.Sound;
	}
}
