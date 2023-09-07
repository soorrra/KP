using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;


namespace _3Racha
{
    public partial class User
    {
        [Key]
        public int userID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string post { get; set; }

    }

}
