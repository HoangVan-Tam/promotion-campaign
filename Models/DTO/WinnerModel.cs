using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class WinnerModel
    {
        public int WinnerID { get; set; }
        public string GroupName { get; set; }
        public int EntryID { get; set; }
        public string MobileNo { get; set; }
        public DateTime DateWon { get; set; }
    }
}
