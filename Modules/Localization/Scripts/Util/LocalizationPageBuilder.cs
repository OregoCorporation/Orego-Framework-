using System.Collections.Generic;
using OregoFramework.Util;
using static OregoFramework.Module.LocalizationPage;

namespace OregoFramework.Module
{
    public static class LocalizationPageBuilder
    {
        public static List<LanguageDictionary> BuildDictionaries(string text)
        {
            var lines = ParseText(text);
            var lineCount = lines.Count;
            if (lineCount == Int.ZERO)
            {
                return new List<LanguageDictionary>();
            }

            var entityCount = lineCount - Int.ONE;

            //Build empty dictionaries:
            var headerRow = lines[Int.ZERO];
            var headerChunks = ParseLine(headerRow);
            var languageCount = headerChunks.Count - Int.ONE;
            var dictionaries = new List<LanguageDictionary>();
            for (var i = Int.ZERO; i < languageCount; i++)
            {
                var languageText = headerChunks[i + Int.ONE];
                var dictionary = new LanguageDictionary
                {
                    language = languageText,
                    entities = new List<LanguageEntity>()
                };
                dictionaries.Add(dictionary);
            }

            //Fill dictionaries:
            for (var i = Int.ZERO; i < entityCount; i++)
            {
                var line = lines[i + Int.ONE];
                var chunks = ParseLine(line);
                var key = chunks[Int.ZERO];
                if (!key.StartsWith("TXT"))
                {
                    continue;
                }

                for (var j = Int.ZERO; j < languageCount; j++)
                {
                    var dictionary = dictionaries[j];
                    var entity = new LanguageEntity
                    {
                        key = key,
                        translation = chunks[j + Int.ONE]
                    };
                    dictionary.entities.Add(entity);
                }
            }

            return dictionaries;
        }

        private static List<string> ParseText(string text)
        {
            var lines = new List<string>();
            var textLength = text.Length;
            var currentPointer = Int.ZERO;
            var startPosition = Int.ZERO;
            var quotesMode = false;
            while (currentPointer < textLength)
            {
                var character = text[currentPointer++];
                if (character == '\n' && !quotesMode)
                {
                    var lineLength = currentPointer - startPosition - Int.ONE;
                    var line = text.Substring(startPosition, lineLength);
                    lines.Add(line);
                    startPosition = currentPointer;
                }

                if (character == '"')
                {
                    quotesMode = !quotesMode;
                }
            }

            var endLineLength = currentPointer - startPosition;
            var endLine = text.Substring(startPosition, endLineLength);
            lines.Add(endLine);
            return lines;
        }

        private static List<string> ParseLine(string line)
        {
            var words = new List<string>();
            var lineLength = line.Length;
            var currentPointer = Int.ZERO;
            var startPosition = Int.ZERO;
            var readMode = false;
            while (currentPointer < lineLength)
            {
                var currentCharacter = line[currentPointer++];
                if (currentCharacter != '"')
                {
                    continue;
                }

                if (!readMode)
                {
                    readMode = true;
                    startPosition = currentPointer;
                    continue;
                }

                var wordLength = currentPointer - startPosition - Int.ONE;
                var word = line.Substring(startPosition, wordLength);
                words.Add(word);
                readMode = false;
            }

            return words;
        }
    }
}