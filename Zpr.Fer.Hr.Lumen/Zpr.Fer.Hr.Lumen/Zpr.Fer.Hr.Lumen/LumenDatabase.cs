using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Zpr.Fer.Hr.Lumen.Models;

namespace Zpr.Fer.Hr.Lumen
{
    public class LumenDatabase
    {
        private static SQLiteConnection _database;
        public LumenDatabase(string path)
        {
            _database = new SQLiteConnection(path);
            //if (_database.ExecuteScalar<string>("SELECT name FROM sqlite_master WHERE type='table' AND name='Word'") != "Word")
            //{
                InitializeDatabase();
            //}
        }

        public List<Word> GetAllWords()
        {
            if (_database == null) return null;
            var words = _database.Query<Word>("select * from Word");
            return words;
        }

        public Word GetWord()
        {
            if (_database == null) return null;
            return _database.Get<Word>(x => x.Name == "GOL");
        }

        private static void InitializeDatabase()
        {
            //_database.Execute("drop table difficultyName");
            //_database.Execute("drop table wordname");
            //_database.Execute("drop table word");
            //_database.Execute("drop table difficulty");
            //_database.Execute("drop table language");
            if (_database.Table<Word>() != null) _database.DropTable<Word>();
            if (_database.Table<Difficulty>() != null) _database.DropTable<Difficulty>();

            _database.CreateTable<Word>();
            _database.CreateTable<Difficulty>();

            _database.Insert(new Word
            {
                Name = "GOL",
                ImagePath = "gol.jpg",
                Difficulty = 0,
                Language = "hr-HR"
            });
            _database.Insert(new Difficulty
            {
                Name = "Lagano",
                Level = 0,
                Language = "hr-HR"
            });
        }
    }
}
