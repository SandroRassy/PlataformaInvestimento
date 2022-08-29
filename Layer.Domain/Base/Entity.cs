using Layer.Domain.Interfaces;

namespace Layer.Domain.Base
{
    public abstract class Entity : IEntity
    {
        public Guid Key { get; set; }

        protected Entity()
        {
            this.Key = Guid.NewGuid();
        }
    }
}
