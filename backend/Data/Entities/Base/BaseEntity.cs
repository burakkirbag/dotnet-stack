using System;

namespace stack.Data
{
    public abstract class BaseEntity : BaseEntity<Guid>
    {

    }

    public abstract class BaseEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
