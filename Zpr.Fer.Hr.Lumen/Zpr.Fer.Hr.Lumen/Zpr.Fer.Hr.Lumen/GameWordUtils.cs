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
        public static Random Random { get; private set; } = new Random();
        private static List<Word> words = App.Database.GetAllWords();
        private static List<Word> usedWords = new List<Word>();
        private static Word word = new Word();
        private static List<Letter> _letters = App.Database.GetAllLetters();


        public static Word GetRandomWord()
        {
            
            while (true)
            {
                word = words[Random.Next(words.Count)];
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

        public static Boolean CheckWord(string Name)
        {
            return word.Name == Name;
        }

        public static Word GetWord()
        {
            return word;
        }

        public static Boolean CheckLetter(int letterCounter, string letter)
        {
            string c = String.Empty;
            if (letterCounter < word.Name.Length)
            {
                c = char.ToString(word.Name[letterCounter]);
            }
            return letter == c;
        }

        public static List<Letter> GetRandomLetters(int count)
        {
            var randomLetters = new List<Letter>();
            for(var i = 0; i < count; i++)
            {
                randomLetters.Add(_letters[Random.Next(_letters.Count)]);
            }
            return randomLetters;
        }

        public static List<Letter> GetLettersForWord(Word word)
        {
            if (word == null || string.IsNullOrWhiteSpace(word.Name)) return null;
            var letters = new List<Letter>();
            var name = word.Name;
            for(var i = 0; i < name.Length; i++)
            {
                switch (name[i])
                {
                    case 'L':
                    case 'N':
                        if (i + 1 < name.Length && name[i + 1] == 'J')
                        {
                            letters.Add(_letters.Where(l => l.Name == "LJ").Single());
                            i++;
                        }
                        else
                            letters.Add(_letters.Where(l => l.Name == name[i].ToString()).Single());
                        break;
                    case 'D':
                        if (i + 1 < name.Length && name[i + 1] == 'Ž')
                        {
                            letters.Add(_letters.Where(l => l.Name == "DŽ").Single());
                            i++;
                        }
                        else
                            letters.Add(_letters.Where(l => l.Name == name[i].ToString()).Single());
                        break;
                    default:
                        letters.Add(_letters.Where(l => l.Name == name[i].ToString()).Single());
                        break;
                }
            }
            return letters;
        }
    }
}
