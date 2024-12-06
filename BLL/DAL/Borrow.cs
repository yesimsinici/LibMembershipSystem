using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DAL
{
    public class Borrow
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public DateTime? BorrowDate { get; set; } 
        public DateTime? DueDate { get; set; } 
        public DateTime? ReturnDate { get; set; } 

        
        public int BookId { get; set; }
        public Book Book { get; set; }

        
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
