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

### Startup.cs

```C#
using MiniatureIOC.Extensions;

...

public void ConfigureServices(IServiceCollection services)
{
    services.AddMiniIOCDependenciesFromAssembliesContaining(
      typeof(TypeFromSomeProject), typeof(TypeFromAnotherProject)
    );
}
```

Then place the MiniIOCDependency attribute above each of your services as follows:

### Add a service without specifying an interface

```C#
using MiniatureIOC;

[MiniIOCDependency]
public class BasicRegistration { }
```

### Add a service with a specified interface

```C#
using MiniatureIOC;

[MiniIOCDependency(typeof(IInterfaceRegistration))]
public class InterfaceRegistration : IInterfaceRegistration { }
```

### Add a service with a specified lifetime

```C#
using MiniatureIOC;

[MiniIOCDependency(Lifetime.Singleton)]
public class SingletonRegistration { }
```

# So how is it "Miniature"?

This whole project is only 3 C# files. Needless to say, it's not going to have a large impact on your build time.

# License

GPL 3.0 License

MiniatureIOC - Miniature Inversion of Control for DotNet

Copyright (C) 2021 Joshua Hull

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
