using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace DailyProgrammer_245I
{
    public class Decoder
    {
        private Dictionary<string, char> alienDictionary = new Dictionary<string, char>();
        private int maxAlienLetterLength = 0;

        // Parameterless constructor should not be used.
        private Decoder() { }

        public Decoder(string key)
        {
            // Validation.
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or whitespace.", nameof(key));

            key = key.Trim();

            List<string> keySubstrings = key.Split(' ').ToList();

            // Must have an even number of substrings, else we've got a partial KeyValue pair.
            if (keySubstrings.Count % 2 != 0)
                throw new ArgumentException("Key contains a partial KeyValue pair.", nameof(key));

            // Populate dictionary with 'letters' if valid.
            for (int i = 0; i < keySubstrings.Count; i += 2)
            {
                string humanLetter = keySubstrings[i];
                string alienLetter = keySubstrings[i + 1];

                // Ensure the first string in the pair is only a single character.
                if (humanLetter.Length != 1)
                    throw new ArgumentException("Invalid key - each KeyValue pair's human letter should only be one character long.", nameof(key));

                // Ensure the second string in the pair contains only the letter 'G'.
                if (alienLetter.ToLower().Any(character => character != 'g'))
                    throw new ArgumentException("Invalid key - each KeyValue pair's alien letter should only contain the characters 'g' or 'G'.", nameof(key));

                alienDictionary.Add(alienLetter, humanLetter.First());

                // Keep track of the longest alien letter for when we decode.
                if (alienLetter.Length > maxAlienLetterLength)
                    maxAlienLetterLength = alienLetter.Length;
            }
        }

        public bool TryDecodeString(string stringToDecode, out string decodedString)
        {
            // Return value.
            decodedString = "";

            // Validation.
            if (stringToDecode == null)
                return false;

            stringToDecode = stringToDecode.Trim();

            // Decoding loop.
            string currentSubstring = "";
            for (int i = 0; i < stringToDecode.Length; i++)
            {
                char currentCharacter = stringToDecode[i];
                bool isCurrentCharacterG = (currentCharacter == 'g' || currentCharacter == 'G');

                // If the current character is a 'g' - add it to the current substring and attempt to decode it.
                if (isCurrentCharacterG)
                {
                    currentSubstring += currentCharacter;

                    // If we have a match, add the corresponding human letter to the decoded string and clear the current substring.
                    if (alienDictionary.ContainsKey(currentSubstring))
                    {
                        decodedString += alienDictionary[currentSubstring];
                        currentSubstring = "";
                    }
                    else
                    {
                        // If we don't have a match and the currentSubstring is too long to be an alien letter we know, we can't decode any further so return false.
                        if (currentSubstring.Length > maxAlienLetterLength)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    // If current substring is not empty when a non-G character is found, then we have an alien letter that we cannot decode and we need to return false.
                    if (currentSubstring.Length != 0)
                    {
                        return false;
                    }

                    // Otherwise, copy the character over to the decoded string.
                    decodedString += currentCharacter;
                }
            }

            // If our current substring is not empty at this point, we have an unmatched alien letter and must return false.
            if (currentSubstring.Length != 0)
                return false;

            // Otherwise we've finished decoding successfully!
            return true;
        }
    }
}
