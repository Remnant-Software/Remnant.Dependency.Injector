# Remnant Dependency Injector

- Install nuget package: 

        Install-Package Remnant.Dependency.Injector -Version 2.0.0

- The injection follows a pull pattern unlike all other DI containers which uses a push pattern
- The pull pattern has no need to declare constructor arguments for DI, also no hierarchical DI wiring is required
- The DI container is global accessible within your current app domain
- Anywhere, any place in your code the DI container can be requested to resolve the object needed
- An extension method 'Resolve<<TType>>' is implemented on 'object' to allow any objects to call the container
- You can use the [Inject] attribute on fields which will automatically inject the dependency object

> **Note**: When using the [Inject] attribute to decorate class fields, your class must be specified as partial.

> **Important**: If your class already contains an empty parameter constructor: <br/>
- The analyzer can't generate a constructor with the injection code because the method (constructor) exists already
- In that case the Analyzer generates a public 'Inject' method, as well as a static 'Create' method on the class
- So you can call the static 'Create' method which will create an instance of the class and it calls the 'Inject' method to perform the injection
- Or you can instantiate the class and call the 'Inject' method yourself
- Keep in mind on your constructor, the injected fields will be null (unless you call 'Inject()' as the first thing on your constructor)
- Ensure that the specified injected fields are not <b>readonly</b>
- <i>(see example 4)</i>.
------
### Quick console app examples (<a href="https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements">using top-level statements feature</a>): 

#### Example 1: register something with the container (used by all examples)

```
using ConsoleApp;
using Remnant.Dependency.Injector;

DIContainer
  .Create("MyContainer")
  .Register(new List<string> { "John", "Jane" }, "Names");
```

#### Example 2: using extension method 'Resolve' on object

```csharp object
new object()
  .Resolve<List<string>>("Names")
  .ForEach(name => Console.WriteLine(name));
}
```

#### Example 3: using 'Inject' attribute
``` csharp
new Members().ListNames();
```
``` csharp
// Members.cs
using Remnant.Dependency.Injector;

namespace ConsoleApp
{
  public partial class Members
  {
    [Inject("Names")]
    private readonly List<string> _names;
    public void ListNames() => _names.ForEach(name => Console.WriteLine(name));
  }
}
```

#### Example 4: using static 'Create' method
``` csharp
//... static 'Create' member is generated which will do injection
Members.Create().ListNames();
```
``` csharp
// Members.cs
using Remnant.Dependency.Injector;

namespace ConsoleApp
{
  public partial class Members
  {
    [Inject("Names")]
    private List<string> _names; //must not be readonly

    public Members()
    {
      Console.WriteLine("Remnant code analyzer can not generate a constructor for injection, thus use generated static 'Create' member.");
    }

    public void ListNames() => _names.ForEach(name => Console.WriteLine(name));
  }
}
```
