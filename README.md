# ExplicitMapper
A mapping framework that is completely explicit.

## No magic
Type mapping and member mapping must be fully defined.  
If it's not explicitly mapped, it won't be mapped.

## Easy to use
Base classes are supplied for mapping classes (reference types), structs (value types), and strings.  
**Collection** and **asynchronous** mapping is supported without additional effort.  
Common mappings are included out-of-the-box. For example, the `XmlSerializerMapper` is a specialist type of `StringMapper`.

## DI framework friendly
Complex or nested mappings can be constructed through constructor injection in your favourite DI framework.  
Examples can be found in the [Dependency Injection](./DependencyInjection.md) page.  
All mappers implement the same generic interface for simple installation and use.

```
public interface IMapper<TSource, TDestination>
{
    // Map a source object to a destination object.
    TDestination Map(TSource source);

    // Map a source collection to a destination collection.
    IEnumerable<TDestination> Map(IEnumerable<TSource> source);

    // Map a source object to a destination object asynchronously.
    Task<TDestination> MapAsync(TSource source);

    // Map a source collection to a destination collection asynchonously.
    Task<IEnumerable<TDestination>> MapAsync(IEnumerable<TSource> source);

    // Map a source collection to a destination collection concurrently and asynchronously.
    Task<IEnumerable<TDestination>> MapParallel(IEnumerable<TSource> source);
}
```

## Installation

### Package Manager
`PM> Install-Package Explicit.Mapper`

### .NET CLI
`> dotnet add package Explicit.Mapper`

## Mapper examples

### Class mapper for reference types
The `ClassMapper` is used to map **reference** types. It is an abstract class, it implements the `IMapper` interface, and it includes a single abstract `Map` method your mapper must implement.
```
protected abstract void Map(TSource source, TDestination destination);
```
The `source` object and a new instance of the `destination` class are supplied. The method must map from the source to the destination.

```
protected override void Map(UserRegistrationModel source, Address destination)
{
    destination.AddressLine1 = source.AddressLine1;
    
    destination.AddressLine2 = source.City;

    destination.PostalCode = source.Postcode;
}
```

### Struct mapper for value types
The `StructMapper` is used to map **value** types. It is an abstract class, it implements the `IMapper` interface, and it includes a single abstract `Map` method your mapper must implement.
```
protected abstract void Map(TSource source, out TDestination destination);
```
The `source` object is supplied alongside an 'out' `destination` parameter. The method must assign to the out parameter from the source.

```
protected override void Map(UserRegistrationModel source, out DateTime destination)
{
    destination = new DateTime(source.YearOfBirth, source.MonthOfBirth, source.DayOfBirth);
}
```

### String Mapper
The `StringMapper` is a special case mapper for strings. Like the other mapper base classes it is abstract, it implements the `IMapper` interface, and it includes a single abstract `Map` method your mapper must implement.
```
protected abstract void Map(TSource source, StringBuilder destination);
```
The `source` object and a destination `StringBuilder` are supplied. The method must use the `StringBuilder` to build the string from the `source` object.

## Asynchronous collection mapping
Two asynchronous collection mapping methods are available, `MapAsync` and `MapParallel`.

```
public async Task<IEnumerable<TDestination>> MapAsync(IEnumerable<TSource> source);

public async Task<IEnumerable<TDestination>> MapParallel(IEnumerable<TSource> source);
```

Their signatures are the same, but their behaviour is different:

`MapAsync` executes the collection mapping asynchronously, but within the async `Task` sequentially maps each item in the collection.
```
o----MapAsync(items)----------------------------------------o
          \                                           /
            Map(items[0]) Map(items[1]) Map(items[2])
```

`MapParallel` executes the mapping of each item in the collection asynchronously, completing the async mapping when all items have been mapped.
```
o----MapParallel(items)-------------------------------------o
          \               /
          \ Map(items[0])
          \ Map(items[1])
            Map(items[2])
```

The most appropriate asynchronous collection mapping method to use will depend on each specific use case. The size of the collection and the complexity of the mapping should be considered.
