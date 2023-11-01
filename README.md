## What is this?
Based on [lightjiao/Entitas.Generic](https://github.com/lightjiao/Entitas.Generic) (as you can see for yourself. _.)
with my preferences for working with Entitas

Inspired by [Entitas.Generic](https://github.com/yosadchyi/Entitas.Generic), and I make API more simple. Work perfectly with Native Entitas VisualDebug

> If you use Unity 2022.2 or more, Entitas VisualDebug may have BUG, see this: [sschmid/Entitas#1067 (comment)](https://github.com/sschmid/Entitas/issues/1067#issuecomment-1623734894)

## Code sample 
-  There is a simple .Net6 sample in Entitas.Generic.Sample directory.

## Use guide
- Install [Entitas](https://github.com/sschmid/Entitas) before this.

- Copy this Entitas.Generic directory into your project.

- Copy `EntitasMeta.cs` file into your project if you want.

- ❗Notice: if you want use `EntityIndex`, you need Add or Replace component like below:
  ```csharp
  var entity = GameCtx.Inst.CreateEntity();

  var playerComp = entity.Create<Player>();
  playerComp.Id = 1,
  playerComp.Name = "Jack";
  
  entity.Replace(playerComp);
  ```

- Get a component if it is unique ( get the first entity in group )
  ```csharp
  var theOne = GameCtx.Inst.GetComp<Player>();
  ```

- Flag a empty component if it is unique
  ```csharp
  GameCtx.Inst.SetComp<Player>();
  ```

- You need create and init Entity Index manually(Luckly it is simple and don't need change frequently)

## TODO:
### restore original Entitas's functionality:
- [x] EntityIndex
- [x] PrimaryEntityIndex
- [x] EventAttribute
- [x] CleanupAttribute
- [x] UniqueAttribute
- [ ] Context name?
- [ ] Unity Package

### Addons:
- [ ] EntitytBehaviour
- [ ] ComponentBehaviour
- [ ] ComponentID
- [ ] Some Code Generation:
  - [ ] Initialize all Scopes/Indexes/etc
  - [ ] Dropdown with all components for Unity Editor
  - [ ] EventSystems/CleanupSystems

## Limitations (compared to the original Entitas)
- `EntityIndex` and `PrimaryEntityIndex` components may be only in one context (\scope)
- Mostly support for value components with only 1 field, and for flag components
- Unique components may be used as usual components

## Variants
### CleanupAttribute
Now, to mark some component as cleanup, you need to mark it with marker-interface `ICleanup<RemoveComponent>`, or `ICleanup<DestroyEntity>` <br>
But also you can just remove constraints from `RemoveComponentsSystem` and `DestroyEntitySystem` accordingly. and just set into their type parameters any components <br>
I've used these interfaces to be able to quickly see that the component will cause a Cleanup. And not to guess by component's name:D
