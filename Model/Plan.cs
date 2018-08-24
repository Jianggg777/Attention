using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attention.Model
{
    public class Plan
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PID { get; set; }  // primary key
        public int RID { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int BID { get; set; }
        public string Tip { get; set; }
    }
}
