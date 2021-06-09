# MiniatureIOC - Miniature Inversion of Control for DotNet

MiniatureIOC automatically registers your services with your Dependency Injection container.

It will scan your selected assemblies, and register each service according to the interface and lifetime you choose.

MiniatureIOC is not a Dependency Injection container. Instead, it integrates with Microsoft's built-in ServiceCollection.

# Installation

`dotnet add package MiniatureIOC`

# Features
* Only scans the selected assemblies
* Register with or without specifying an interface
* Supports Transient, Scoped, and Singleton lifetimes - defaults to Transient

### Planned
* Conditional adding of services (eg, according to an environment)

# Usage

### Add a service without specifying an interface

```C#
[MiniIOCDependency]
public class BasicRegistration { }
```

### Add a service with a specified interface

```C#
[MiniIOCDependency(typeof(IInterfaceRegistration))]
public class InterfaceRegistration : IInterfaceRegistration { }
```

### Add a service with a specified lifetime

```C#
[MiniIOCDependency(Lifetime.Singleton)]
public class SingletonRegistration { }
```
