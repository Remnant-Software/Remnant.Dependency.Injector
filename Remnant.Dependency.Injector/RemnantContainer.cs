using Remnant.Dependency.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Remnant.Dependency.Injector
{

	/// <summary>
	/// Remnant dependency container 
	/// </summary>
	public sealed class RemnantContainer : IContainer
	{
		private readonly List<DIContainerObject> _containerObjects = new List<DIContainerObject>();

		private DIContainerObject AddObject(DIContainerObject containerObject)
		{
			if (!string.IsNullOrEmpty(containerObject.Name) && 
				_containerObjects.Exists(co => co.Name == containerObject.Name))
				throw new InvalidOperationException($"Unable to register dependency, because there is already an existing registry called '{containerObject.Name}'");

			if (containerObject.ObjectType.IsInterface)
				throw new InvalidCastException($"Invalid type '{containerObject.ObjectType.FullName}' registration. Interfaces cannot be registered without specifying also a class that implements the interface.");

			_containerObjects.Add(containerObject);
			return containerObject;
		}

		public IContainer Register<TType>(object instance, string name = null) where TType : class
		{
			if (instance?.GetType() == typeof(Lifetime))
				AddObject(new DIContainerObject(typeof(TType), typeof(TType), null, (Lifetime)instance, name));
			else
				AddObject(new DIContainerObject(typeof(TType), instance.GetType(), instance, Lifetime.Singleton, name));

			return this;
		}

		public IContainer Register(Type type, object instance, string name = null)
		{
			AddObject(new DIContainerObject(type, instance.GetType(), instance, Lifetime.Singleton, name));
			return this;
		}

		public IContainer Register(object instance, string name = null)
		{
			AddObject(new DIContainerObject(instance.GetType(), instance.GetType(), instance, Lifetime.Singleton, name));
			return this;
		}

		public IContainer Register<TType>(Lifetime lifetime, string name = null) where TType : class, new()
		{
			//if (typeof(TType).IsInterface)
			//	throw new InvalidCastException($"Invalid type '{typeof(TType).FullName}' registration. Interfaces cannot be registered without specifying also a class that implements the interface.");

			Register<TType, TType>(lifetime, name);
			return this;
		}

		public IContainer Register<TType, TObject>(Lifetime lifetime, string name = null)
			where TType : class
			where TObject : class, new()
		{
			AddObject(new DIContainerObject(typeof(TType), typeof(TObject), null, lifetime, name));
			return this;
		}

		public IContainer DeRegister<TType>()
			where TType : class
		{
			var containerObject = _containerObjects.FirstOrDefault((m => m.Type == typeof(TType)));

			if (containerObject != null)
			{
				_containerObjects.Remove(containerObject);
			}
			return this;
		}

		public IContainer DeRegister(object instance)
		{
			var containerObject = _containerObjects.FirstOrDefault((m => m.Type == instance.GetType()));

			if (containerObject != null)
			{
				_containerObjects.Remove(containerObject);
			}
			return this;
		}

		public IContainer Clear()
		{
			_containerObjects.Clear();
			return this;
		}

		public TType ResolveInstance<TType>(string name = null)
			where TType : class
		{
			DIContainerObject containerObject = null;

			if (!string.IsNullOrEmpty(name))
				containerObject = _containerObjects.Find(co => co.Name == name && co.Type == typeof(TType));

			else if (_containerObjects.Exists(co => co.Type == typeof(TType)))
			{
				if (_containerObjects.FindAll(co => co.Type == typeof(TType)).Count > 1)
					throw new InvalidOperationException($"Unable to resolve requested dependency, the type '{typeof(TType).FullName}' is registered more than once. Please specify the unique name to be more specific. ");

				containerObject = _containerObjects.FirstOrDefault(m => m.Type == typeof(TType));
			}

			if (containerObject == null)
				throw new ArgumentException($"The container cannot resolve requested dependency using '{typeof(TType).FullName}' or name '{name}'.");

			if (containerObject.Object == null &&
				containerObject.Lifetime == Lifetime.Singleton)
				lock(containerObject) containerObject.Object = Activator.CreateInstance(containerObject.ObjectType); 

			return containerObject.Object != null
				? (TType)containerObject.Object
				: (TType)Activator.CreateInstance(containerObject.ObjectType);	
		}

		public TContainer InternalContainer<TContainer>() where TContainer : class
		{
			if (this as TContainer == null)
				throw new InvalidCastException($"The internal container is of type {this.GetType().FullName} and cannot be cast to {typeof(TContainer).FullName}");

			return this as TContainer;
		}
	}
}
