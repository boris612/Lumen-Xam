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
            var difficulty = Helpers.Settings.DifficultyOption;
            var language = Helpers.Settings.Language;
            if (difficulty == -1)
            {
                difficulty = CalculateDifficulty();
            }
            var words = _database.Query<Word>($"select * from Word where Language = '{language}' and Difficulty = {difficulty}");
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
            var language = Helpers.Settings.Language;
            var letters = _database.Query<Letter>($"select * from Letter where Language = '{language}'");
            return letters;
        }

        public List<Difficulty> GetAllDifficulties()
        {
            if (_database == null) return null;
            var language = Helpers.Settings.Language;
            var difficulties = _database.Query<Difficulty>($"select * from Difficulty where Language = '{language}' order by Level");
            return difficulties;
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

            _database.Insert(new Word
            {
                Name = "BEBA",
                ImagePath = "beba.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "CIPELA",
                ImagePath = "cipela.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "ČARAPE",
                ImagePath = "carape.png",
                Difficulty = 2,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "DOKTOR",
                ImagePath = "doktor.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "GITARA",
                ImagePath = "gitara.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "GUMICA",
                ImagePath = "gumica.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "HLAĆE",
                ImagePath = "hlace.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "JAGODA",
                ImagePath = "jagoda.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "JAKNA",
                ImagePath = "jakna.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "KAMEN",
                ImagePath = "kamen.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "KIŠA",
                ImagePath = "kisa.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "KAVA",
                ImagePath = "kava.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "KEMIJSKA",
                ImagePath = "kemijska.png",
                Difficulty = 2,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "KIŠOBRAN",
                ImagePath = "kisobran.png",
                Difficulty = 2,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "KLJUČ",
                ImagePath = "kljuc.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "KNJIGA",
                ImagePath = "knjiga.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "KOCKA",
                ImagePath = "kocka.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "KOLICA",
                ImagePath = "kolica.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "KREVET",
                ImagePath = "krevet.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "KRUG",
                ImagePath = "krug.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "LIST",
                ImagePath = "list.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "LONAC",
                ImagePath = "lonac.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "LUK",
                ImagePath = "luk.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "MAGARAC",
                ImagePath = "magarac.png",
                Difficulty = 2,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "MAJICA",
                ImagePath = "majica.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "MIKROFON",
                ImagePath = "mikrofon.png",
                Difficulty = 2,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "MJESEC",
                ImagePath = "mjesec.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "MUNJA",
                ImagePath = "munja.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "NOVAC",
                ImagePath = "novac.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "OGRADA",
                ImagePath = "ograda.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "OKO",
                ImagePath = "oko.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "OLOVKA",
                ImagePath = "olovka.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "ORMAR",
                ImagePath = "ormar.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "OSA",
                ImagePath = "osa.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "PAPIGA",
                ImagePath = "papiga.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "PAPRIKA",
                ImagePath = "paprika.png",
                Difficulty = 2,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "PČELA",
                ImagePath = "pcela.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "PIRAMIDA",
                ImagePath = "piramida.png",
                Difficulty = 2,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "POLICIJA",
                ImagePath = "policija.png",
                Difficulty = 2,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "RADIO",
                ImagePath = "radio.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "RAK",
                ImagePath = "rak.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "RAVNALO",
                ImagePath = "ravnalo.png",
                Difficulty = 2,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "REKET",
                ImagePath = "reket.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "RODA",
                ImagePath = "roda.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "RUŽA",
                ImagePath = "ruza.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "SALATA",
                ImagePath = "salata.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "ŠALICA",
                ImagePath = "salica.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "ŠKOLJKA",
                ImagePath = "skoljka.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "ŠLJIVA",
                ImagePath = "sljiva.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "SOK",
                ImagePath = "sok.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "STOL",
                ImagePath = "stol.png",
                Difficulty = 0,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "SUNCE",
                ImagePath = "sunce.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "SVEMIR",
                ImagePath = "svemir.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "TORBA",
                ImagePath = "torba.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "TRAVA",
                ImagePath = "trava.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "TREšNJA",
                ImagePath = "tresnja.png",
                Difficulty = 2,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "TROKUT",
                ImagePath = "trokut.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "VATRA",
                ImagePath = "vatra.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "VILICA",
                ImagePath = "vilica.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "VJETAR",
                ImagePath = "vjetar2.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "VRATA",
                ImagePath = "vrata.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "ZEMLJA",
                ImagePath = "zemlja.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "ZVONO",
                ImagePath = "zvono.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            _database.Insert(new Word
            {
                Name = "ŽLICA",
                ImagePath = "zlica.png",
                Difficulty = 1,
                Language = "hr-HR"
            });

            #endregion

            #region Difficulties

            _database.Insert(new Difficulty
            {
                Name = "Automatski",
                Level = -1,
                Language = "hr-HR"
            });

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

        private static int CalculateDifficulty()
        {
            var correctGuesses = Helpers.Settings.CorrectGuesses;
            var falseGuesses = Helpers.Settings.FalseGuesses;
            var sum = correctGuesses + falseGuesses;
            if (sum < 10) return 0;

            var ratio = (double)correctGuesses / sum;
            if (ratio <= 0.3)
                return 0;
            else if (ratio > 0.3 && ratio <= 0.6)
                return 1;
            else 
                return 2;
        }
    }
}
