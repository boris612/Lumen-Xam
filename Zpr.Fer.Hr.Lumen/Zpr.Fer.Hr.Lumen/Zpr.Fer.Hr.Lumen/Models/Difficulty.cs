using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zpr.Fer.Hr.Lumen.Models
{
    class Difficulty
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
    }
}
