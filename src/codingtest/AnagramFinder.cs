using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace codetest
{
    public class AnagramFinder
    {
        public string[] FindAnagrams(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"{filename} does not exist");
                return Array.Empty<string>();
            }

            string[] lines = File.ReadLines(filename).ToArray(); // use array for pre-determined number of lines
            if (lines.Length < 1)
            {
                Console.Write($"file is empty");
                return Array.Empty<string>();
            }

            var anagramGroups = GroupAnagrams(lines);

            return anagramGroups.Select(group => string.Join(",", group.Distinct())).ToArray();
        }

        private List<List<string>> GroupAnagrams(string[] words)
        {
            var anagramGroups = new List<List<string>>();   // use list for dynamically changing groups
            var wordToGroupMapping = new Dictionary<string, List<string>>();  // use dictionary for fast key value retrieval of dynamic groups

            foreach (var word in words)
            {
                string sortedWord = new string(word.OrderBy(c => c).ToArray());

                if (wordToGroupMapping.TryGetValue(sortedWord, out var group))
                {
                    group.Add(word);
                }
                else
                {
                    var newGroup = new List<string> { word };
                    wordToGroupMapping[sortedWord] = newGroup;
                    anagramGroups.Add(newGroup);
                }
            }

            return anagramGroups;
        }

    }
}
