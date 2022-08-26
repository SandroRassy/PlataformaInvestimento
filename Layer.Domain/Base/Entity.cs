using Layer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
