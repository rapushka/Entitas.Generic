using System.Text;

namespace Entitas.Generic
{
    public abstract class EntityStringBuilderFormatter<TScope> : IEntityFormatter<TScope>
        where TScope : IScope
    {
        private StringBuilder _stringBuilder;

        public string ToString(Entity<TScope> entity)
        {
            _stringBuilder ??= new StringBuilder();
            _stringBuilder.Clear();

            BuildName(ref _stringBuilder, entity);

            return _stringBuilder.ToString();
        }

        protected abstract void BuildName(ref StringBuilder stringBuilder, in Entity<TScope> entity);
    }
}