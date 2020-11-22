using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DailyProgrammer_245I
{
    public static class Encoder
    {
        public static (string key, string alienMessage) EncodeString(string input)
        {
            // Validation.
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be null or whitespace.", nameof(input));

            // Get a list of all the unique letters in the input string.
            List<char> uniqueLetters = input.Distinct().Where(character => char.IsLetter(character)).ToList();

            // Determine how many bits we need to fit the number of unique letters.
            int bitsNeeded = BitsNeededToFitNumber(uniqueLetters.Count);

            // Calculate each letter's corresponding alien letter.
            Dictionary<char, string> humanToAlienDictionary = new Dictionary<char, string>();
            for (int i = 0; i < uniqueLetters.Count; i++)
            {
                // Get a bit string for the number of the current iteration (i).
                string bitString = Convert.ToString(i, 2);

                // Pad the bit string with 0s until we get the desired length.
                bitString = bitString.PadLeft(bitsNeeded, '0');

                // Convert the bit string to our alien letter and store in the dictionary.
                string alienLetter = BitStringToAlienLetter(bitString);
                humanToAlienDictionary.Add(uniqueLetters[i], alienLetter);
            }

            string key = GetKeyStringFromDictionary(humanToAlienDictionary);
            string alienMessage = EncodeStringUsingDictionary(input, humanToAlienDictionary);

            return (key, alienMessage);
        }

        // Returns how many bits are needed to fit a given number.
        internal static int BitsNeededToFitNumber(int number)
        {
            // Dealing with edge cases.
            if (number <= 0)
                return 0;
            if (number == 1)
                return 1;

            // ln(N) / ln(2) rounded up will give us the number of bits.
            double result = Math.Log(number) / Math.Log(2);

            return (int)(Math.Ceiling(result));
        }

        // Converts a bit string to an alien letter. '0' becomes 'g', '1' becomes 'G'.
        internal static string BitStringToAlienLetter(string bitString)
        {
            return bitString.Replace('0', 'g').Replace('1', 'G');
        }

        // Formats the dictionary as a key string.
        internal static string GetKeyStringFromDictionary(Dictionary<char, string> humanToAlienDictionary)
        {
            string keyString = "";

            foreach (var kvPair in humanToAlienDictionary)
            {
                // Each kvPair has the human letter first, then the alien letter.
                keyString += kvPair.Key + " " + kvPair.Value + " ";
            }

            // Trim the trailing space and return.
            return keyString.Trim();
        }

        // Performs the actual string encoding once we have a dictionary.
        internal static string EncodeStringUsingDictionary(string inputString, Dictionary<char, string> humanToAlienDictionary)
        {
            string encodedString = "";

            foreach (char c in inputString)
            {
                if (char.IsLetter(c))
                {
                    // Convert letters to alien letters and append to the new string.
                    encodedString += humanToAlienDictionary[c];
                }
                else
                {
                    // Copy non-letters directly to the new string.
                    encodedString += c;
                }
            }

            return encodedString;
        }
    }
}
