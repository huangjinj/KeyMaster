using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyMaster.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string CreateDate { get; set; }
    }
}
