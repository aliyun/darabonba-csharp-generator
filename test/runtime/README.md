# Model mapping checks

Models implement their own `ToMap` and static `FromMap`, following the field-by-field
approach in [C# PR #83](https://github.com/aliyun/darabonba-csharp-generator/pull/83/files)
and the [Python generator](https://github.com/aliyun/darabonba-python-generator/blob/master/lib/generator.js#L1153).
Lists and maps recurse into the current element, use `Add`, and preserve null and
empty collections. Declared model types call their own conversion methods.

`FromMap` creates and populates the model directly, including inherited fields.
The existing null-safe `ModelExtensions.ToMap` dispatches to virtual `ToMapCore`.
Old SDK models inherit its reflection-based default implementation, while newly
generated models override it with field-by-field conversion. Generated mapping methods do not call
`ToMapFactory`, `Model.ToObject`, JSON serialization, or property reflection.

The new generator requires the accompanying `tea-csharp` change (`ToMapCore`).
Referenced models also need their own mapping
methods; the accompanying OpenApi changes supply these for its models. These
runtime/dependency changes must be published before generated SDKs can consume
them as NuGet packages. Package versions are not changed here.

This restores the old generator's type-directed behavior: `any` values pass
through without inspecting their runtime properties, and string fields accept
strings rather than implicitly serializing lists/maps to JSON. Nullable numeric
fields still use `System.Convert` for boxed numbers and existing numeric strings.
Null fields and declared stream handling follow the current C# model behavior.

Generate and compare the checked-in C# fixtures:

```sh
npm test
```

The runtime project references the sibling `tea-csharp` source tree. Build with
C# 5 for .NET 8, .NET Standard 2.0, and .NET Framework 4.5, then run on .NET 8:

```sh
dotnet build test/runtime/ModelMapping.csproj
dotnet run --project test/runtime/ModelMapping.csproj --framework net8.0 --no-build

dotnet build test/runtime/ModelMapping.csproj -p:MappingFixture=complexModel
dotnet run --project test/runtime/ModelMapping.csproj --framework net8.0 --no-build -p:MappingFixture=complexModel
```

Checks cover 64-level recursive models, mutually recursive definitions, nested
anonymous models (including exception fields), local/builtin/imported inheritance,
list/map mixtures, null elements, empty collections, field aliases, typed dictionary
input, numeric limits and overflow, byte arrays, streams, `any` pass-through,
local/imported API return conversion, and round trips under `en-US` and `fr-FR`.
Imported models are implemented independently and count factory calls; getters
that throw on reflection verify direct dispatch. Recursive definitions use finite
data trees; cyclic object graphs have no finite dictionary representation.
