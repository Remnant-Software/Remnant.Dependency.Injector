//using Ninject;
//using NUnit.Framework;
//using Remnant.Dependency.Injector;
//using Remnant.Dependency.Ninject;
//using Remnant.Dependeny.Injector.Tests.TestObjects;

//namespace Remnant.Dependeny.Injector.Tests
//{
//	public class TestNinject : DITests
//	{
//		public TestNinject()
//		{
//			var adapter = new NinjectAdapter(new StandardKernel());	
//			SetContainer(Container.Create("MyContainer", adapter));

//			Assert.IsNotNull(Container.InternalContainer<StandardKernel>());
//			Assert.IsNotNull(adapter.Resolve<IAnimal>());
//			Assert.IsNotNull(adapter.Resolve<Dog>());
//		}
//	}
//}