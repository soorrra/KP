//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;
//using System.Data.SQLite;

//namespace _3Racha
//{
//    public class DataContext : DbContext
//    {
//        public DataContext() : base(new SQLiteConnection()
//        {
//            ConnectionString = new SQLiteConnectionStringBuilder()
//            {
//                DataSource = "/DB.db",
//                ForeignKeys = true
//            }.ConnectionString
//        }, true)
//        {
//        }
//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
//            base.OnModelCreating(modelBuilder);
//        }
//        public DbSet<User> Users { get; set; }
//    }
//}
