using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DailyProgrammer_245I
{
    public static class DecoderTests
    {
        // TODO - should probably test more than just the valid sample case... but for now I'm doing just the one.
        [Fact]
        public static void TestDecoderSuccess()
        {
            // Arrange
            string key = "H GgG d gGg e ggG l GGg o gGG r Ggg w ggg";
            string alienMessage = "GgGggGGGgGGggGG, ggggGGGggGGggGg!";
            string expectedResult = "Hello, world!";

            Decoder decoder = new Decoder(key);

            // Act
            string result;
            bool didSucceed = decoder.TryDecodeString(alienMessage, out result);

            // Assert
            Assert.True(didSucceed);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(null)]                                      // null message
        [InlineData("GGGgGggGGGgGGggGG, ggggGGGggGGggGg!")]     // Alien letter exceeding max alien letter size.
        [InlineData("Gg,GggGGGgGGggGG, ggggGGGggGGggGg!")]      // Current substring not empty when non-'g' character is found.
        [InlineData("GgGggGGGgGGggGG, ggggGGGggGGggGg!g")]      // Current substring not empty when end of loop is reached.
        public static void TestDecoderFailure(string badAlienMessage)
        {
            // Arrange
            string validKey = "H GgG d gGg e ggG l GGg o gGG r Ggg w ggg";
            Decoder decoder = new Decoder(validKey);

            // Act
            bool didSucceed = decoder.TryDecodeString(badAlienMessage, out string decodedString);

            // Assert
            Assert.False(didSucceed);
        }
    }
}
