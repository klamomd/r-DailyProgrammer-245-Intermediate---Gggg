﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DailyProgrammer_245I
{
    public class DecoderTests
    {
        [Theory]
        [InlineData("H GgG d gGg e ggG l GGg o gGG r Ggg w ggg",
            "GgGggGGGgGGggGG, ggggGGGggGGggGg!",
            "Hello, world!")]
        [InlineData("a GgG d GggGg e GggGG g GGGgg h GGGgG i GGGGg l GGGGG m ggg o GGg p Gggg r gG y ggG",
            "GGGgGGGgGGggGGgGggG /gG/GggGgGgGGGGGgGGGGGggGGggggGGGgGGGgggGGgGggggggGggGGgG!",
            "hooray /r/dailyprogrammer!")]
        [InlineData("C GgggGgg H GgggGgG T GgggGGg a gGg c GGggG d GggG e GgG g ggGgG h GGgGg i gGGg j GgggGGG l gGGG m ggGGg n GGgGG o ggg p ggGGG r GGGg s GGGG t GGgggG u ggGgg v Ggggg w GGggggg y GGggggG",
            "GgggGGgGGgGggGGgGGGG GGggGGGgGggGggGGGgGGGGgGGGgGGggGgGGgG GGggggggGgGGGG ggGGGGGGggggggGGGgggGGGGGgGGggG gGgGGgGGGggG GggGgGGgGGGGGGggGggGggGGGGGGGGGgGGggG gggGggggGgGGGGg gGgGGgggG /GGGg/GggGgGggGGggGGGGGggggGggGGGGGGggggggGgGGGGggGgggGGgggGGgGgGGGGg_gGGgGggGGgGgGgGGGG. GgggGgGgGgGggggGgG gGg GGggGgggggggGGG GGggGGGgGggGggGGGgGGGGgGGGgGGggGgGGgG gGGgGggGGgGgGg? GgggGgggggggGGgGgG GgggGGGggggGGgGGgGG ggGggGGGG gggGggggGgGGGGg GGgggGGGgGgGgGGGGgGgG!",
            "This challenge was proposed and discussed over at /r/dailyprogrammer_ideas. Have a cool challenge idea? Come join us over there!")]
        public void TestDecoderSuccess(string key, string alienMessage, string expectedResult)
        {
            // Arrange
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
        public void TestDecoderFailure(string badAlienMessage)
        {
            // Arrange
            string validKey = "H GgG d gGg e ggG l GGg o gGG r Ggg w ggg";
            Decoder decoder = new Decoder(validKey);

            // Act
            bool didSucceed = decoder.TryDecodeString(badAlienMessage, out string decodedString);

            // Assert
            Assert.False(didSucceed);
        }

        // TODO - I should probably add checks that all the exceptions are throwing correctly, but I added those because I wanted to - not because I need this to be robust, so I don't think I'll spend the time.
    }
}
