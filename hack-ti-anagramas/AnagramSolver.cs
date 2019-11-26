using System;
using System.Collections.Generic;
using System.Linq;

namespace hack_ti_anagramas
{
    public class AnagramSolver
    {
        private static List<string> listAllAnagrams = new List<string>();

        private static string sentence = string.Empty;
        private static string remainingText = string.Empty;
        
        private static List<string> anagram = new List<string>();
        private static List<string> wordsThatFitRemaining = new List<string>();
        
        public static List<string> GenerateAnagrams(string inputSentence)
        {
            listAllAnagrams.Clear();

            sentence = Util.RemoveSpaceStringReader(inputSentence);
            remainingText = sentence;

            List<string> listValidWords = ReadFileTXT.ReadValidWordsFile();

            List<string> wordsThatFit = AnagramSolver.SearchWordsThatFit(sentence, listValidWords);

            recursiveWordsFit(wordsThatFit, true);

            return listAllAnagrams.OrderBy(f => f).Distinct().ToList();
        }

        private static void recursiveWordsFit(List<string> wordsThatFit, bool isFirstCall = false)
        {
            foreach (var word in wordsThatFit)
            {
                if (isFirstCall)
                {
                    anagram.Clear();
                    remainingText = sentence;
                }

                anagram.Add(word);

                string remainingTextBefore = new string(remainingText);
                bool remainingChar = true;
                foreach (var c in word)
                {
                    int indexOfChar = remainingText.IndexOf(c);
                    if(indexOfChar != -1)
                    {
                        remainingText = remainingText.Remove(indexOfChar, 1);
                    }
                    else
                    {
                        remainingChar = false;
                    }
                }

                if(!remainingChar)
                {
                    anagram.RemoveAt(anagram.Count - 1);
                    remainingText = remainingTextBefore;

                    continue;
                }

                wordsThatFitRemaining = AnagramSolver.SearchWordsThatFit(remainingText, wordsThatFit);

                if (wordsThatFitRemaining.Count > 0)
                {
                    recursiveWordsFit(wordsThatFitRemaining);
                }
                else
                {
                    if(remainingText == string.Empty)
                    {
                        string newAnagram = new string(anagram.OrderBy(f => f).Aggregate((i, j) => i + " " + j));
                        listAllAnagrams.Add(newAnagram);
                        anagram.Clear();
                        remainingText = sentence;
                    }
                    else
                    {
                        anagram.RemoveAt(anagram.Count -1);
                        remainingText = remainingTextBefore;
                    }
                }
            }
        }

        private static List<string> SearchWordsThatFit(string inputSentence, List<string> listValidWords)
        {          
            List<string> searchedWords = new List<string>();
            List<string> listToRemove = new List<string>();

            //Only words that have all characters 
            foreach (var word in listValidWords)
            {
                var results = inputSentence.Split().Where(str => word.All(c => str.Contains(c))).ToList();

                if (results.Count > 0)
                {
                    searchedWords.Add(word);
                }
            }

            //Counts how many equal characters in the sentence 
            var mappedCharSentence = inputSentence.GroupBy(c => c).Select(c => new Tuple<char, int>(c.Key, c.Count()));

            //Remove words that have more characters than the sentence
            foreach (var word in searchedWords)
            {
                var mappedCharPossibleWord = word.GroupBy(c => c).Select(c => new Tuple<char, int>(c.Key, c.Count()));

                foreach (var ch in mappedCharPossibleWord)
                {
                    if (ch.Item2 > mappedCharSentence.FirstOrDefault(f => f.Item1 == ch.Item1).Item2)
                    {
                        listToRemove.Add(word);
                    }
                }
            }

            foreach (var wordRemove in listToRemove)
            {
                searchedWords.Remove(wordRemove);
            }

            return searchedWords;
        }          
    }
}
