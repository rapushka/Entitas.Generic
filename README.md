# What is this?
[lightjiao/Entitas.Generic](https://github.com/lightjiao/Entitas.Generic) fork (as you can see on your own. _.)  
Plus:
- My subjective preferences — like codestyle, naming, etc
- Functionality from original [sschmid/Entitas](https://github.com/sschmid/Entitas), which wasn't present in the [lightjiao/Entitas.Generic](https://github.com/lightjiao/Entitas.Generic) — see [What works](https://github.com/rapushka/Entitas.Generic/blob/main/README.md#what-works)
- Some additional stuff — like `ComponentID`s, or `EntityBehaviour`s; see [Addons](https://github.com/rapushka/Entitas.Generic/blob/main/README.md#addons)

Originally inspired by [yosadchyi/Entitas.Generic](https://github.com/yosadchyi/Entitas.Generic), but with simpler API. Works perfectly with Native Entitas VisualDebug

# How to install
## Unity
- Put `https://github.com/rapushka/Entitas.Generic.git` in Window -> Package Manager -> + -> Add package from git URL...
- [More info](https://docs.unity3d.com/Manual/upm-git.html)
> ❗ I've applied the [fix](https://github.com/sschmid/Entitas/issues/1067#issuecomment-1623734894) for Entitas.VisualDebug in Unity 2022.2+  
> ❗ So if you wanna use this library with older unity – you need to revert this [commit](https://github.com/rapushka/Entitas.Generic/commit/598154ca6e7079e9a9a3d79a9002f93ed931f86f).. somehow:^

## Godot
- Clone the [godot/dev](https://github.com/rapushka/Entitas.Generic/tree/godot/dev) branch anywhere within your project folder
- In the `.csproj` file add references to the following `.dll`s:
  - In Rider you can do it by right clicking on your project > Add > Add reference....
  - othervise add next lines for each library in `<ItemGroup>`
    ```
    <Reference Include="DllName">
      <HintPath>path/to/DllName.dll</HintPath>
    </Reference>
    ```
    - `Entitas/DesperateDevs/DesperateDevs.Caching.dll`
    - `Entitas/DesperateDevs/DesperateDevs.Extensions.dll`
    - `Entitas/DesperateDevs/DesperateDevs.Reflection.dll`
    - `Entitas/DesperateDevs/DesperateDevs.Serialization.dll`
    - `Entitas/DesperateDevs/DesperateDevs.Threading.dll`
    - `Entitas/Entitas/Entitas.dll`
- More info about Godot-Entitas integration:
  - https://github.com/PanMadzior/GodotEntitas
  - https://github.com/Guendeli/godot-entitas-template


## Pure C# (...why?)
- Just clone the repo to your project, i guess:^)
- And remove files, that constains `UnityEngine` mention

# Code samples
There is the [Samples~](https://github.com/rapushka/Entitas.Generic/tree/master/Samples%7E) folder with a sample for Unity and one for pure C# (cmd)
> ❗ It isn't actual yet, sowwy

# Use Guide
TODO: wiki

# What works?
Almost all of the original [sschmid/Entitas](https://github.com/sschmid/Entitas) functionality has been implemented (at least what makes sense imho), including:
- `EntityIndex` and `PrimaryEntityIndex`
- `EventAttribute`
  - except [some of its parameters](https://github.com/sschmid/Entitas/wiki/Attributes#parameters) (`EventType`, priority)
- `CleanupAttribute`
- `UniqueAttribute`

## Addons
Also i've added some additional stuff, that i needed in the original Entitas, including:
- EntitytBehaviour and ComponentBehaviour (aka. Blueprints)
- ComponentID (dropdown to pick desired component in unity as value)

# Some plans
- WIP: Rework `ContextAttribute` with `IComponent<IScope>`
- Reflection backing & Boilerplate backing (yes, source-generation, lol)
