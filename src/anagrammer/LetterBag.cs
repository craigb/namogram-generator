using System.Linq;
using System.Collections.Generic;

namespace anagrammer
{
    public record LetterBag
    {
        public static LetterBag Empty { get; } = new LetterBag(string.Empty);

        public IReadOnlyDictionary<char, int> LetterGrouping { get; init; }

        public ISet<char> LetterProfile { get; init; }

        public int Count => LetterGrouping.Count;

        public LetterBag(string value)
        {
            LetterGrouping = value.ToUpperInvariant().GroupBy(letter => letter).ToDictionary(g => g.Key, g => g.Count());
            LetterProfile = LetterGrouping.Keys.ToHashSet();
        }

        internal LetterBag(IReadOnlyDictionary<char, int> bag)
        {
            LetterGrouping = bag;
            LetterProfile = LetterGrouping.Keys.ToHashSet();
        }

        public bool TryGetAnagramRemainder(LetterBag component, out LetterBag remainder)
        {
            remainder = LetterBag.Empty;
            if (!component.LetterProfile.IsSubsetOf(LetterProfile))
            {
                return false;
            }

            var dictionary = new Dictionary<char, int>();
            foreach (var letter in LetterProfile)
            {
                var letterCount = LetterGrouping[letter];
                if (component.LetterGrouping.TryGetValue(letter, out int componentCount))
                {
                    letterCount -= componentCount;
                    if (letterCount < 0)
                    {
                        return false;
                    }
                }
                if (letterCount > 0)
                {
                    dictionary[letter] = letterCount;
                }
            }

            if (dictionary.Count is 0)
            {
                remainder = LetterBag.Empty;
            }
            else
            {
                remainder = new LetterBag(dictionary);
            }
            return true;
        }
    }
}