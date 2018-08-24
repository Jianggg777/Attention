using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attention.Model
{
    public class Record
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RID { get; set; }  // primary key
        public string Name { get; set; }
        public DateTime Today { get; set; }
        public bool IsSaved { get; set; }
    }
}
