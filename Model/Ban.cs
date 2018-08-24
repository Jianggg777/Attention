using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attention.Model
{
    public class Ban
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BID { get; set; }  // primary key
        public string Content { get; set; }
    }
}
