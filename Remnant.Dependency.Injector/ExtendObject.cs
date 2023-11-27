using Remnant.Dependency.Interface;
using System;

namespace Remnant.Dependency.Injector
{
	public static class ExtendObject
	{
		/// <summary>
		/// Resolve using the global dependency injection container
		/// </summary>
		/// <typeparam name="TType">The type to resolve</typeparam>
		/// <param name="source"></param>
		/// <param name="name">Optional, a unique name that can be used to resolve dependency</param>
		/// <returns>Returns the singleton or transient instance</returns>
		/// <exception cref="NullReferenceException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public static TType Resolve<TType>(this object source, string name = null)
			where TType : class
		{
			var containerName = AppDomain.CurrentDomain.GetData("RemnantContainer");
		
			if (string.IsNullOrEmpty((string)containerName))
				throw new NullReferenceException($"There is no Remnant dependency injection container registered with the app domain. Unable to resolve {typeof(TType).FullName}.");

			var container = AppDomain.CurrentDomain.GetData((string)containerName);

			if (container == null || container as IContainer == null)
				throw new NullReferenceException($"The container registered as '{containerName}' doesn't exist within the app domain.");

			var instance = (container as IContainer).ResolveInstance<TType>(name);

			if (instance == null)
				throw new ArgumentException($"The container cannot resolve requested object '{typeof(TType).FullName}'.");

			return instance;
		}
	}
}
