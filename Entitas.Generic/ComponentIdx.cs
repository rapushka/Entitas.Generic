namespace Entitas.Generic
{
    public class ComponentIdx<TScope, TComponent> where TScope : IScope where TComponent : IComponent, new()
    {
        public static int Id;
    }
}