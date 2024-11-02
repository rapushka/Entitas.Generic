using System.Text;

namespace Entitas.Generic
{
    public abstract class EntityStringBuilderFormatter<TScope> : IEntityFormatter<TScope>
        where TScope : IScope
    {
        public string ToString(Entity<TScope> entity)
        {
            var stringBuilder = new StringBuilder();
            BuildName(ref stringBuilder, entity);

            return stringBuilder.ToString();
        }

        protected abstract void BuildName(ref StringBuilder stringBuilder, in Entity<TScope> entity);
    }
}