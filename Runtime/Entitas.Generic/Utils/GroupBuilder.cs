using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Entitas.Generic
{
    [PublicAPI]
    public class GroupBuilder<TScope>
        where TScope : IScope
    {
        private readonly List<IMatcher<Entity<TScope>>> _andMatchers = new();
        private readonly List<IMatcher<Entity<TScope>>> _orMatchers = new();
        private readonly List<IMatcher<Entity<TScope>>> _noneMatchers = new();

        private GroupBuilder() { }

        public static GroupBuilder<TScope> With<TComponent>()
            where TComponent : IComponent, IInScope<TScope>, new()
            => new GroupBuilder<TScope>().And<TComponent>();

        public static GroupBuilder<TScope> Either<TComponent>()
            where TComponent : IComponent, IInScope<TScope>, new()
            => new GroupBuilder<TScope>().Or<TComponent>();

        public GroupBuilder<TScope> And<TComponent>()
            where TComponent : IComponent, IInScope<TScope>, new()
        {
            _andMatchers.Add(ScopeMatcher<TScope>.Get<TComponent>());
            return this;
        }

        public GroupBuilder<TScope> Or<TComponent>()
            where TComponent : IComponent, IInScope<TScope>, new()
        {
            _orMatchers.Add(ScopeMatcher<TScope>.Get<TComponent>());
            return this;
        }

        public GroupBuilder<TScope> Without<TComponent>()
            where TComponent : IComponent, IInScope<TScope>, new()
        {
            _noneMatchers.Add(ScopeMatcher<TScope>.Get<TComponent>());
            return this;
        }

        public IGroup<Entity<TScope>> Build()
        {
            IMatcher<Entity<TScope>> matcher = null;

            if (_andMatchers.Any())
                matcher = ScopeMatcher<TScope>.AllOf(_andMatchers.ToArray());

            if (_orMatchers.Any())
                matcher = ((IAllOfMatcher<Entity<TScope>>)matcher)?.AnyOf(_orMatchers.ToArray())
                    ?? ScopeMatcher<TScope>.AnyOf(_orMatchers.ToArray());

            if (_noneMatchers.Any())
                matcher = ((IAnyOfMatcher<Entity<TScope>>)matcher)?.NoneOf(_noneMatchers.ToArray())
                    ?? ScopeMatcher<TScope>.AllOf().NoneOf(_noneMatchers.ToArray());

            _andMatchers.Clear();
            _orMatchers.Clear();
            _noneMatchers.Clear();

            return Contexts.Instance.GetGroup(matcher);
        }
    }
}