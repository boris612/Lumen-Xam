using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zpr.Fer.Hr.Lumen.Models
{
    public class Letter
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string SoundPath { get; set; }
        public string Language { get; set; }
    }
}
