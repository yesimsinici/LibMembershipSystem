using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        
        public string Author { get; set; }
        public DateTime? PublicationYear { get; set; }
        public string ISBN { get; set; }
        [Required]
        [StringLength(13)]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Borrow>Borrows { get; set; }=new List<Borrow>();

    }
}
