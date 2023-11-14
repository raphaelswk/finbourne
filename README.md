# LUSID Caching Service

LUSID Caching Service is a C# implementation of a generic in-memory cache with a Least Recently Used eviction policy to be used in LUSID app.

## Table of Contents
- [About](#about)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Examples](#examples)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [License](#license)

## About

Our LUSID Caching Service is designed to handle large volumes of data efficiently by implementing an Least Recently Used eviction policy. It allows developers to store arbitrary types of objects using unique keys and automatically evicts the least recently used item when the cache reaches its maximum capacity.

## Features

- Generic in-memory cache with Least Recently Used eviction policy.
- Thread-safe implementation for concurrent usage.
- Configurable maximum capacity to avoid running out of memory.
- Eviction callback mechanism to notify when items are evicted.
- Singleton Design Pattern, [based on this website](https://www.dofactory.com/net/singleton-design-pattern)

## Getting Started

### Prerequisites

- [.NET Core 6](https://dotnet.microsoft.com/download/dotnet/6.0)

### Installation

Clone the repository to your local machine:

```bash
git clone https://github.com/yourusername/LRUCache.git
cd finbourne
dotnet build
```

## Usage
1. Create an instance of the CacheService:
```csharp
var cacheServiceInstance = CacheService<int, string>.GetInstance();
```
2. Add or update items in the cache:
```csharp
cacheServiceInstance.AddOrUpdate(1, "Jhon Doe");
```
3. Retrieve an item from the cache:
```csharp
if (cacheServiceInstance.TryGetValue(1, out var value))
    Console.WriteLine("Value for key '1': " + value);
```
4. Implement the eviction callback if needed:
```csharp
private static void EvictionCallback(TKey key, TValue value)
{
    Console.WriteLine($"Evicted: Key='{key}', Value='{value}'");
}
```

## Examples

Check the [Console App](https://github.com/raphaelswk/finbourne/blob/main/LUSID.Presentation.ConsoleApp/Program.cs) or the [CacheServiceTests](https://github.com/raphaelswk/finbourne/blob/main/LUSID.Services.Tests/UnitTests/CacheServiceTests.cs) files for usage examples and test cases.

## Roadmap

Some of the possible future implementations that can be done on the project:
- Turn it into a Nuget Package
- Implement Load Tests
- Configure DevSecOps
- EvictionCallback - Perform additional actions on eviction, e.g., logging, cleanup, calling a messaging service or API, Email etc.)

## Contributing

Contributions are welcome! If you find any issues or have improvements, feel free to open a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

Made with ♥ &nbsp;by Raphael Socolowski 👋 &nbsp;[Check out my linkedin](https://www.linkedin.com/in/raphaelswk/)
