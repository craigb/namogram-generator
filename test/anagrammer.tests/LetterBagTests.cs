using System;
using Xunit;

namespace anagrammer.tests
{
    public class WordTests
    {
        [Theory]
        [InlineData("cat", "tac")]
        [InlineData("asdf", "FASD")]
        [InlineData("taco", "ta", "co")]
        public void AreAnagrams(string super, params string[] others)
        {
            var bigBag = new LetterBag(super);

            foreach (var word in others)
            {
                if (bigBag.TryGetAnagramRemainder(new LetterBag(word), out var remainder))
                {
                    bigBag = remainder;
                }
                else
                {
                    Assert.True(false, $"'{word}' not found in bag.");
                }
            }

            Assert.Equal(0, bigBag.Count);
        }
    }
}
