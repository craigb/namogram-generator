using System;
using System.Linq;
using System.Collections.Generic;

namespace anagrammer
{
    public record Word
    {
        public string Value { get; init; }
        public IReadOnlyDictionary<char, int> LetterGrouping { get; init; }
        public Word(string value)
        {
            Value = value;
            LetterGrouping = value.GroupBy(letter => letter).ToDictionary(g => g.Key, g => g.Count());
        }
    }
}