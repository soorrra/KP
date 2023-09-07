//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace _3Racha
//{
//    public class Data
//    {
//        public static List<User> GetUsers()
//        {
//            var list=new List<User>();
//            using(var ctx=new DataContext())
//            {
//                foreach(var u in ctx.Users)
//                {
//                    list.Add(u);
//                }
//            }
//            return list;
//        }
//    }
//}
