# What is this?
[lightjiao/Entitas.Generic](https://github.com/lightjiao/Entitas.Generic) fork (as you can see on your own. _.)  
Plus:
- My subjective preferences — like codestyle, naming, etc
- Functionality from original [sschmid/Entitas](https://github.com/sschmid/Entitas), which wasn't present in the [lightjiao/Entitas.Generic](https://github.com/lightjiao/Entitas.Generic) — see [What works](https://github.com/rapushka/Entitas.Generic/blob/main/README.md#what-works)
- Some additional stuff — like `ComponentID`s, or `EntityBehaviour`s; see [Addons](https://github.com/rapushka/Entitas.Generic/blob/main/README.md#addons)

Originally inspired by [yosadchyi/Entitas.Generic](https://github.com/yosadchyi/Entitas.Generic), but with simpler API. Works perfectly with Native Entitas VisualDebug

# How to install
- Just Clone this repo into your project's folder
- Maybe you'll need some additional setup for your engine:

## Unity
> ❗ I've applied the [fix](https://github.com/sschmid/Entitas/issues/1067#issuecomment-1623734894) for Entitas.VisualDebug in Unity 2022.2+  
> ❗ So if you wanna use this library with older unity – you need to revert this [commit](https://github.com/rapushka/Entitas.Generic/commit/598154ca6e7079e9a9a3d79a9002f93ed931f86f) i guess

## Godot
- In the `.csproj` file add references to the following `.dll`s:
  - In Rider you can do it by right clicking on your project > Add > Add reference....
  - othervise open the `.csproj` in a text editor add next lines for each library in `<ItemGroup>`
    ```
    <Reference Include="DLL_NAME">
      <HintPath>path/to/DLL_NAME.dll</HintPath>
    </Reference>
    ```
    - `Entitas/DesperateDevs/DesperateDevs.Caching.dll`
    - `Entitas/DesperateDevs/DesperateDevs.Extensions.dll`
    - `Entitas/DesperateDevs/DesperateDevs.Reflection.dll`
    - `Entitas/DesperateDevs/DesperateDevs.Serialization.dll`
    - `Entitas/DesperateDevs/DesperateDevs.Threading.dll`
    - `Entitas/Entitas/Entitas.dll`
  - [How this should look like](https://github.com/rapushka/RerollKnight-godot/blob/dev/src/RerollKnight.csproj)
- More info about Godot-Entitas integration:
  - https://github.com/PanMadzior/GodotEntitas
  - https://github.com/Guendeli/godot-entitas-template


## Other
- It has to work as is, but if it doesn't [let me know](https://github.com/rapushka/Entitas.Generic/issues/new)

# Code samples
TODO: wiki  
But you can check my other projects, where i used this library by myself!
- [the Bad Luck (Godot)](https://github.com/rapushka/RerollKnight-godot) – WIP
- [the Bad Luck (Unity)](https://github.com/rapushka/RerollKnight) – deprecated
- [Burned Jack (Unity)](https://github.com/rapushka/acerola-jam-0/tree/main/src/21-Deckbuilder) – a submission for [Acerola Jam 0](https://itch.io/jam/acerola-jam-0) (so the code is shittier than usual)

# Use Guide
TODO: wiki

# What works?
## Unity
Almost all of the original [sschmid/Entitas](https://github.com/sschmid/Entitas) functionality has been implemented (at least what makes sense imho), including:
- `EntityIndex` and `PrimaryEntityIndex`
- `EventAttribute`
  - except [some of its parameters](https://github.com/sschmid/Entitas/wiki/Attributes#parameters) (`EventType`, priority)
- `CleanupAttribute`
- `UniqueAttribute`

## Godot
The core is working, but Visual Debugger and other editor things are WIP

## Addons
Also i've added some additional stuff, that i needed in the original Entitas, including:
- EntitytBehaviour and ComponentBehaviour (aka. Blueprints)
- ComponentID (dropdown to pick desired component in unity as value)
