using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DailyProgrammer_245I
{
    public static class EncoderTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(7, 3)]
        [InlineData(8, 3)]
        [InlineData(9, 4)]
        public static void TestBitsNeededToFitNumber(int number, int expectedBits)
        {
            // Act
            int bits = Encoder.BitsNeededToFitNumber(number);

            // Assert
            Assert.Equal(expectedBits, bits);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("01", "gG")]
        [InlineData("10", "Gg")]
        public static void TestBitStringToAlienLetter(string bitString, string expectedAlienLetter)
        {
            // Act
            string alienLetter = Encoder.BitStringToAlienLetter(bitString);

            // Assert
            Assert.Equal(expectedAlienLetter, alienLetter);
        }

        [Theory]
        [InlineData("Hello, world!")]
        [InlineData("John Jacob Jingleheimer Schmidt")]
        [InlineData("Super/cali$fragi#listic@expi.ali!docious*")]
        public static void TestEncodeString(string inputString)
        {
            // Act
            (string key, string alienMessage) = Encoder.EncodeString(inputString);

            Decoder decoder = new Decoder(key);

            bool decodedSuccessfully = decoder.TryDecodeString(alienMessage, out string decodedString);

            // Assert
            Assert.True(decodedSuccessfully);
            Assert.Equal(inputString, decodedString);
        }

        // TODO - I should probably add checks that all the exceptions are throwing correctly, but I added those because I wanted to - not because I need this to be robust, so I don't think I'll spend the time.
    }
}
