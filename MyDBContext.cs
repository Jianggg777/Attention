using Attention.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attention
{
    class MyDBContext : DbContext
    {
        public MyDBContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Ban> Bans { get; set; }
        public DbSet<Record> Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            string dbLocation = AppDomain.CurrentDomain.BaseDirectory + "DB.db";
            optionbuilder.UseSqlite("Data Source="+ dbLocation);  // 使用的DB類型
            Logger.WriteLog("read db in "+ dbLocation);
        }

    }

}
