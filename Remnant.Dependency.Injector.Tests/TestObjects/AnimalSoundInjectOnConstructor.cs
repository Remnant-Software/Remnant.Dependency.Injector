using Remnant.Dependency.Injector;

namespace Remnant.Dependeny.Injector.Tests.TestObjects
{
	public class AnimalSoundInjectOnConstructor
	{
		private readonly IAnimal _animal;

		public AnimalSoundInjectOnConstructor()
		{
			_animal = this.Resolve<IAnimal>();
		}

		public string MakeSound() => _animal.Sound;
	}
}
