using Remnant.Dependency.Interface;
using System;

namespace Remnant.Dependency.Injector
{
	internal class DIContainerObject
	{
		public DIContainerObject(Type type, Type objectType, object @object, Lifetime lifetime, string name = null)
		{
			Name = name ?? type.Name;
			ObjectType = objectType;	
			Object = @object;
			Type = type;
			Lifetime = lifetime;
		}

		public string Name { get; set; }
		public object Object { get; set; }
		public Type ObjectType { get;  }
		public Type Type { get;  }
		public Lifetime Lifetime { get; }
	}
}