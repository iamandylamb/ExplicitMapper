# ExplicitMapper
A mapping framework that is completely explicit.

## No magic
Type mapping and member mapping must be fully defined.  
If it's not explicitly mapped, it won't be mapped.

## Easy to use
Base classes supplied for `ClassMapper`, `StructMapper` and `StringMapper` mappings.  
**Collection** and **asynchronous** mapping supported without additional effort.  
Common mappings included out-of-the-box. For example, the `XmlSerializerMapper` is a specialist type of `StringMapper`.

## DI framework friendly
Complex or nested mappings can be constructed through constructor injection in your favourite DI framework.  
All mappers implement the same generic interface for simple installation and use.

```
public interface IMapper<TSource, TDestination>
{
    TDestination Map(TSource source);

    IEnumerable<TDestination> Map(IEnumerable<TSource> source);

    Task<TDestination> MapAsync(TSource source);

    Task<IEnumerable<TDestination>> MapAsync(IEnumerable<TSource> source);
}
```

## Examples
To Do
