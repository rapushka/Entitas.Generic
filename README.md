# What is this?
Based on [lightjiao/Entitas.Generic](https://github.com/lightjiao/Entitas.Generic) (as you can see on your own. _.)
with my preferences for working with Entitas. And some functionality from original Entitas, which wasn't present in [lightjiao/Entitas.Generic](https://github.com/lightjiao/Entitas.Generic) + some additional stuff

Inspired by [Entitas.Generic](https://github.com/yosadchyi/Entitas.Generic), with simpler API. Work perfectly with Native Entitas VisualDebug

> ❗ I've applied the [fix](https://github.com/sschmid/Entitas/issues/1067#issuecomment-1623734894) for Entitas.VisualDebug in Unity 2022.2+  
> ❗ If you wanna use this library with older unity – you need to revert the [commit](https://github.com/rapushka/Entitas.Generic/commit/598154ca6e7079e9a9a3d79a9002f93ed931f86f).. somehow:^

# How to install
## Unity
- Put `https://github.com/rapushka/Entitas.Generic.git` in Window -> Package Manager -> + -> Add package from git URL...
- [More info](https://docs.unity3d.com/Manual/upm-git.html)

## Pure C# (..why?)
- Just clone the repo to your project, i guess:^)
- And remove files, that constains `UnityEngine` mention

# Code samples
-  There is the [Samples~](https://github.com/rapushka/Entitas.Generic/tree/master/Samples%7E) folder with a sample for Unity and one for pure C# (cmd)

# Use Guide
Sometimes I will add wiki..

# What works?
Almost all of the original Entitas functionality has been implemented (at least what makes sense imho), including:
- EntityIndex and PrimaryEntityIndex
- EventAttribute
  - except [its parameters](https://github.com/sschmid/Entitas/wiki/Attributes#parameters) (`EventTarget`, `EventType`, priority)
- CleanupAttribute
- UniqueAttribute

## Addons:
Also i've added some additional stuff, that i needed in the original Entitas, including:
- EntitytBehaviour and ComponentBehaviour (aka. Blueprints)
- ComponentID (dropdown to pick desired component in unity as value)
