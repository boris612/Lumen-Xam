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

        public List<Letter> GetAllLetters()
        {
            if (_database == null) return null;
            var letters = _database.Query<Letter>("select * from Letter");
            return letters;
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
            if (_database.Table<Letter>() != null) _database.DropTable<Letter>();

            _database.CreateTable<Word>();
            _database.CreateTable<Difficulty>();
            _database.CreateTable<Letter>();
            
            #region Insert words
            _database.Insert(new Word
            {
                Name = "GOL",
                ImagePath = "gol.jpg",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "OBLAK",
                ImagePath = "oblak.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "ČEŠALJ",
                ImagePath = "cesalj.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "LJULJAČKA",
                ImagePath = "ljuljacka.png",
                Difficulty = 2,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "MOBITEL",
                ImagePath = "mobitel.png",
                Difficulty = 2,
                Language = "hr-HR"
            });

            #endregion

            #region Difficulties
            _database.Insert(new Difficulty
            {
                Name = "Lagano",
                Level = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Difficulty
            {
                Name = "Srednje",
                Level = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Difficulty
            {
                Name = "Teško",
                Level = 2,
                Language = "hr-HR"
            }); 
            #endregion

            #region InsertLetters
            _database.Insert(new Letter
            {
                Name = "A",
                ImagePath = "A.png",
                Language = "hr-HR", 
                SoundPath = "a.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "B",
                ImagePath = "B.png",
                Language = "hr-HR",
                SoundPath = "b.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "C",
                ImagePath = "C.png",
                Language = "hr-HR",
                SoundPath = "c.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "Č",
                ImagePath = "CH.png",
                Language = "hr-HR",
                SoundPath = "ch.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "Ć",
                ImagePath = "CC.png",
                Language = "hr-HR",
                SoundPath = "cc.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "D",
                ImagePath = "D.png",
                Language = "hr-HR",
                SoundPath = "d.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "Đ",
                ImagePath = "DD.png",
                Language = "hr-HR",
                SoundPath = "dd.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "DŽ",
                ImagePath = "DZ.png",
                Language = "hr-HR",
                SoundPath = "dz.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "E",
                ImagePath = "E.png",
                Language = "hr-HR",
                SoundPath = "e.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "F",
                ImagePath = "F.png",
                Language = "hr-HR",
                SoundPath = "f.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "G",
                ImagePath = "G.png",
                Language = "hr-HR",
                SoundPath = "g.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "H",
                ImagePath = "H.png",
                Language = "hr-HR",
                SoundPath = "h.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "I",
                ImagePath = "I.png",
                Language = "hr-HR",
                SoundPath = "i.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "J",
                ImagePath = "J.png",
                Language = "hr-HR",
                SoundPath = "j.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "K",
                ImagePath = "K.png",
                Language = "hr-HR",
                SoundPath = "k.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "L",
                ImagePath = "L.png",
                Language = "hr-HR",
                SoundPath = "l.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "LJ",
                ImagePath = "LJ.png",
                Language = "hr-HR",
                SoundPath = "lj.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "M",
                ImagePath = "M.png",
                Language = "hr-HR",
                SoundPath = "m.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "N",
                ImagePath = "N.png",
                Language = "hr-HR",
                SoundPath = "n.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "NJ",
                ImagePath = "NJ.png",
                Language = "hr-HR",
                SoundPath = "nj.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "O",
                ImagePath = "O.png",
                Language = "hr-HR",
                SoundPath = "o.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "P",
                ImagePath = "P.png",
                Language = "hr-HR",
                SoundPath = "p.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "R",
                ImagePath = "R.png",
                Language = "hr-HR",
                SoundPath = "r.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "S",
                ImagePath = "S.png",
                Language = "hr-HR",
                SoundPath = "s.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "Š",
                ImagePath = "SS.png",
                Language = "hr-HR",
                SoundPath = "ss.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "T",
                ImagePath = "T.png",
                Language = "hr-HR",
                SoundPath = "t.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "U",
                ImagePath = "U.png",
                Language = "hr-HR",
                SoundPath = "u.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "V",
                ImagePath = "V.png",
                Language = "hr-HR",
                SoundPath = "v.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "Z",
                ImagePath = "Z.png",
                Language = "hr-HR",
                SoundPath = "z.mp3"
            });
            _database.Insert(new Letter
            {
                Name = "Ž",
                ImagePath = "ZZ.png",
                Language = "hr-HR",
                SoundPath = "zz.mp3"
            });

            #endregion
        }
    }
}
