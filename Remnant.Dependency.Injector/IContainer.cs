using System;

namespace Remnant.Dependency.Interface
{
	/// <summary>
	/// Specify the lifetime when registering a type
	/// </summary>
	public enum Lifetime
	{
		Singleton = 0,
		Transient
	}

	public interface IContainer
	{
		/// <summary>
		/// Register a singleton using a specified type to resolve
		/// </summary>
		/// <typeparam name="TType">The type that will be used to resolve the singleton entry</typeparam>
		/// <param name="instance">The singleton instance</param>
		/// <param name="name">Optional, a unique name that can be used to resolve dependency</param>
		/// <returns>Returns the container</returns>
		IContainer Register<TType>(object instance, string name = null) 
			where TType : class;

		/// <summary>
		/// Register a singleton using a specified type to resolve
		/// </summary>
		/// <typeparam name="TType">The type that will be used to resolve the singleton entry</typeparam>
		/// <param name="instance">The singleton instance</param>
		/// <param name="name">Optional, a unique name that can be used to resolve dependency</param>
		/// <returns>Returns the container</returns>
		IContainer Register(Type type, object instance, string name = null);

		/// <summary>
		/// Register a singleton
		/// </summary>
		/// <param name="instance">The singleton instance</param>
		/// <param name="name">Optional, a unique name that can be used to resolve dependency</param>
		/// <returns>Returns the container</returns>
		IContainer Register(object instance, string name = null);

		/// <summary>
		/// Register a singleton or transient with the container
		/// </summary>
		/// <typeparam name="TType">The type that will be used to resolve and construct entry</typeparam>
		/// <param name="lifetime">Specify if registration is for singleton or transient</param>
		/// <param name="name">Optional, a unique name that can be used to resolve dependency</param>
		/// <returns>Returns the container</returns>
		IContainer Register<TType>(Lifetime lifetime, string name = null) 
			where TType : class, new();

		/// <summary>
		/// Register a singleton or transient with the container
		/// </summary>
		/// <typeparam name="TType">The type that will be used to resolve entry</typeparam>
		/// <typeparam name="TObject">The type that will be constructed and return on resolve</typeparam>
		/// <param name="lifetime">Specify if registration is for singleton or transient</param>
		/// <param name="name">Optional, a unique name that can be used to resolve dependency</param>
		/// <returns>Returns the container</returns>
		IContainer Register<TType, TObject>(Lifetime lifetime, string name = null) 
			where TType : class
			where TObject : class, new();

		/// <summary>
		/// Deregister a container entry using generic type
		/// </summary>
		/// <typeparam name="TType">The type that was registered</typeparam>
		IContainer DeRegister<TType>() where TType : class;

		/// <summary>
		/// Deregister a container entry using instance
		/// </summary>
		/// <param name="instance">The type of instance that will be removed from the container</param>
		IContainer DeRegister(object instance);

		/// <summary>
		/// Resolve type to get the instance
		/// </summary>
		/// <typeparam name="TType">The type that was registered</typeparam>
		/// <param name="name">Optional, a unique name that can be used to resolve dependency</param>
		/// <returns>Returns an  instance of the specified type</returns>
		TType ResolveInstance<TType>(string name = null) 
			where TType : class;

		/// <summary>
		/// Clear container from all registeries
		/// </summary>
		IContainer Clear();

		/// <summary>
		/// Returns the internal container that to be used for direct access
		/// </summary>
		/// <typeparam name="TContainer">Specify the type for the internal container. Exception will be thrown if casting fails.</typeparam>
		/// <returns></returns>
		TContainer InternalContainer<TContainer>()
			where TContainer : class;
	}
}
