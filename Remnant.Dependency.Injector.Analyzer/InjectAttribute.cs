using System;

namespace Remnant.Dependency.Injector
{
	/// <summary>
	/// Inject field by specifying the 'Type' explicitly, other wise the field declaration type will be used (inferred).<br/>
	/// Specify the name, for the scenarios where the type 'Type' are registered mutliple times but are used in different context.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class InjectAttribute : Attribute
	{

		/// <summary>
		/// Construct attribute, the type will be inferred
		/// </summary>
		public InjectAttribute()
		{
		}

		/// <summary>
		/// Construct attribute, the type will be inferred
		/// <param name="name">The unique name for the registered dependency</param>
		/// </summary>
		public InjectAttribute(string name)
		{
			Name = name;
		}

		/// <summary>
		/// Construct attribute with the 'Type' and optional name to resolve
		/// </summary>
		/// <param name="type">The dependency type</param>
		/// <param name="name">Optional, unique name for the registered dependency</param>
		public InjectAttribute(Type type, string name = null)
		{
			Type = type;
			Name = name;
		}

		/// <summary>
		/// The 'Type' to resolve
		/// </summary>
		public Type Type { get; }

		/// <summary>
		/// The unique name of the dependency as registered to resolve
		/// </summary>
		public string Name { get; }
	}
}
