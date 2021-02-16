using System;
using System.Collections.Generic;

#nullable disable

namespace www.dagligkaffe.dk.Models
{
    public partial class Coffee
    {
        public Coffee()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Storie { get; set; }
        public int Bean { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
