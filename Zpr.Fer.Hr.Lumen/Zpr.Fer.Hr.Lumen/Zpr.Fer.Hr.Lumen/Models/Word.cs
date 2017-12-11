using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zpr.Fer.Hr.Lumen.Models
{
    public class Word
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string ImagePath { get; set; }
        public int DifficultyID { get; set; }
    }
}
