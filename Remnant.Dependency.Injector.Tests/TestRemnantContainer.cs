using NUnit.Framework;
using Remnant.Dependency.Injector;
using Remnant.Dependency.Interface;
using Remnant.Dependeny.Injector.Tests.TestObjects;
using System;

namespace Remnant.Dependeny.Injector.Tests
{
	public class TestRemnantContainer
	{
		public TestRemnantContainer()
		{
			Assert.IsNotNull(DIContainer.Create("MyContainer"));
		}

		[Test]
		public void Should_be_able_to_create_container()
		{		
			Assert.IsNotNull(DIContainer.InternalContainer<RemnantContainer>());
		}

		[Test]
		public void Should_error_if_container_already_exists()
		{
			Assert.Throws<InvalidOperationException>(() => DIContainer.Create("MyContainer"));
		}

		[Test]
		public void Should_be_able_to_resolve_from_object()
		{
			DIContainer.Clear();
			DIContainer.Register<IAnimal>(new Dog());
			DIContainer.Register<Dog>(new Dog());
			Assert.IsTrue(new Dog().Sound == new object().Resolve<IAnimal>().Sound);
			Assert.IsTrue(new Dog().Sound == new object().Resolve<Dog>().Sound);
		}

		[Test]
		public void Should_be_able_to_inject_on_field_declaration()
		{
			DIContainer.Clear();
			DIContainer.Register<IAnimal>(new Dog());
			DIContainer.Register<Dog>(new Dog());
			var animalSound = new AnimalSoundInjectOnField();
			Assert.IsTrue(new Dog().Sound == animalSound.MakeSound());
			Assert.IsTrue(new Dog().Sound == new object().Resolve<Dog>().Sound);

			DIContainer.Clear();
			DIContainer.Register<IAnimal>(new Cat());
			animalSound = new AnimalSoundInjectOnField();
			Assert.IsTrue(new Cat().Sound == animalSound.MakeSound());
		}

		[Test]
		public void Should_be_able_to_inject_on_constructor_declaration()
		{
			DIContainer.Clear();
			DIContainer.Register<IAnimal>(new Dog());
			var animalSound = new AnimalSoundInjectOnConstructor();
			Assert.IsTrue(new Dog().Sound == animalSound.MakeSound());

			DIContainer.Clear();
			DIContainer.Register<IAnimal>(new Cat());
			animalSound = new AnimalSoundInjectOnConstructor();
			Assert.IsTrue(new Cat().Sound == animalSound.MakeSound());
		}

		[Test]
		public void Should_be_able_to_register_with_just_type_as_transient()
		{
			DIContainer.Clear();
			DIContainer.Register<Dog>(Lifetime.Transient);

			var dog1 = DIContainer.Resolve<Dog>();
			dog1.Name = "Boomer";

			var dog2 = DIContainer.Resolve<Dog>();
			Assert.That(dog1.Name != dog2.Name);
		}

		[Test]
		public void Should_be_able_to_register_with_just_type_as_singleton()
		{
			DIContainer.Clear();
			DIContainer.Register<Dog>(Lifetime.Singleton);

			var dog1 = DIContainer.Resolve<Dog>();
			dog1.Name = "Boomer";

			var dog2 = DIContainer.Resolve<Dog>();
			Assert.That(dog1.Name == dog2.Name);
		}

		#region These tests can only be tested when referencing packages not projects (require analyzers)

		//[Test]
		//public void Should_be_able_to_inject_using_inject_attribute_name()
		//{
		//	DIContainer.Clear();
		//	DIContainer.Register<IAnimal>(new Dog(), "Dog");
		//	DIContainer.Register<IAnimal>(new Cat(), "Cat");

		//	var animalSound = new AnimalSoundInjectUsingAttrWithName();
		//	Assert.IsTrue($"dog = {new Dog().Sound} and cat = {new Cat().Sound}" == animalSound.MakeSound());
		//}
		//[Test]
		//public void Should_be_able_to_inject_using_inject_attribute()
		//{
		//	DIContainer.Clear();
		//	DIContainer.Register<IAnimal>(new Dog());
		//	var animalSound = new AnimalSoundInjectUsingAttr();
		//	Assert.IsTrue(new Dog().Sound == animalSound.MakeSound());

		//	DIContainer.Clear();
		//	DIContainer.Register<IAnimal>(new Cat());
		//	animalSound = new AnimalSoundInjectUsingAttr();
		//	Assert.IsTrue(new Cat().Sound == animalSound.MakeSound());
		//}

		//[Test]
		//public void Should_be_able_to_inject_using_create_due_to_existing_constructor()
		//{
		//	DIContainer.Clear();
		//	DIContainer.Register<IAnimal>(new Dog());
		//	var animalSound = AnimalSoundInjectUsingCreate.Create();
		//	Assert.IsTrue(new Dog().Sound == animalSound.MakeSound());

		//	DIContainer.Clear();
		//	DIContainer.Register<IAnimal>(new Cat());
		//	animalSound = AnimalSoundInjectUsingCreate.Create();
		//	Assert.IsTrue(new Cat().Sound == animalSound.MakeSound());
		//}
		#endregion
	}
}