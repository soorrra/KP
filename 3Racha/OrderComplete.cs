using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace _3Racha
{
    public partial class OrderComplete
    {
        [Key]
        public int order_id { get; set; }
        public int dishID { get; set; }
        public int price { get; set; }
        public string status { get; set; }
        public int table_num { get; set; }
        public string place { get; set; }
        public DateTime dataTime { get; set; }
    }
}
