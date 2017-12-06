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
            if (_database.ExecuteScalar<string>("SELECT name FROM sqlite_master WHERE type='table' AND name='Word'") != "Word")
            {
                InitializeDatabase();
            }
        }

        public List<Word> GetAllWords()
        {
            if (_database == null) return null;
            var words = _database.Query<Word>("SELECT * FROM Word");
            return words;
        }

        public Word GetWord()
        {
            if (_database == null) return null;
            var word = _database.FindWithQuery<Word>("SELECT * FROM Word ORDER BY LastUsed LIMIT 1");
            return word;
        }

        private static void InitializeDatabase()
        {
            #region CreateTables
            _database.Execute("CREATE TABLE Word( " +
                        "ID INT PRIMARY KEY," +
                        "ImagePath TEXT UNIQUE NOT NULL," +
                        "LastUsed DATETIME" +
                    ");" +
                    "CREATE TABLE Language(" +
                        "ID INT PRIMARY KEY," +
                        "Name TEXT UNIQUE NOT NULL" +
                    ");" +
                    "CREATE TABLE Difficulty(" +
                        "ID INT PRIMARY KEY" +
                    ");" +
                    "CREATE TABLE WordName(" +
                        "ID INT PRIMARY KEY," +
                        "WordID INT NOT NULL," +
                        "LanguageID INT NOT NULL," +
                        "Name TEXT NOT NULL," +
                        "DifficultyID INT NOT NULL," +
                        "FOREIGN KEY(WordID) REFERENCES Word(ID)," +
                        "FOREIGN KEY(LanguageID) REFERENCES Language(ID)," +
                        "FOREIGN KEY(DifficultyID) REFERENCES DifficultyID(ID)," +
                        "UNIQUE(WordID, LanguageID, DifficultyID)" +
                    ");" +
                    "CREATE TABLE DifficultyName(" +
                        "ID INT PRIMARY KEY, " +
                        "DifficultyID INT NOT NULL, " +
                        "LanguageID INT NOT NULL, " +
                        "Name TEXT NOT NULL, " +
                        "FOREIGN KEY(DifficultyID) REFERENCES(Difficulty), " +
                        "FOREIGN KEY(LanguageID) REFERENCES(Language), " +
                    ");");
            #endregion


            #region Insert
            _database.Execute("INSERT INTO Word(ID, ImagePath) VALUES(0, 'gol.jpg')");
            _database.Execute("INSERT INTO Language(ID, Name) VALUES(0, 'hr-HR')");
            _database.Execute("INSERT INTO Difficulty(ID) VALUES(0)");
            _database.Execute("INSERT INTO DifficultyName(ID, DifficultyID, LanguageID, Name) " +
                "VALUES(0, 0, 0, 'Lagano')");
            _database.Execute("INSERT INTO WordName(ID, WordID, LanguageID, DifficultyID, Name)" +
                "VALUES(0, 0, 0, 0, 'GOL')");
            #endregion
        }
    }
}
