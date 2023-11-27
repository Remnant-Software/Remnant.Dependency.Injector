# Remnant Dependency Injector

- The injection follows a pull pattern unlike all other DI containers which uses a push pattern
- The pull pattern has no need to declare constructor arguments for DI, also no hierarchical DI wiring is required
- The DI container is global accessible within your current app domain
- Anywhere, any place in your code the DI container can be requested to resolve the object needed
- An extension method 'Resolve<<TType>>' is implemented on 'object' to allow any objects to call the container
- You can use the [Inject] attribute on fields which will automatically inject the dependency object