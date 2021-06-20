using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string fullName { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
