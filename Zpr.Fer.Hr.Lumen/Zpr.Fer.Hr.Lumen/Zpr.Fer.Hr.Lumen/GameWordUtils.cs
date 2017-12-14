using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zpr.Fer.Hr.Lumen.Models;

namespace Zpr.Fer.Hr.Lumen
{
    static class GameWordUtils
    {
        private static Random rnd = new Random();
        private static List<Word> words = App.Database.GetAllWords();
        private static List<Word> usedWords = new List<Word>();
        private static Word word = new Word();

        public static Random getRandom()
        {
            return rnd;
        }

        public static Word getRandomWord()
        {
            
            while (true)
            {
                word = words[rnd.Next(words.Count)];
                if (usedWords != null && usedWords.Contains(word))
                        continue;
                else
                    usedWords.Add(word);
                    break;
            }
            if (usedWords.Count > words.Count - 1)
            {
                usedWords = new List<Word>();
            }
            return word;
        }

        public static Boolean checkWord(string Name)
        {
            return word.Name == Name;
        }

        public static Word getWord()
        {
            return word;
        }

        public static Boolean checkLetter(int letterCounter, string letter)
        {
            string c = String.Empty;
            if (letterCounter < word.Name.Length)
            {
                c = char.ToString(word.Name[letterCounter]);
            }
            return letter == c;
        }
    }
}
