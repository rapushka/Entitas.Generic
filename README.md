# What is this?
This is the fork of the [lightjiao/Entitas.Generic](https://github.com/lightjiao/Entitas.Generic) which is "Originally inspired by [yosadchyi/Entitas.Generic](https://github.com/yosadchyi/Entitas.Generic)"  
and which gives you opportunity to use the [sschmid/Entitas](https://github.com/sschmid/Entitas) without Source-Code Generation! With power of c# Generics😼  

Plus:
- My subjective preferences — like codestyle, naming, etc.
- Functionality from original [sschmid/Entitas](https://github.com/sschmid/Entitas), which wasn't present in the [lightjiao/Entitas.Generic](https://github.com/lightjiao/Entitas.Generic) — see [What works](https://github.com/rapushka/Entitas.Generic/blob/main/README.md#what-works)
- Some additional stuff — like `ComponentID`s, or `EntityBehaviour`s; see [Addons](https://github.com/rapushka/Entitas.Generic/blob/main/README.md#addons)
- Reflection Backing

# How to install
- Just Clone this repo into your project's folder
- Maybe you'll need some additional setup for your engine:

## Unity
- It should automatically add the `ENTITAS_GENERIC_UNITY_SUPPORT` define to your project in `Project Settings/Player/Other Settings/Script Compilation/Scripting Define Symbols`.  
But if there compile errors - the define won't be added, and you can add it manually.  
Or remove the `Editor/AutoDefineUnity.cs` if you don't want this define  

> ❗ I've applied the [fix](https://github.com/sschmid/Entitas/issues/1067#issuecomment-1623734894) for Entitas.VisualDebug in Unity 2022.2+
> So if you wanna use this library with older unity – you need to revert this [commit](https://github.com/rapushka/Entitas.Generic/commit/598154ca6e7079e9a9a3d79a9002f93ed931f86f) i guess

## Godot
- In the `.csproj` file add references to the following `.dll`s:
  - In Rider you can do it by right clicking on your project > Add > Add reference...
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

# Examples
There is not wiki yet:( but you can check my other projects, where i used this library by myself!
- [Deck Scaler (Unity)](https://github.com/rapushka/deck_scaler) - currently working
- [the Bad Luck (Godot)](https://github.com/rapushka/RerollKnight-godot) – deprecated
- [the Bad Luck (Unity)](https://github.com/rapushka/RerollKnight) – deprecated
- [Burned Jack (Unity)](https://github.com/rapushka/acerola-jam-0/tree/main/src/21-Deckbuilder) – a submission for [Acerola Jam 0](https://itch.io/jam/acerola-jam-0) (so the code is shittier than usual)

# How to
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
Runtime works, but not the Visual Debugger and other editor things

## Addons
I also added some additional features that i wish were in the original Entitas, including:
- `EntitytBehaviour` and `ComponentBehaviour` (a.k.a. Blueprints) – Allow you to add your components as MonoBehaviours
- `ComponentID` – dropdown to pick component in Unity
