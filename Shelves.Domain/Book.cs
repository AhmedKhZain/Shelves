using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shelves.Domain
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int CurrentAvailabeCopies { get; set; } = 0;



        public Guid? AuthrId { get; set; }
        public Authr? Authr { get; set; } = null;
        public Book(Guid? authrId, string title, string description, int currentAvailableCopies) : base()
        {
            AuthrId = authrId;
            Title = title;
            Description = description;
            CurrentAvailabeCopies = currentAvailableCopies;
        }
    }
}
