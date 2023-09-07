using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3Racha
{
    public partial class OrderQueue
    {

        [Key]
        public int dishID { get; set; }
        public int order_id { get; set; }
        public int price { get; set; }
    }
}
