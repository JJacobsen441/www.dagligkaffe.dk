using System;
using System.Collections.Generic;

#nullable disable

namespace www.dagligkaffe.dk.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Comment1 { get; set; }
        public int CoffeeId { get; set; }

        public virtual Coffee Coffee { get; set; }
    }
}
