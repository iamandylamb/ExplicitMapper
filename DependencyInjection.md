# Dependency Injection

There are many dependency injection frameworks available in the .NET space. They're all built on the same basic principles, although terminology will differ.

1. A **service** is a `Type`, usually an interface or base class, that you want to make available through the dependency injection framework.
1. A **component** is a concrete implementation of a **service**. Either the implementation of an interface, or derived from a base class. In some instances, the **service** and **component** are the same `Type`. 
1. A **component** may implement one or more **services**. For example, a class may implement one or more interfaces.
1. A **container** registers the association between **components** and **services**. When a **service** is requested, the appropriate **component** is provided.

Most DI frameworks also support open generic **components** being resolved as closed generic **services**. We won't go into detail about that here.

All DI frameworks require a mechanism for registering **components** and the **services** they provide. They all do this slightly differently, but one common requirement is the need to identify which **services** a **component** *can* provide.  
To make the registration of `IMapper<,>` **services** easier, `ExplicitMapper` includes an extension method on `Type` to identify all `IMapper<,>` **services** for a given **component**. The below examples make use of this to register all `IMapper<,>` **services** from a given assembly.

## Autofac

### Mapper Registration Helper
This extension method registers all mapper classes from the supplied assembly. The mappers are registered as **singletons** but other lifestyles may be appropriate depending on your use case.
```
using System.Linq;
using System.Reflection;
using Autofac;
using ExplicitMapper.DependencyInjection;

namespace Example.Autofac
{
    public static class MapperInstaller
    {
        public static ContainerBuilder RegisterMappers(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                   .Where(type => !type.IsGenericTypeDefinition && type.GetMappers().Any())
                   .As(type => type.GetMappers())
                   .SingleInstance(); // Other lifestyles may be preferable.

            return builder;
        }
    }
}
```

### Mapper Registration
This is a contrived example to demonstrate the registration of mapper classes and the provided open generic mappers. Look to the Autofac documentation for best practice guidance. 
```
var builder = new ContainerBuilder();

// Install all mapper classes from an assembly.
builder.RegisterMappers(Assembly.GetAssembly(typeof(UserRegistrationMapper)))
       // Register specific generic mappers.
       .RegisterGeneric(typeof(XmlSerializerMapper<>)).AsSelf();

var container = builder.Build();
```

## Castle Windsor

### Mapper Registration Helper
Castle Windsor uses `IWindsorInstaller` implementations for registration. The `MapperInstaller` below accepts a `FromDescriptor` via its constructor describing the assembly (or other collection of types) that should be scanned for `IMapper<,>` **services**. The mappers are registered as **singletons** but other lifestyles may be appropriate depending on your use case.
```
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ExplicitMapper.DependencyInjection;

namespace Example.Castle.Windsor
{
    public class MapperInstaller : IWindsorInstaller
    {
        private readonly FromDescriptor classes;

        public MapperInstaller(FromDescriptor classes)
        {
            this.classes = classes;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(this.classes.Where(type => !type.IsGenericTypeDefinition && type.GetMappers().Any())
                                           .WithService.Select((type, _) => type.GetMappers())
                                           .LifestyleSingleton()); // Other lifestyles may be preferable.
        }
    }
}
```

### Mapper Registration
This is a contrived example to demonstrate the registration of mapper classes and the provided open generic mappers. Look to the Castle Windsor documentation for best practice guidance. 
```
var container = new WindsorContainer();

// Install all mapper classes from an assembly.
container.Install(new MapperInstaller(Classes.FromAssemblyContaining<UserRegistrationMapper>()));

// Register specific generic mappers.
container.Register(Component.For(typeof(XmlSerializerMapper<>))
                            .ImplementedBy(typeof(XmlSerializerMapper<>))
                            .LifestyleSingleton());
```
## Microsoft Dependency Injection

### Mapper Registration Helper
This extension method registers all mapper classes from the supplied assembly. The mappers are registered as **singletons** but other lifestyles may be appropriate depending on your use case.
```
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ExplicitMapper.DependencyInjection;

namespace Example.Microsoft.Extensions.DependencyInjection
{
    public static class MapperInstaller
    {
        public static IServiceCollection AddMappers(this IServiceCollection serviceCollection, Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition)
                .SelectMany(t => t.GetMappers()
                    .Select(i => new { Service = i, Implementation = t }))
                .Aggregate(serviceCollection, (sc, s) => 
                    sc.AddSingleton(s.Service, s.Implementation)); // Other lifestyles may be preferable.
        }
    }
}
```

### Mapper Registration
This is a contrived example to demonstrate the registration of mapper classes and the provided open generic mappers. Look to the Microsoft documentation for best practice guidance. 
```
var serviceCollection = new ServiceCollection();

var services = 
    // Install all mapper classes from an assembly.
    serviceCollection.AddMappers(Assembly.GetAssembly(typeof(UserRegistrationMapper)))
                     // Register specific generic mappers.
                     .AddSingleton(typeof(XmlSerializerMapper<>), typeof(XmlSerializerMapper<>))
                     .BuildServiceProvider();
```

## Structure Map

### Mapper Registration Helper
Structure Map uses `Registry` implementations for registration. The `MapperRegistry` below accepts an `Assembly` via its constructor that should be scanned for `IMapper<,>` **services**. The mappers are registered as **singletons** but other lifestyles may be appropriate depending on your use case.
```
using System.Reflection;
using StructureMap;
using ExplicitMapper;

namespace Example.StructureMap
{
    public class MapperRegistry : Registry
    {
        public MapperRegistry(Assembly assembly)
        {
            Scan(scanner => 
            {
                // Install all mapper classes from an assembly.
                scanner.Assembly(assembly);
                scanner.Exclude(type => type.IsGenericTypeDefinition);
                scanner.ConnectImplementationsToTypesClosing(typeof(IMapper<,>))
                       .OnAddedPluginTypes(config => config.Singleton()); // Other lifestyles may be preferable.
            });

            // Register specific generic mappers.
            this.ForSingletonOf(typeof(XmlSerializerMapper<>)).Use(typeof(XmlSerializerMapper<>)).Singleton();
        }
    }
}
```

### Mapper Registration
This is a contrived example to demonstrate the registration of mapper classes and the provided open generic mappers. Look to the Structure Map documentation for best practice guidance. 
```
var container = new Container(new MapperRegistry(Assembly.GetAssembly(typeof(UserRegistrationMapper))));
```