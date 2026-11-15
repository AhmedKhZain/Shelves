using System;
using System.Collections.Generic;
using System.Text;

namespace Shelves.Domain
{
    public class Authr : BaseEntity
    {
        public ICollection<Book> Books { get; set; } = new List<Book>();



    }
}
