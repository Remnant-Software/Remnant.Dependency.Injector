namespace Remnant.Dependeny.Injector.Tests.TestObjects
{
	public class Dog : IAnimal
	{
		public string Name { get; set; }	
		public string Sound => "Woof!";
	}
}
