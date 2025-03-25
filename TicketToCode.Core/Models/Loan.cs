using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketToCode.Core.Models
{
    public class Loan
    {
        public int Id {get; set;}
        public int BookId {get; set;}
        public int UserId {get; set;}
        public DateTime LoanDate {get; set;} = DateTime.UtcNow;
        public DateTime? ReturnDate {get; set;} // null om den inte är returnerad
    }
}