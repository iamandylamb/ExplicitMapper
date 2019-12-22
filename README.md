# ExplicitMapper
A mapping framework that is completely explicit.

## No magic
Type mapping and member mapping must be fully defined.  
If it's not explicitly mapped it won't be mapped.

## DI framework friendly
Complex mappers can be constructed through constructor injection in your favourite DI framework.  
All mappers implement the same generic interface for simple installation and use.

```
IMapper<TSource, TDestination>
```