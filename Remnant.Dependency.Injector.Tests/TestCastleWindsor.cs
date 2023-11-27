//using Castle.Windsor;
//using NUnit.Framework;
//using Remnant.Dependency.CastleWindsor;
//using Remnant.Dependency.Injector;
//using Remnant.Dependeny.Injector.Tests.TestObjects;

//namespace Remnant.Dependeny.Injector.Tests
//{
//	public class TestCastleWindsor : DITests
//	{
//		public TestCastleWindsor()
//		{
//			var adapter = new CastleAdapter(new WindsorContainer());
//			SetContainer(Container.Create("MyContainer", adapter));

//			Assert.IsNotNull(Container.InternalContainer<WindsorContainer>());
//			Assert.IsNotNull(adapter.Resolve<IAnimal>());
//			Assert.IsNotNull(adapter.Resolve<Dog>());
//		}
//	}
//}