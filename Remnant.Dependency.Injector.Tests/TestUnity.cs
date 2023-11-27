//using NUnit.Framework;
//using Remnant.Dependency.Injector;
//using Remnant.Dependency.Unity;
//using Remnant.Dependeny.Injector.Tests.TestObjects;
//using Unity;

//namespace Remnant.Dependeny.Injector.Tests
//{
//	public class TestUnity : DITests
//	{
//		public TestUnity()
//			{
//			var adapter = new UnityAdapter(new UnityContainer());
//			SetContainer(Container.Create("MyContainer", adapter));

//			Assert.IsNotNull(Container.InternalContainer<UnityContainer>());
//			Assert.IsNotNull(adapter.Resolve<IAnimal>());
//			Assert.IsNotNull(adapter.Resolve<Dog>());
//		}
//	}
//}