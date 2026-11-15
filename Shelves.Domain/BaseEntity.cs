using System;
using System.Collections.Generic;
using System.Text;

namespace Shelves.Domain
{
    public abstract class BaseEntity
    {
       public Guid Id { get; set; }

        public BaseEntity()
        { Id = Guid.CreateVersion7(); }








    }
}
