using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;


namespace _3Racha
{
    public partial class Dish
    {
        [Key]
        public int dishID { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public int price { get; set; }
        public int weight { get; set; }
    }
}
